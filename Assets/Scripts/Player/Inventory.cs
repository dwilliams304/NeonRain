using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon weapon;
    public ScriptableObject usableItems;
    private Combat combat;
    public static Inventory Instance;

    // public delegate void SwapWeapon(Weapon weapon);
    // public static SwapWeapon swapWeapon;

    void Start(){
        combat = GetComponent<Combat>();
    }

    public void SwapWeapon(Weapon swap){
        combat.AssignWeaponStats(swap);
        weapon = swap;
    }
}
