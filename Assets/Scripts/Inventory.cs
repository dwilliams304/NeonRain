using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon weapon;
    public ScriptableObject usableItems;


    public void SwapWeapon(Weapon swap){
        weapon = swap;
        Combat.combat.AssignWeaponStats(swap);
    }
}
