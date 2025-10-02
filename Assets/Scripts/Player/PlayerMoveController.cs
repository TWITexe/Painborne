using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float speed = 4.5f;

    // jump
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private bool jumpPressed;

    private Rigidbody2D rigidBody;
    private Animator anim;

    private Vector3 baseScale;

    private float moveDirection;

    private void Awake()
    {
        baseScale = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            gameObject.GetComponent<Health>().TakeDamage(5);
        }



        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            gameObject.GetComponent<Health>().TakeDamage(5);

    }
    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveDirection * speed, rigidBody.linearVelocity.y);
        rigidBody.linearVelocity = movement;

        if (anim != null)
            anim.SetFloat("speed", Mathf.Abs(movement.x));

        Flip();
        Jump();
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpPressed && isGrounded)
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
        }

        jumpPressed = false;
    }
    private void Flip()
    {
        if (moveDirection != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveDirection) * baseScale.x, baseScale.y, baseScale.z);
    }
}
