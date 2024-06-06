using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformedMagicAttack : MonoBehaviour
{

    public int damageAmount = 25;
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
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        print(dir);
        transform.Translate(dir * 1900f * Time.deltaTime);
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
