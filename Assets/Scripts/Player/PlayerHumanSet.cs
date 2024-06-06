using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHumanSet : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerController playerController;
    public PlayerController enemyHealthBar;
    public bool isPunching;
    public bool isKicking;
    public string firstPlayerTag = "Player1";
    public string otherPlayerTag = "Player2";
    public float punchCooldown = 0.2f;
    private float punchCooldownTimer = 0f;
    public bool isTouchingPlayer;
    public GameObject sword;
    public TransformedSwordAttack transformSword;
    public bool isFacingRight;
    private string lastCollidedTag;
    public bool isSecondPlayer;
    public float transformDamage = 1f;
    public bool isBoosted = false;
    private string currentTag;


    public AudioClip swordSwingAudio;
     public AudioClip swordSlashAudio;
    public AudioClip kickAudio;
    
    public AudioSource audioManager;
    

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
        audioManager = GetComponent<AudioSource>();
        
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 100f, direction*5000f);
        Debug.DrawRay(transform.position + Vector3.up * 100f, direction * 5000f, Color.red);
        if (hit.collider != null)
        {
            lastCollidedTag = hit.collider.gameObject.tag;
            //Debug.Log("Last collided object tag: " + lastCollidedTag);
            if (lastCollidedTag == "Left") {
                isSecondPlayer = true;
            }else{
                isSecondPlayer = false;
            }
        }
        currentTag = gameObject.tag;
    }

    void Update()
    {
        animator.SetBool("isPunching", isPunching);
        animator.SetBool("isKicking", isKicking);
        if (punchCooldownTimer > 0)
        {
            punchCooldownTimer -= Time.deltaTime;
        }
        if(playerController.isTransformed == true && isBoosted == false){
            transformDamage = transformDamage * 1.5f;
            isBoosted = true;
        }
    }

    public void Punch(InputAction.CallbackContext context)
    {
        if (context.started && punchCooldownTimer <= 0 && isKicking == false && playerController.GetStam() >= 2)
        {
            playerController.LoseStam(1);
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
            Vector3 currentPosition = transform.position;
            print(yVal);
            Vector3 spawnPosition = currentPosition + new Vector3(yVal, 100f, 0);
            if(playerController.isTransformed == true){
                audioManager.clip = swordSlashAudio;
                audioManager.Play();
                playerController.LoseStam(2);
                punchCooldown = 5f;
                transformSword.direction = isFacingRight;
                TransformedSwordAttack taS = transformSword.GetComponent<TransformedSwordAttack>();
                taS.currentTag = currentTag;
                spawnPosition = currentPosition + new Vector3(yVal, 1f, 0);
                Instantiate(transformSword, spawnPosition, Quaternion.identity);
            }else{
                audioManager.clip = swordSwingAudio;
                audioManager.Play();
                SwordAttack sa = sword.GetComponent<SwordAttack>();
                sa.currentTag = currentTag;
                sa.player = gameObject;
                sa.offset = new Vector3(yVal, 100f, 0);
                Instantiate(sword, spawnPosition, Quaternion.identity);
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
        if (context.started && isPunching == false && playerController.GetStam()  >= 4)
        {
            playerController.LoseStam(3);
            isKicking = true;
            audioManager.clip = kickAudio;
            audioManager.Play();
            if(isTouchingPlayer == true)
                {
                    enemyHealthBar.TakeDamage((int) (7f * transformDamage));
                }
            punchCooldownTimer = punchCooldown;
        }
        if (context.canceled)
        {
            isKicking = false;
        }
    }

    public void Guard(InputAction.CallbackContext context)
    {
        if(playerController.GetStam() > 0){
            if(context.performed )
            {
                animator.SetBool("isGuarding", true);
                playerController.isDefending = true;
            }

            if(context.canceled){
                animator.SetBool("isGuarding", false);
                playerController.isDefending = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(otherPlayerTag) || collision.gameObject.CompareTag(firstPlayerTag))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(otherPlayerTag) || collision.gameObject.CompareTag(firstPlayerTag))
        {
            isTouchingPlayer = false;
        }
    }
}
