using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 100;
    public int maxStamina = 20;
    public int currentHealth;
    public int currentStam;
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public bool isTransformable;
    public bool isTransformed;
    public bool isDefending;
    private Animator animator;
    public GameOver gameOverScript;
    public GameObject gameOver;



    void Start()
    {
        StartCoroutine(RunContinuously(3));
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        isTransformable = false;
        isTransformed = false;
        isDefending = false;
        animator = GetComponent<Animator>();
        
        print(GameObject.FindWithTag("GameOverCanvas"));
        gameOver = GameObject.FindWithTag("GameOverCanvas");
        gameOverScript = gameOver.GetComponent<GameOver>();
    }

    void Update()
    {
        currentStam = (int) staminaBar.GetStamina();
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= maxHealth * 0.3f)
            isTransformable = true;
        if(currentHealth <= 0){
            gameOver.SetActive(true);
            gameOverScript.EndGame(gameObject.name);
        }
    }

    public void TakeDamage(int damage)
    {
        if(isDefending == true){
            float staminaDamage = (float) damage * 0.5f;
            if(staminaBar.GetStamina() > staminaDamage){
                staminaBar.SetStamina((int) staminaDamage);
            }else{
                staminaBar.SetStamina(2);
                animator.SetBool("isGuarding", false);
                currentHealth -= damage;
                healthBar.SetHealth(currentHealth);
            }
        }else{
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void LoseStam(int staminaToLose)
    {
        currentStam -= staminaToLose;
        staminaBar.SetStamina(currentStam);
    }

    public int GetStam(){
        return (int) currentStam;
    }

    public void Transform(InputAction.CallbackContext context)
    {
        if(isTransformable == true && isTransformed == false){
            if(context.performed)
            {
                animator.SetBool("isTransformed", true);
                currentHealth = maxHealth;
                staminaBar.SetStamina(maxStamina);
                isTransformed = true;
            }

            if(context.canceled){
            }
        }
    }

    IEnumerator RunContinuously(float startTime)
    {
        yield return new WaitForSeconds(startTime);

        while (true)
        {
            int secondsToWait = 1;
            float stamina = staminaBar.GetStamina();
            staminaBar.SetStamina((int)stamina + 4);
            if(isDefending==true){
                secondsToWait=4;
            }
            if(isTransformed == true){
                currentHealth += 1;
                if(currentHealth > maxHealth){
                    currentHealth = maxHealth;
                }
            }

            yield return new WaitForSeconds(secondsToWait);
        }
    }

}
