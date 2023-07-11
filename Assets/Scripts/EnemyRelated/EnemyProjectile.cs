using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage;
    void OnCollisionEnter2D(Collision2D coll){
        gameObject.SetActive(false);
        if(coll.gameObject.TryGetComponent<PlayerStats>(out PlayerStats statsComponent)){
            statsComponent.TakeDamage(damage);
        }
        // Destroy(gameObject);
    }
}
