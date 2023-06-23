using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootObject : MonoBehaviour
{
    public Weapon weaponData;
    private UIManager _uiMng;

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
        if(coll.CompareTag("Player")){
            _uiMng.ShowWeaponToolTip(weaponData);
        }
        //weaponToolTip.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D coll){
        if(Input.GetKey(KeyCode.E)){
            coll.TryGetComponent<Inventory>(out Inventory inventory);
            inventory.SwapWeapon(weaponData);
            StopCoroutine(DestroyThis());
            _uiMng.HideWeaponToolTip();
            Destroy(gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        if(coll.CompareTag("Player")){

            _uiMng.HideWeaponToolTip();
        }
        //weaponToolTip.SetActive(false);
    }
}
