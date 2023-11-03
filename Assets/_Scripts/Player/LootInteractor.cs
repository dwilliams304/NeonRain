using System.Collections.Generic;
using UnityEngine;

public class LootInteractor : MonoBehaviour
{    
    public delegate void GunSwapInitiated(List<Gun> guns, Gun currentGun);
    public static GunSwapInitiated gunSwapInitiated;

    Collider2D[] colliders2D;
    private List<Gun> possibleWeapons = new List<Gun>();
    private List<GameObject> objects = new List<GameObject>();

    void OnEnable(){
        WeaponSwapSystem.onGunSwap += RemoveGun;
    }
    void OnDisable(){
        WeaponSwapSystem.onGunSwap -= RemoveGun;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E) && !WeaponSwapSystem.Swapping){
            CheckForWeapons();
        }
    }

    void CheckForWeapons(){
        colliders2D = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach(Collider2D coll in colliders2D){
            if(coll.TryGetComponent(out LootObject gun)){
                possibleWeapons.Add(gun.weaponData);
                objects.Add(gun.gameObject);
            }
        }
        if(possibleWeapons.Count > 0) {
            gunSwapInitiated?.Invoke(possibleWeapons, Inventory.Instance.gun);
        }
    }

    void RemoveGun(Gun gun, int idx){
        Destroy(objects[idx]);
        possibleWeapons.Clear();
        objects.Clear();
    }
}
