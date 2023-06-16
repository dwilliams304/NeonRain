using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // private float lifeTime = 5f;
    
    
    void OnTriggerEnter2D(Collider2D collision){

        if(collision.CompareTag("Enemy")){
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.ReceiveDamage(2);
            Destroy(this.gameObject);
        }
    }

    
}
