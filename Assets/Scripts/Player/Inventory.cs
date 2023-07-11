using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon weapon;
    public ScriptableObject usableItems;
    private Combat combat;
    public static Inventory Instance;

    public int PlayerGold = 0;

    // public delegate void SwapWeapon(Weapon weapon);
    // public static SwapWeapon swapWeapon;

    void Start(){
        combat = GetComponent<Combat>();
    }

    void AddGold(int amount){
        PlayerGold += amount;
    }

    public void SwapWeapon(Weapon swap){
        combat.AssignWeaponStats(swap);
        weapon = swap;
    }
}
