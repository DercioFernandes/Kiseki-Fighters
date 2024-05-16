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

    public StaminaBar stamBar;
    public int maxStam = 20;
    public int currentStam;

    public bool isRunning;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isDashing;
    private float dashTime;
    private float dashCooldownTime;
    public float moveSpeed = 500f;
    public float jumpForce = 500f;
    public float dashSpeed = 200f;
    public float dashDistance = 200f;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        currentStam = maxStam;
        stamBar.SetMaxStamina(maxStam);
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
        if(currentStam > 0){
            if(context.performed && isGrounder())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if(context.canceled && rb.velocity.y > 0f){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            LoseStam(1);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(currentStam > 0){
                Vector3 targetPosition;
                print("DASHED");
                // Check the player's facing direction
                    if (transform.localScale.x > 0)
                    {
                        // Player is facing right
                        targetPosition = transform.position + transform.right * dashDistance;
                    }
                    else
                    {
                        // Player is facing left
                        targetPosition = transform.position - transform.right * dashDistance;
                    }
                print(targetPosition);
                transform.position = targetPosition;
                LoseStam(1);
            }
        }
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

    void LoseStam(int staminaToLose)
    {
        currentStam -= staminaToLose;
        stamBar.SetStamina(currentStam);
    }
}
