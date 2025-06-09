using System.Collections.Generic;
using UnityEngine;

public class InMeleeCollider : MonoBehaviour
{
    public List<HealthSystem> enemies = new List<HealthSystem>(); 
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.TryGetComponent<HealthSystem>(out HealthSystem h)){
            enemies.Add(h);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.TryGetComponent<HealthSystem>(out HealthSystem h)){
            enemies.Remove(h);
        }
    }
}
