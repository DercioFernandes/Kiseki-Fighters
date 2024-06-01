using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAttack : MonoBehaviour
{

    public int damageAmount = 4;
    public string otherPlayerTag = "Player2";

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Alternatively, you can use OnTriggerEnter if you want to use triggers instead of collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision");
        // Check if the object entering the trigger has the PlayerHealth component
        PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.CompareTag(otherPlayerTag))
        {
            // Call the TakeDamage method on the player
            playerHealth.TakeDamage(damageAmount);
            print("damaged");
        }
    }
}
