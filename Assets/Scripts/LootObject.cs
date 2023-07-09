using System.Collections;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public Weapon weaponData;

    bool playerInRange = false;

    void Start(){
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll){
    }

    void OnTriggerExit2D(Collider2D coll){
    }
}
