using System.Collections.Generic;
using UnityEngine;

public class LootInteractor : MonoBehaviour
{    
    public delegate void GunSwapInitiated(List<Gun> guns, Gun currentGun);
    public static GunSwapInitiated gunSwapInitiated;

    Collider2D[] colliders2D;
    private List<Gun> possibleWeapons = new List<Gun>();

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            CheckForWeapons();
        }
    }

    void CheckForWeapons(){
        colliders2D = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach(Collider2D coll in colliders2D){
            if(coll.TryGetComponent(out LootObject gun)){
                possibleWeapons.Add(gun.weaponData);
            }
        }
        if(possibleWeapons.Count > 0) {
            gunSwapInitiated?.Invoke(possibleWeapons, Inventory.Instance.gun);
        }
    }
}
