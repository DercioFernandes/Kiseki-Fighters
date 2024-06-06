using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerElfSet : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerController playerController;
    public PlayerController enemyHealthBar;
    public bool isPunching;
    public bool isKicking;
    public string firstPlayerTag = "Player1";
    public string otherPlayerTag = "Player2";
    public float punchCooldown = 0.1f;
    private float punchCooldownTimer = 0f;
    public bool isTouchingPlayer;
    public GameObject magicAttack;
    public GameObject transformedMagicAttack;
    private string lastCollidedTag;
    private bool isFacingRight = true;
    public MagicAttack mA;
    public bool isSecondPlayer;
    public float transformDamage = 1f;
    public bool isBoosted = false;
    private string currentTag;

    public AudioClip zoltraakAudio;
    public AudioClip heighOfMagicAudio;
    public AudioClip kickAudio;
    public AudioClip guardAudio;
    
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
        
        mA = magicAttack.GetComponent<MagicAttack>();
        
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 100f, direction*5000f);
        Debug.DrawRay(transform.position + Vector3.up * 100f, direction * 5000f, Color.red);
        if (hit.collider != null)
        {
            lastCollidedTag = hit.collider.gameObject.tag;
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
        if (context.started && punchCooldownTimer <= 0 && isKicking == false && playerController.GetStam() >= 4)
        {
            playerController.LoseStam(3);
            isPunching = true;
            Vector3 currentPosition = transform.position;
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
            Vector3 spawnPosition = currentPosition + new Vector3(yVal, 200f, 0);
            mA.direction = isFacingRight;
            if(playerController.isTransformed == true){
                audioManager.clip = heighOfMagicAudio;
                audioManager.Play();
                playerController.LoseStam(3);
                punchCooldown = 10f;
                TransformedMagicAttack taM = transformedMagicAttack.GetComponent<TransformedMagicAttack>();
                taM.currentTag = currentTag;
                spawnPosition = currentPosition + new Vector3(yVal, 1f, 0);
                Instantiate(transformedMagicAttack, spawnPosition, Quaternion.identity);
            }else{
                audioManager.clip = zoltraakAudio;
                audioManager.Play();
                mA.currentTag = currentTag;
                Instantiate(magicAttack, spawnPosition, Quaternion.identity);
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
            audioManager.clip = kickAudio;
            audioManager.Play();
            playerController.LoseStam(2);
            isKicking = true;
            if(isTouchingPlayer == true)
                {
                    enemyHealthBar.TakeDamage((int)(5f * transformDamage));
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
                audioManager.clip = guardAudio;
                audioManager.Play();
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
