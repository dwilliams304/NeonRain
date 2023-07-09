using System.Collections.Generic;
using UnityEngine;

public class LootInteractor : MonoBehaviour
{    
    private int possibleWeaponCount = 0;

    public delegate void WeaponSwapInitiated(List<Weapon> weapons, int weaponCount);
    public static WeaponSwapInitiated wepSwapInitiated;
    bool lootChecked = false;

    Collider2D[] colliders2D;
    private List<Weapon> possibleWeapons = new List<Weapon>();
    private List<LootObject> lootObjs = new List<LootObject>();

    void OnEnable(){
        WeaponSwapper.wepSwapComplete += Confirm;
        WeaponSwapper.wepSwapCancelled += Cancel;
    }
    void OnDisable(){
        WeaponSwapper.wepSwapComplete -= Confirm;
        WeaponSwapper.wepSwapCancelled -= Cancel;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(!lootChecked){
                CheckForWeapons();
            }
        }
    }

    void Confirm(int idx){
        LootObject o = lootObjs[idx];
        lootObjs.RemoveAt(idx);
        Destroy(o.gameObject);
        ClearLootAndWeaponList();
        lootChecked = false;
    }
    void Cancel(){
        ClearLootAndWeaponList();
        lootChecked = false;
    }

    void CheckForWeapons(){
        colliders2D = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach(Collider2D coll in colliders2D){
            if(coll.CompareTag("Loot")){
                lootChecked = true;
                coll.TryGetComponent(out LootObject wep);
                AddLootToList(wep);
                AddToListOfWeapons(wep.weaponData);
                wepSwapInitiated?.Invoke(possibleWeapons, possibleWeaponCount);
            }
        }
    }

    void AddLootToList(LootObject obj){
        lootObjs.Add(obj);
    }
    void AddToListOfWeapons(Weapon wep){
        possibleWeapons.Add(wep);
    }

    void ClearLootAndWeaponList(){
        possibleWeaponCount = 0;
        lootObjs.Clear();
        possibleWeapons.Clear();
    }
}
