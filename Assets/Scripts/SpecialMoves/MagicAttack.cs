using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{

    public int damageAmount = 20;
    public string firstPlayerTag = "Player1";
    public string otherPlayerTag = "Player2";
    public bool direction = true;
    public Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        if(direction){
            dir = Vector2.right;
        }else{
            dir = Vector2.left;
        }
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        print(dir);
        transform.Translate(dir * 1600f * Time.deltaTime);
    }

    // Alternatively, you can use OnTriggerEnter if you want to use triggers instead of collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision");
        // Check if the object entering the trigger has the PlayerHealth component
        PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.CompareTag(otherPlayerTag) || collision.gameObject.CompareTag(firstPlayerTag))
        {
            // Call the TakeDamage method on the player
            playerHealth.TakeDamage(damageAmount);
            print("damaged");
        }
    }
}
