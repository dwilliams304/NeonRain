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
    // void OnEnable(){
    //     swapWeapon += DoWeaponSwap;
    // }
    // void OnDisable(){
    //     swapWeapon = DoWeaponSwap;
    // }



    public void SwapWeapon(Weapon swap){
        combat.AssignWeaponStats(swap);
        weapon = swap;
    }
}
