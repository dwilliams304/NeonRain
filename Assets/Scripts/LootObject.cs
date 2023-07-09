using System.Collections;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    public Weapon weaponData;
    private UIManager _uiMng;

    bool playerInRange = false;

    void Start(){
        StartCoroutine(DestroyThis());
        _uiMng = UIManager.uiManagement;
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(10f);
        _uiMng.HideWeaponToolTip();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll){
        playerInRange = true;
        if(coll.CompareTag("Player") && playerInRange){
            _uiMng.ShowWeaponToolTip(weaponData);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        playerInRange = false;
        if(!playerInRange){
            _uiMng.HideWeaponToolTip();
        }
    }
}
