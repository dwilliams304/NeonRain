using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManagement;
    [SerializeField] Inventory inventory;
    [Header("Player UI")]
    [Header("Player XP/Health")]
    [SerializeField] TMP_Text _playerLevel;
    [SerializeField] Slider _playerHealthBar;
    [SerializeField] TMP_Text _playerHPText;
    [SerializeField] Slider _playerXP;
    [SerializeField] TMP_Text _playerXPText;
    [SerializeField] GameObject _playerXPIncreaseObject;
    [SerializeField] TMP_Text _playerLevelText;
    [Header("Right Side Player UI")]
    [SerializeField] TMP_Text _playerAmmo;
    [SerializeField] TMP_Text _goldAmount;
    [SerializeField] TMP_Text _ammoText;
    [SerializeField] GameObject _ammoTextObject;
    [SerializeField] GameObject _dashCoolDownObject;
    [SerializeField] GameObject _reloadBarObject;
    [Header("Consumables")]
    [SerializeField] TMP_Text _amntOfHealthPots;


    [Header("Boss UI")]
    [SerializeField] TMP_Text _bossName;
    [SerializeField] Slider _bossHealthBar;

#region Test Code
    // [Header("Weapon ToolTip UI")]
    // [SerializeField] private GameObject weaponToolTipPanel;
    // [SerializeField] private List<TMP_Text> weaponToolTipText;
    // [SerializeField] private TMP_Text weaponName; //0
    // [SerializeField] private TMP_Text weaponDmg; //1
    // [SerializeField] private TMP_Text weaponCritChance; //2
    // [SerializeField] private TMP_Text weaponFireRate; //3
    // [SerializeField] private TMP_Text weaponReloadSpeed; //4
    // [SerializeField] private TMP_Text weaponMagSize; //5
    // [SerializeField] private TMP_Text weaponType; //6
    // [SerializeField] private TMP_Text weaponTier; //7
    // [SerializeField] private Sprite weaponImg;
    // [Header("Weapon ToolTip UI -> Differences")]
    // [SerializeField] private TMP_Text dmgDiff;
    // [SerializeField] private TMP_Text critDiff;
    // [SerializeField] private TMP_Text fireRateDiff;
    // [SerializeField] private TMP_Text reloadSpeedDiff;
    // [SerializeField] private TMP_Text magSizeDiff;
#endregion
    
    [Header("Other UI")]
    [SerializeField] TMP_Text _corruptionTierText;
    [SerializeField] Image _corruptionTierIcon;
    
    [Header("Pop-Up Panels")]
    [SerializeField] private GameObject loseUIPanel;
    [SerializeField] private GameObject pauseGamePanel;
    [SerializeField] private GameObject abilityUpgradePanel;
    [SerializeField] private GameObject weaponSwapPanel;

    float currentTimeScale;
    bool isPaused = false;
    bool upgradePanelActive = false;

    private PlayerStats _playerStats;

    void Awake(){
        uiManagement = this;
    }


    void OnEnable(){
        HealthPotion.addHealth += UpdateHealthPotionUI;
        PlayerStats.onPlayerDeath += LoseGameUI;
    }
    void OnDisable(){
        HealthPotion.addHealth -= UpdateHealthPotionUI;
        PlayerStats.onPlayerDeath -= LoseGameUI;
    }



    void Start(){
        _playerStats = FindObjectOfType<PlayerStats>();
        //_playerLevel.text = "Lv. " + _playerStats.CurrentLevel.ToString();
        _playerHealthBar.maxValue = _playerStats.PlayerMaxHealth;
        _playerHealthBar.value = _playerStats.PlayerMaxHealth;
        UpdateHealthBar();
        UpdateGoldUI(0);
    }

    void Update(){
        if(Input.GetButtonDown("PauseGame")){
            if(weaponSwapPanel.activeInHierarchy){
                return;
            }
            PauseGame();
        }
        else if(Input.GetButtonDown("AbilityMenu")){
            AbilityUpgrades();
        }
    }

    public void UpdateHealthBar(){
        _playerHealthBar.maxValue = _playerStats.PlayerMaxHealth;
        _playerHealthBar.value = _playerStats.CurrentHealth;
        _playerHPText.text = $"{_playerHealthBar.value} / {_playerHealthBar.maxValue}";
    }
    
    public void UpdateXPBar(float toNextLevel, float currentXPAmnt, int level){
        _playerXP.maxValue = toNextLevel;
        _playerXP.value = currentXPAmnt;
        _playerLevelText.text = $"Lvl. {level}";
        _playerXPText.text = $"{_playerXP.value} / {_playerXP.maxValue}";
    }

    void UpdateHealthPotionUI(int healthAdded, int amountOfPotions){
        _amntOfHealthPots.text = $"x{amountOfPotions}";
        UpdateHealthBar();
    }

    public void UpdateAmmo(int currentAmmo, int magSize){
        _playerAmmo.text = $"{currentAmmo} / {magSize}";
        if(currentAmmo == 0){
            _ammoText.text = "No ammo!";
            _ammoText.color = Color.red;
        }else if(currentAmmo <= 7){
            _ammoText.text = "Low ammo!";
            _ammoText.color = Color.yellow;
            _ammoTextObject.SetActive(true);
        }else{
            _ammoTextObject.SetActive(false);
        }
    }

    public void DashCoolDownBar(float coolDownTime){
        _dashCoolDownObject.LeanScaleX(0.02f, 0);
        _dashCoolDownObject.LeanScaleX(0, coolDownTime);
    }
    public void ReloadBar(float reloadSpeed){
        _reloadBarObject.LeanScaleX(0.02f, 0);
        _reloadBarObject.LeanScaleX(0, reloadSpeed);
    }

    public void UpdateGoldUI(float playerGold){
        _goldAmount.text = $"Gold: {playerGold}";
    }

    void LoseGameUI(){
        loseUIPanel.SetActive(true);
    }


    void PauseGame(){
        isPaused = !isPaused;
        if(isPaused){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
        pauseGamePanel.SetActive(isPaused);
    }
    void AbilityUpgrades(){
        upgradePanelActive = !upgradePanelActive;
        if(upgradePanelActive){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
        abilityUpgradePanel.SetActive(upgradePanelActive);
    }


#region Test Code
    // public void ShowWeaponToolTip(Weapon weaponData){
    //     weaponToolTipPanel.SetActive(true);
    //     weaponName.text = weaponData.weaponName;
    //     weaponDmg.text = $"Damage: {weaponData.minDamage} - {weaponData.maxDamage}";
    //     weaponCritChance.text = $"Crit chance: {weaponData.critChance}%";
    //     weaponFireRate.text = $"Fire rate: {weaponData.fireRate}";
    //     weaponReloadSpeed.text = $"Reload speed: {weaponData.reloadSpeed}";
    //     weaponMagSize.text = $"Magazine size: {weaponData.magSize}";
    //     CheckDifferences(weaponData);
    //     if(weaponData.rarity == Weapon.Rarity.Common){
    //         ChangeTextColorInUI(Color.white);
    //     }else if(weaponData.rarity == Weapon.Rarity.Uncommon){
    //         ChangeTextColorInUI(Color.green);
    //     }else if(weaponData.rarity == Weapon.Rarity.Rare){
    //         ChangeTextColorInUI(Color.blue);
    //     }else if(weaponData.rarity == Weapon.Rarity.Corrupted){
    //         ChangeTextColorInUI(Color.red);
    //     }else if(weaponData.rarity == Weapon.Rarity.Legendary){
    //         ChangeTextColorInUI(Color.yellow);
    //     }
    //     LeanTween.scaleX(weaponToolTipPanel, 1f, 0.15f);
    //     LeanTween.scaleY(weaponToolTipPanel, 1f, 0.15f);
    // }


    // //WIll CHANGE!!!
    // //ALL VERY NASTY CODE!
    // //int : 0 -> Good(green) // 1 -> Neutral(yellow) // 2 -> Bad(red)
    // void CheckDifferences(Weapon w){
    //     Weapon wep = inventory.weapon;
    //     float dmgDifference = w.maxDamage - wep.maxDamage;
    //     float critDifference = w.critChance - wep.critChance;
    //     float fireRateDiff = w.fireRate - wep.fireRate;
    //     float reloadSpeedDiff = w.reloadSpeed - wep.reloadSpeed;
    //     int magSizeDifference = w.magSize - wep.magSize;
    //     //UI ELEMENTS
    //     if(dmgDifference > 0){
    //         DamDifferentUI(dmgDifference, Color.green, "+");
    //     }else if(dmgDifference == 0){
    //         DamDifferentUI(dmgDifference, Color.yellow, "");
    //    }
    //     else if(dmgDifference < 0){
    //         DamDifferentUI(dmgDifference, Color.red, "");
    //     }

    //     if(critDifference > 0){
    //         CritDifferentUI(critDifference, Color.green, "+");
    //     }else if(critDifference == 0){
    //         CritDifferentUI(critDifference, Color.yellow, "");
    //     }
    //     else if(critDifference < 0){
    //         CritDifferentUI(critDifference, Color.red, "");
    //     }

    //     if(fireRateDiff > 0){
    //         FireRateDifferentUI(fireRateDiff, Color.red, "+");
    //     }else if(fireRateDiff == 0){
    //         FireRateDifferentUI(fireRateDiff, Color.yellow, "");
    //     }
    //     else if(fireRateDiff < 0){
    //         FireRateDifferentUI(fireRateDiff, Color.green, "");
    //     }

    //     if(reloadSpeedDiff > 0){
    //         ReloadSpeedDifferentUI(reloadSpeedDiff, Color.red, "+");
    //     }else if(reloadSpeedDiff == 0){
    //         ReloadSpeedDifferentUI(reloadSpeedDiff, Color.yellow, "");
    //     }
    //     else if(reloadSpeedDiff < 0){
    //         ReloadSpeedDifferentUI(reloadSpeedDiff, Color.green, "");
    //     }

    //     if(magSizeDifference > 0){
    //         MagSizeDifferentUI(magSizeDifference, Color.green, "+");
    //     }else if(magSizeDifference == 0){
    //         MagSizeDifferentUI(magSizeDifference, Color.yellow, "");
    //     }
    //     else if(magSizeDifference < 0){
    //         MagSizeDifferentUI(magSizeDifference, Color.red, "");
    //     }
    // }
    // void DamDifferentUI(float value, Color color, string pm){
    //     dmgDiff.text = $"{pm}{value}";
    //     dmgDiff.color = color;
    // }
    // void CritDifferentUI(float value, Color color, string pm){
    //     critDiff.text = $"{pm}{value}";
    //     critDiff.color = color;
    // }
    // void FireRateDifferentUI(float value, Color color, string pm){
    //     fireRateDiff.text = $"{pm}{value}";
    //     fireRateDiff.color = color;
    // }
    // void ReloadSpeedDifferentUI(float value, Color color, string pm){
    //     reloadSpeedDiff.text = $"{pm}{value}";
    //     reloadSpeedDiff.color = color;
    // }
    // void MagSizeDifferentUI(float value, Color color, string pm){
    //     magSizeDiff.text = $"{pm}{value}";
    //     magSizeDiff.color = color;
    // }

    // void ChangeTextColorInUI(Color desiredColor){
    //     foreach(TMP_Text textComponenet in weaponToolTipText){
    //         textComponenet.color = desiredColor;
    //     }
    // }
    // public void HideWeaponToolTip(){
    //     LeanTween.scaleX(weaponToolTipPanel, 0f, .05f);
    //     LeanTween.scaleY(weaponToolTipPanel, 0f, .05f);
    //     //weaponToolTipPanel.SetActive(false);
    // }
#endregion
}
