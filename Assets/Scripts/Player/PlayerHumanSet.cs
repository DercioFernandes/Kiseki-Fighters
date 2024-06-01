using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHumanSet : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerController playerController;
    public HealthBar enemyHealthBar;
    public bool isPunching;
    public bool isKicking;
    public string otherPlayerTag = "Player2";
    public float punchCooldown = 1.0f;
    private float punchCooldownTimer = 0f;
    public bool isTouchingPlayer;
    public GameObject sword;
    public bool isFacingRight;
    private string lastCollidedTag;
    public bool isSecondPlayer;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isPunching = false;
        isKicking = false;
        isTouchingPlayer = false;
        
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 100f, direction*5000f);
        Debug.DrawRay(transform.position + Vector3.up * 100f, direction * 5000f, Color.red);
        if (hit.collider != null)
        {
            lastCollidedTag = hit.collider.gameObject.tag;
            Debug.Log("Last collided object tag: " + lastCollidedTag);
            if (lastCollidedTag == "Left") {
                isSecondPlayer = true;
            }else{
                isSecondPlayer = false;
            }
        }
    }

    void Update()
    {
        animator.SetBool("isPunching", isPunching);
        animator.SetBool("isKicking", isKicking);
        if (punchCooldownTimer > 0)
        {
            punchCooldownTimer -= Time.deltaTime;
        }
    }

    public void Punch(InputAction.CallbackContext context)
    {
        if (context.started && punchCooldownTimer <= 0 && isKicking == false && playerController.GetStam() >= 2)
        {
            playerController.LoseStam(2);
            isPunching = true;
            float yVal = 0f;
            if(isSecondPlayer == true){
                if(transform.localScale.x > 0){
                    isFacingRight = false;
                    yVal = -200f;
                }else{
                    isFacingRight = true;
                    yVal = 200f;
                }
            }else{
                if(transform.localScale.x > 0){
                    isFacingRight = true;
                    yVal = 200f;
                }else{
                    isFacingRight = false;
                    yVal = -200f;
                }
            }
            punchCooldownTimer = punchCooldown;
            playerController.LoseStam(4);
            Vector3 currentPosition = transform.position;
            print(yVal);
            Vector3 spawnPosition = currentPosition + new Vector3(yVal, 100f, 0);
            Instantiate(sword, spawnPosition, Quaternion.identity);
            punchCooldownTimer = punchCooldown;
        }
        if (context.canceled)
        {
            isPunching = false;
        }
    }

    public void Kick(InputAction.CallbackContext context)
    {
        if (context.started && punchCooldownTimer <= 0 && isPunching == false && playerController.GetStam()  >= 4)
        {
            playerController.LoseStam(4);
            isKicking = true;
            if(isTouchingPlayer == true)
                {
                    enemyHealthBar.TakeDamage(15);
                }
            punchCooldownTimer = punchCooldown;
        }
        if (context.canceled)
        {
            isKicking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(otherPlayerTag))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(otherPlayerTag))
        {
            isTouchingPlayer = false;
        }
    }
}
