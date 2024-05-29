using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 100;
    public int maxStamina = 20;
    public int currentHealth;
    public int currentStam;
    public HealthBar healthBar;
    public StaminaBar staminaBar;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunContinuously(3));
        //print("current stamina bar is: " + staminaBar.name);
    }

    // Update is called once per frame
    void Update()
    {
        currentStam = (int) staminaBar.GetStamina();
        print("updated stamina bar is: " + staminaBar.name);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print("current health is:" + currentHealth);
        healthBar.SetHealth(currentHealth);
        print("current health bar is: " + healthBar.name);
    }

    public void LoseStam(int staminaToLose)
    {
        
        print("current stamina bar is: " + staminaBar.name);
        currentStam -= staminaToLose;
        print("current stamina is:" + currentStam);
        staminaBar.SetStamina(currentStam);
    }

    public int GetStam(){
        return (int) currentStam;
    }

    IEnumerator RunContinuously(float startTime)
    {
        // Wait for the initial start time
        yield return new WaitForSeconds(startTime);

        // Loop indefinitely
        while (true)
        {
            // Your repeated action goes here
            float stamina = staminaBar.GetStamina();
            staminaBar.SetStamina((int)stamina + 1);

            // Yield control back to Unity and wait for the next frame before continuing the loop
            yield return new WaitForSeconds(1);
        }
    }

}
