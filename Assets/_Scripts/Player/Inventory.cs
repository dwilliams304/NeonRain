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

    void OnEnable(){
        WeaponSwapSystem.onGunSwap += SwapGuns;
    }
    void OnDisable(){
        WeaponSwapSystem.onGunSwap -= SwapGuns;
    }

    void Awake(){
        Instance = this;
    }
    void Start(){
        combat = GetComponent<Combat>();
    }

    public void AddGold(int amount){
        PlayerGold += Mathf.RoundToInt(amount * GoldModifier);
        addGold?.Invoke(amount);
        
    }

    public void RemoveGold(int amount){
        PlayerGold -= amount;
        addGold?.Invoke(amount);
    }

    void SwapGuns(Gun newGun){
        CorruptionTicker.Instance.AddToTickAmount(-gun.corruptionGain); //Remove whatever the previous gun had added
        if(newGun.corruptionGain > 0){
            CorruptionTicker.Instance.AddToTickAmount(newGun.corruptionGain); //If they have corruption, add it 
        }
        gun = newGun;
    }

    // void SwapSwords(Sword swap){
    //     combat.AssignMeleeStats(swap);
    //     sword = swap;
    // }
}
