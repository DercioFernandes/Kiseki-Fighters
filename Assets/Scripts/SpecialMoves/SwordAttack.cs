using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public int damageAmount = 2;
    public string firstPlayerTag = "Player1";
    public string otherPlayerTag = "Player2";
    public string currentTag;
    public GameObject player; // Reference to the player transform
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
         transform.position = player.transform.position + offset;
    }

    // Alternatively, you can use OnTriggerEnter if you want to use triggers instead of collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision");
        // Check if the object entering the trigger has the PlayerHealth component
        PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.CompareTag(otherPlayerTag) || collision.gameObject.CompareTag(firstPlayerTag) && collision.gameObject.tag != currentTag)
        {
            // Call the TakeDamage method on the player
            playerHealth.TakeDamage(damageAmount);
            //print("damaged");
        }
    }
}
