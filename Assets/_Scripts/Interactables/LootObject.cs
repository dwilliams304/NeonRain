using System.Collections;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public Weapon weaponData;
    [SerializeField] private GameObject toolTip;
    // bool playerInRange = false;


    void Start(){
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            // playerInRange = true;
            toolTip.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        toolTip.SetActive(false);
    }
}
