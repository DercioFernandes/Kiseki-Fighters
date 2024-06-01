using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTankSet : MonoBehaviour
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
            if(isTouchingPlayer == true)
                {
                    enemyHealthBar.TakeDamage(10);
                }
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
