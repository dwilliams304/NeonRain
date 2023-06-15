using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // private float lifeTime = 5f;
    
    
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(this.gameObject);
    }

    
}
