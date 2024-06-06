using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public int damageAmount = 2;
    public string firstPlayerTag = "Player1";
    public string otherPlayerTag = "Player2";
    public string currentTag;
    public GameObject player; 
    public Vector3 offset;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
         transform.position = player.transform.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.CompareTag(otherPlayerTag) || collision.gameObject.CompareTag(firstPlayerTag) && collision.gameObject.tag != currentTag)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
