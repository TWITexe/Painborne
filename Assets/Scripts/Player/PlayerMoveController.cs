using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float climbSpeed = 3f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private bool jumpPressed;
    private bool isClimbing;

    private Rigidbody2D rb;
    private Animator anim;
    private float horizontalInput;
    private float verticalInput;
    private float baseGravity;

    private Vector3 baseScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        baseScale = transform.localScale;
        baseGravity = rb.gravityScale;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            HandleClimbing();
        }
        else
        {
            HandleMovement();
            Jump();
        }


        Flip();
    }

    private void HandleMovement()
    {
        Vector2 movement = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        if (anim != null)
            anim.SetFloat("speed", Mathf.Abs(movement.x));
    }

    private void HandleClimbing()
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(horizontalInput * speed, verticalInput * climbSpeed);

        //if (anim != null)
        //anim.SetFloat("climbSpeed", Mathf.Abs(verticalInput));
    }

    private void Flip()
    {
        if (horizontalInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput) * baseScale.x, baseScale.y, baseScale.z);
    }

    public void SetClimbing(bool value)
    {
        isClimbing = value;

        if (!value)
            rb.gravityScale = baseGravity;
    }


    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpPressed = false;
    }



}


