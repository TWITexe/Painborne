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

    [Header("Mobile UI")]
    [Tooltip("�������� ������ ������ ���������� UI (Canvas/Panel). ����� ����������/������������.")]
    [SerializeField] private GameObject mobileUIRoot;
    [Tooltip("�������� ��������� ����� �� ��������� �� ��������.")]
    [SerializeField] private bool autoEnableOnMobilePlatform = true;
    [SerializeField] private bool mobileMode = false;

    private bool isGrounded;
    private bool jumpPressed;
    private bool isClimbing;

    private Rigidbody2D rb;
    private Animator anim;
    private float baseGravity;
    private Vector3 baseScale;

    // � �� ����
    private float kbHorizontal;
    private float kbVertical;

    // � ��������� ���� (��������� ������)
    private bool mLeftHeld, mRightHeld, mUpHeld, mDownHeld;
    private bool mJumpPressedFrame; // ������������ �������

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        baseScale = transform.localScale;
        baseGravity = rb.gravityScale;

        if (autoEnableOnMobilePlatform && Application.isMobilePlatform)
            SetMobileMode(true);
        else
            ApplyMobileUIVisibility();
    }

    private void Update()
    {
        // ��������� �� ���� ������ ���� ��������� ����� ��������
        if (!mobileMode)
        {
            kbHorizontal = Input.GetAxisRaw("Horizontal");
            kbVertical = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))
                jumpPressed = true;
        }
        else
        {
            // � ��������� ������ �������� ��� �� ���������
            int h = 0;
            if (mLeftHeld) h -= 1;
            if (mRightHeld) h += 1;

            int v = 0;
            if (mDownHeld) v -= 1;
            if (mUpHeld) v += 1;

            kbHorizontal = h; // �������������� �� �� ����������
            kbVertical = v;

            if (mJumpPressedFrame)
            {
                jumpPressed = true;
                mJumpPressedFrame = false; // ���������� ������������ �������
            }
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            HandleClimbing(kbHorizontal, kbVertical);
        }
        else
        {
            HandleMovement(kbHorizontal);
            Jump();
        }

        Flip(kbHorizontal);
    }

    private void HandleMovement(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        if (anim != null)
            anim.SetFloat("speed", Mathf.Abs(movement.x));
    }

    private void HandleClimbing(float horizontalInput, float verticalInput)
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(horizontalInput * speed, verticalInput * climbSpeed);

        // ��������
        // if (anim != null) anim.SetFloat("climbSpeed", Mathf.Abs(verticalInput));
    }

    private void Flip(float horizontalInput)
    {
        if (horizontalInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput) * baseScale.x, baseScale.y, baseScale.z);
    }

    public void SetClimbing(bool value)
    {
        isClimbing = value;
        rb.gravityScale = value ? 0f : baseGravity;
    }

    private void Jump()
    {
        if (!groundCheck) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpPressed = false;
    }

    // =========================
    //       ���  �������
    // =========================

    public void ToggleMobileMode()
    {
        SetMobileMode(!mobileMode);
    }
    public void SetMobileMode(bool enabled)
    {
        mobileMode = enabled;
        ResetMobileHeld();
        ApplyMobileUIVisibility();
    }

    private void ApplyMobileUIVisibility()
    {
        if (mobileUIRoot)
            mobileUIRoot.SetActive(mobileMode);
    }

    private void ResetMobileHeld()
    {
        mLeftHeld = mRightHeld = mUpHeld = mDownHeld = false;
        mJumpPressedFrame = false;
        kbHorizontal = kbVertical = 0f;
    }
   
    public void MobileLeftDown() { mLeftHeld = true; }
    public void MobileLeftUp() { mLeftHeld = false; }
    public void MobileRightDown() { mRightHeld = true; }
    public void MobileRightUp() { mRightHeld = false; }

    public void MobileUpDown() { mUpHeld = true; }
    public void MobileUpUp() { mUpHeld = false; }
    public void MobileDownDown() { mDownHeld = true; }
    public void MobileDownUp() { mDownHeld = false; }
    public void MobileJumpPress() { mJumpPressedFrame = true; }

}
