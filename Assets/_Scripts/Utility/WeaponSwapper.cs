using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSwapper : MonoBehaviour
{

    public delegate void WeaponSwapComplete(int idx);
    public delegate void WeaponSwapCancelled();
    public static WeaponSwapCancelled wepSwapCancelled;
    public static WeaponSwapComplete wepSwapComplete;

    [Header("Scripts To Assign")]
    [SerializeField] private Inventory inventory;


    [Header("Weapon Swap Panel")]
    [SerializeField] GameObject panel;
    [SerializeField] GameObject blackOut;

    [Header("Info UI")]
    [SerializeField] TMP_Text amountOfWeapons;
    
    [Header("Current Weapon Object/UI")]
    [SerializeField] List<TMP_Text> currentWeaponText;
    [SerializeField] TMP_Text curWepName;
    [SerializeField] TMP_Text curWepType;
    [SerializeField] TMP_Text curWepDmg;
    [SerializeField] TMP_Text curWepFR;
    [SerializeField] TMP_Text curWepCritChance;
    [SerializeField] TMP_Text curWepRS;
    [SerializeField] TMP_Text curWepMag;
    [SerializeField] TMP_Text curWepTier;
    [SerializeField] TMP_Text curWepRarity;

    [Header("Other Weapon Object/UI")]
    [SerializeField] List<TMP_Text> otherWeaponText;
    [SerializeField] TMP_Text otherWepName;
    [SerializeField] TMP_Text otherWepType;
    [SerializeField] TMP_Text otherWepDmg;
    [SerializeField] TMP_Text otherWepFR;
    [SerializeField] TMP_Text otherWepCritChance;
    [SerializeField] TMP_Text otherWepRS;
    [SerializeField] TMP_Text otherWepMag;
    [SerializeField] TMP_Text otherWepTier;
    [SerializeField] TMP_Text otherWepRarity;
    

    float currentTimeScale;
    bool panelActive = false;

    [SerializeField] private List<Weapon> weapons;
    private int weaponCount;
    private int currentWeaponIndex = 0;


    void Start(){
        // inventory = Inventory.Instance;
    }
    void OnEnable(){
        LootInteractor.wepSwapInitiated += OpenPanel;
    }
    void OnDisable(){
        LootInteractor.wepSwapInitiated -= OpenPanel;
    }


    // void Update(){
    //     if(!panelActive){
    //         return;
    //     }
    //     else{
    //         if(Input.GetKeyDown(KeyCode.A)){
    //             ShowPreviousWeapon();
    //         }else if(Input.GetKeyDown(KeyCode.D)){
    //             ShowNextWeapon();
    //         }
    //         else if(Input.GetKeyDown(KeyCode.C)){
    //             Debug.Log("Swap weapons!");
    //             WeaponSwapConfirm();
    //         }else if(Input.GetKeyDown(KeyCode.Escape)){
    //             WeaponSwapCancel();
    //         }
    //         // else if(Input.GetKeyDown(KeyCode.Escape)){
    //         //     panelActive = false;
    //         //     panel.SetActive(false);
    //         // }
    //     }
    // }

    void OpenPanel(List<Weapon> wepList, int amnt){
        //UI Elements
        panelActive = true;
        panel.SetActive(true);
        blackOut.SetActive(true);
        weapons = wepList;
        currentWeaponIndex = 0;
        weaponCount = amnt;
        // currentWeaponObject = inventory.weapon;
        // otherWeaponObject = weapons[currentWeaponIndex];
        // UpdateUI(currentWeaponIndex, otherWeaponObject);
        Time.timeScale = 0f;
    }



    // void WeaponSwapConfirm(){
    //     wepSwapComplete?.Invoke(currentWeaponIndex);
    //     HideUI();
    // }

    // void WeaponSwapCancel(){
    //     wepSwapCancelled?.Invoke();
    //     HideUI();
    // }

    // void HideUI(){
    //     panelActive = false;
    //     panel.SetActive(false);
    //     blackOut.SetActive(false);
    //     weapons.Clear();
    //     currentWeaponIndex = 0;
    //     otherWeaponObject = null;
    //     Time.timeScale = 1f;
    // }

    // void UpdateUI(int idx, Weapon otherWep){
    //     amountOfWeapons.text = $"{idx + 1} out of {weapons.Count}";
    //     otherWepName.text = otherWeaponObject.weaponName;
    //     otherWepType.text = otherWeaponObject.weaponType.ToString();
    //     otherWepDmg.text = $"Damage: {otherWeaponObject.minDamage} - {otherWeaponObject.maxDamage}";
    //     otherWepFR.text = $"Fire Rate: {otherWeaponObject.fireRate}";
    //     otherWepCritChance.text = $"Crit Chance: {otherWeaponObject.critChance}";
    //     otherWepRS.text = $"Reload Speed: {otherWeaponObject.reloadSpeed}";
    //     otherWepMag.text = $"Mag Size: {otherWeaponObject.magSize}";
    //     otherWepTier.text = otherWeaponObject.currentWepTier.ToString();
    //     otherWepRarity.text = otherWeaponObject.rarity.ToString();

    //     curWepName.text = currentWeaponObject.weaponName;
    //     curWepType.text = currentWeaponObject.weaponType.ToString();
    //     curWepDmg.text = $"Damage: {currentWeaponObject.minDamage} - {currentWeaponObject.maxDamage}";
    //     curWepFR.text = $"Fire Rate: {currentWeaponObject.fireRate}";
    //     curWepCritChance.text = $"Crit Chance: {currentWeaponObject.critChance}";
    //     curWepRS.text = $"Reload Speed: {currentWeaponObject.reloadSpeed}";
    //     curWepMag.text = $"Mag Size: {currentWeaponObject.magSize}";
    //     curWepTier.text = currentWeaponObject.currentWepTier.ToString();
    //     curWepRarity.text = currentWeaponObject.rarity.ToString();

    //     switch(currentWeaponObject.rarity){
    //         case Rarity.Common:
    //             ChangeCurrentWeaponColors(Color.white);
    //             break;
    //         case Rarity.Uncommon:
    //             ChangeCurrentWeaponColors(Color.green);
    //             break;
    //         case Rarity.Rare:
    //             ChangeCurrentWeaponColors(Color.blue);
    //             break;
    //         case Rarity.Corrupted:
    //             ChangeCurrentWeaponColors(Color.red);
    //             break;
    //         case Rarity.Legendary:
    //             ChangeCurrentWeaponColors(Color.yellow);
    //             break;
    //     }

    //     switch (otherWeaponObject.rarity){
    //         case Rarity.Common:
    //             ChangeOtherWeaponColors(Color.white);
    //             break;
    //         case Rarity.Uncommon:
    //             ChangeOtherWeaponColors(Color.green);
    //             break;
    //         case Rarity.Rare:
    //             ChangeOtherWeaponColors(Color.blue);
    //             break;
    //         case Rarity.Corrupted:
    //             ChangeOtherWeaponColors(Color.red);
    //             break;
    //         case Rarity.Legendary:
    //             ChangeOtherWeaponColors(Color.yellow);
    //             break;
    //     }
    // }

    // void ChangeCurrentWeaponColors(Color desiredColor){
    //     foreach(TMP_Text text in currentWeaponText){
    //         text.color = desiredColor;
    //     }
    // }
    // void ChangeOtherWeaponColors(Color desiredColor){
    //     foreach(TMP_Text text in otherWeaponText){
    //         text.color = desiredColor;
    //     }
    // }


    // public void ShowNextWeapon(){
    //     if(currentWeaponIndex == weapons.Count - 1){
    //         currentWeaponIndex = 0;
    //     }else{
    //         currentWeaponIndex++;
    //     }
    //     otherWeaponObject = weapons[currentWeaponIndex];
    //     UpdateUI(currentWeaponIndex, otherWeaponObject);
    // }  
    // public void ShowPreviousWeapon(){
    //     if(currentWeaponIndex == 0){
    //         currentWeaponIndex = weapons.Count - 1;
    //         // currentWeaponIndex--;
    //     }else{
    //         currentWeaponIndex--;

    //     }
    //     otherWeaponObject = weapons[currentWeaponIndex];
    //     UpdateUI(currentWeaponIndex, otherWeaponObject);

    // }

}
