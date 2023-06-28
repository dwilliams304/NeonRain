using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon weapon;
    public ScriptableObject usableItems;
    private Combat combat;
    void Start(){
        combat = GetComponent<Combat>();
    }



    public void SwapWeapon(Weapon swap){
        combat.AssignWeaponStats(swap);
        weapon = swap;
    }
}
