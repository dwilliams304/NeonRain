using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Gun gun;
    public Sword sword;

    public ScriptableObject usableItems;
    private Combat combat;
    public static Inventory Instance;
    public delegate void AddedPlayerGold(int amount);
    public static AddedPlayerGold addGold;

    public int PlayerGold = 0;
    public float GoldModifier = 1f;

    // public delegate void SwapWeapon(Weapon weapon);
    // public static SwapWeapon swapWeapon;
    void Awake(){
        Instance = this;
    }
    void Start(){
        combat = GetComponent<Combat>();
    }

    public void AddGold(int amount){
        PlayerGold += Mathf.RoundToInt(amount * GoldModifier);
        addGold?.Invoke(PlayerGold);
        
    }

    public void RemoveGold(int amount){
        PlayerGold -= amount;
        addGold?.Invoke(PlayerGold);
    }

    public void SwapGuns(Gun swap){
        combat.AssignRangedStats(swap);
        gun = swap;
    }

    public void SwapSwords(Sword swap){
        combat.AssignMeleeStats(swap);
        sword = swap;
    }
}