using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformedSwordAttack : MonoBehaviour
{

    public int damageAmount = 15;
    public string firstPlayerTag = "Player1";
    public string otherPlayerTag = "Player2";
    public bool direction = true;
    public Vector2 dir;
    public string currentTag;

    void Start()
    {
        if(direction){
            dir = Vector2.right;
        }else{
            dir = Vector2.left;
            Vector3 originalScale = transform.localScale;
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(dir * 3000f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
        if ((collision.gameObject.CompareTag(otherPlayerTag) || collision.gameObject.CompareTag(firstPlayerTag)) && collision.gameObject.tag != currentTag)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
