using System.Collections.Generic;
using UnityEngine;

public class LootInteractor : MonoBehaviour
{    
    [SerializeField] private List<Weapon> possibleWeapons;
    private int possibleWeaponCount = 0;

    public delegate void WeaponSwapInitiated(List<Weapon> weapons, int weaponCount);
    public static WeaponSwapInitiated wepSwapInitiated;
    bool lootChecked = false;

    void OnEnable(){
        WeaponSwapper.wepSwapComplete += Done;
    }
    void OnDisable(){
        WeaponSwapper.wepSwapComplete -= Done;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(!lootChecked){
                CheckForWeapons();
            }
        }
    }

    void Done(){
        possibleWeaponCount = 0;
        possibleWeapons.Clear();
        lootChecked = false;
    }
    void CheckForWeapons(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach(Collider2D coll in colliders){
            if(coll.CompareTag("Loot")){
                lootChecked = true;
                coll.TryGetComponent(out LootObject wep);
                Destroy(wep.gameObject); //Change to set active when pooling (if implemented)
                possibleWeapons.Add(wep.weaponData);
                //wepSwapper.ShowUI(possibleWeapons, possibleWeaponCount);
                wepSwapInitiated?.Invoke(possibleWeapons, possibleWeaponCount);
            }
        }
    }
}
