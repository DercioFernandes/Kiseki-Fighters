using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public StaminaBar staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        StartCoroutine(RunContinuously(3));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
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
            yield return new WaitForSeconds(2);
        }
    }

}
