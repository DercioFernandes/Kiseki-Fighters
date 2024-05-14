using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 jump;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isRunning;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isDashing;
    private float dashTime;
    private float dashCooldownTime;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 40f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isRunning = false;
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        if(!isFacingRight && horizontal > 0f){
            Flip();
        }else if (isFacingRight && horizontal < 0f){
            Flip();
        }
        isRunning = rb.velocity.x != 0;
        animator.SetBool("running", isRunning);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        isRunning = false;
        if(context.performed && isGrounder())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(context.canceled && rb.velocity.y > 0f){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            
            print("DASHED");
            StartDash();
        }

        if(context.canceled){
            EndDash();
        }
    }

    private void StartDash()
    {
        print("DASHED");
        isDashing = true;
        dashTime = Time.time + dashDuration;
        dashCooldownTime = Time.time + dashCooldown;
        rb.velocity = new Vector2(horizontal * dashSpeed, rb.velocity.y);

        // Optionally, set an animation parameter for dashing
        //animator.SetBool("isDashing", true);
    }

    private void EndDash()
    {
        isDashing = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

        // Optionally, reset the animation parameter for dashing
        //animator.SetBool("isDashing", false);
    }

    private bool isGrounder(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
