using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CockroachAI : MonoBehaviour
{
    [Header("Roam Area")]
    [SerializeField] private BoxCollider2D roamArea;
    [SerializeField] private float borderMargin = 0.1f;

    [Header("Movement")]
    [SerializeField] private float wanderSpeed = 1.3f;
    [SerializeField] private float fleeSpeed = 3.2f;
    [SerializeField] private float targetReachDistance = 0.06f;
    [SerializeField] private float minPause = 0.15f;
    [SerializeField] private float maxPause = 0.6f;

    [Header("Awareness")]
    [SerializeField] private float detectionRadius = 1.4f;
    [SerializeField] private LayerMask playerMask;

    [Header("Obstacles (optional)")]
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float obstacleProbeRadius = 0.1f;

    [Header("Visuals (optional)")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Vector2 currentTarget;
    private float pauseTimer = 0f;
    private bool isPaused = false;
    private bool isScared = false;
    private Vector2 lastVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Start()
    {
        if (roamArea == null)
        {
            Debug.LogWarning($"{name}: Roam Area not set. Please assign BoxCollider2D.");
        }
        PickNewRandomTarget();
    }

    private void Update()
    {
        var player = FindClosestPlayerInRange();
        if (player != null)
        {
            isScared = true;
            Vector2 fleeDir = ((Vector2)transform.position - (Vector2)player.bounds.center).normalized;
            Vector2 farPoint = (Vector2)transform.position + fleeDir * GetMaxAreaExtent();
            currentTarget = ClampToArea(farPoint);
            isPaused = false;
            pauseTimer = 0f;
        }
        else
        {
            if (isScared)
            {
                if (Vector2.Distance(transform.position, currentTarget) <= targetReachDistance * 1.2f)
                {
                    isScared = false;
                    StartPause();
                }
            }
            else
            {
                if (isPaused)
                {
                    pauseTimer -= Time.deltaTime;
                    if (pauseTimer <= 0f)
                    {
                        isPaused = false;
                        PickNewRandomTarget();
                    }
                }
                else
                {
                    if (Vector2.Distance(transform.position, currentTarget) <= targetReachDistance)
                    {
                        StartPause();
                    }
                }
            }
        }

        UpdateAnimatorAndFlip();
    }

    private void FixedUpdate()
    {
        if (isPaused) { rb.linearVelocity = Vector2.zero; return; }

        float speed = isScared ? fleeSpeed : wanderSpeed;
        Vector2 pos = rb.position;
        Vector2 dir = (currentTarget - pos);
        if (dir.sqrMagnitude < 0.0001f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        dir.Normalize();


        Vector2 step = dir * speed * Time.fixedDeltaTime;
        Vector2 next = pos + step;

        if (obstacleMask.value != 0 && Physics2D.OverlapCircle(next, obstacleProbeRadius, obstacleMask))
        {
            if (isScared)
            {
                Vector2 jitter = Random.insideUnitCircle * 0.4f;
                Vector2 tryPoint = ClampToArea(pos + (dir + jitter).normalized * GetMaxAreaExtent());
                currentTarget = tryPoint;
            }
            else
            {
                PickNewRandomTarget();
            }
            rb.linearVelocity = Vector2.zero;
            return;
        }

        next = ClampToArea(next);
        Vector2 vel = (next - pos) / Time.fixedDeltaTime;
        rb.MovePosition(next);
        rb.linearVelocity = vel;
        lastVelocity = vel;
    }

    // API
    public void ScareFrom(Vector2 sourcePosition)
    {
        isScared = true;
        Vector2 fleeDir = ((Vector2)transform.position - sourcePosition).normalized;
        Vector2 farPoint = (Vector2)transform.position + fleeDir * GetMaxAreaExtent();
        currentTarget = ClampToArea(farPoint);
        isPaused = false;
        pauseTimer = 0f;
    }


    private Collider2D FindClosestPlayerInRange()
    {
        if (detectionRadius <= 0f) return null;
        var hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerMask);
        Collider2D closest = null;
        float minSq = float.MaxValue;
        foreach (var h in hits)
        {
            if (!h.CompareTag("Player")) continue;
            float sq = ((Vector2)h.bounds.center - (Vector2)transform.position).sqrMagnitude;
            if (sq < minSq)
            {
                minSq = sq;
                closest = h;
            }
        }
        return closest;
    }

    private void PickNewRandomTarget()
    {
        currentTarget = GetRandomPointInArea();
    }

    private void StartPause()
    {
        isPaused = true;
        pauseTimer = Random.Range(minPause, maxPause);
        rb.linearVelocity = Vector2.zero;
    }

    private Vector2 GetRandomPointInArea()
    {
        if (roamArea == null) return transform.position;
        var b = roamArea.bounds;
        float x = Random.Range(b.min.x + borderMargin, b.max.x - borderMargin);
        float y = Random.Range(b.min.y + borderMargin, b.max.y - borderMargin);
        return new Vector2(x, y);
    }

    private Vector2 ClampToArea(Vector2 p)
    {
        if (roamArea == null) return p;
        var b = roamArea.bounds;
        float x = Mathf.Clamp(p.x, b.min.x + borderMargin, b.max.x - borderMargin);
        float y = Mathf.Clamp(p.y, b.min.y + borderMargin, b.max.y - borderMargin);
        return new Vector2(x, y);
    }

    private float GetMaxAreaExtent()
    {
        if (roamArea == null) return 3f;
        var b = roamArea.bounds;
        Vector2 size = b.size;
        return Mathf.Sqrt(size.x * size.x + size.y * size.y) * 0.5f;
    }

    private void UpdateAnimatorAndFlip()
    {
        if (animator != null)
        {
            animator.SetBool("Scared", isScared);
            animator.SetFloat("Speed", rb.linearVelocity.magnitude);
        }

        if (spriteRenderer != null)
        {
            if (Mathf.Abs(rb.linearVelocity.x) > 0.01f)
                spriteRenderer.flipX = rb.linearVelocity.x < 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (roamArea != null)
        {
            Gizmos.color = Color.cyan;
            var b = roamArea.bounds;
            Gizmos.DrawWireCube(b.center, b.size - new Vector3(borderMargin * 2f, borderMargin * 2f, 0f));
        }

        Gizmos.color = isScared ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, currentTarget);
        Gizmos.DrawWireSphere(currentTarget, 0.06f);
    }
}
