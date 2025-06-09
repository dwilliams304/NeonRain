using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Gun gun;
    public Sword sword;

    public ScriptableObject usableItems;
    public static Inventory Instance;
    public delegate void AddedPlayerGold(int amount, int total);
    public static AddedPlayerGold addGold;

    public int PlayerGold = 0;
    float GoldMod = 1f;

    void OnEnable(){
        WeaponSwapSystem.onGunSwap += SwapGuns;
        PlayerStatModifier.onStatChange += UpdateGoldMod;
    }
    void OnDisable(){
        WeaponSwapSystem.onGunSwap -= SwapGuns;
        PlayerStatModifier.onStatChange -= UpdateGoldMod;
    }

    void Awake(){
        Instance = this;
    }

    void UpdateGoldMod(){
        GoldMod = PlayerStatModifier.Instance.MOD_AdditionalGold;
    }

    public void AddGold(int amount){
        int amntAfterMod = Mathf.RoundToInt(amount * GoldMod);
        GameStats.goldEarned += amntAfterMod;
        PlayerGold += amntAfterMod;
        addGold?.Invoke(amntAfterMod, PlayerGold);
        
    }

    public void RemoveGold(int amount){
        PlayerGold -= amount;
        GameStats.goldSpent += amount;
        addGold?.Invoke(amount, PlayerGold);
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
