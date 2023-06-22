using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManagement;
    [Header("Player UI")]
    [SerializeField] TMP_Text _playerLevel;
    [SerializeField] Slider _playerHealthBar;
    [SerializeField] Slider _playerXP;
    [SerializeField] TMP_Text _playerAmmo;
    [SerializeField] GameObject _reloadBar;


    [Header("Boss UI")]
    [SerializeField] TMP_Text _bossName;
    [SerializeField] Slider _bossHealthBar;


    [Header("Weapon ToolTip UI")]
    [SerializeField] private GameObject weaponToolTipPanel;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponDmg;
    [SerializeField] private TMP_Text weaponCritChance;
    [SerializeField] private TMP_Text weaponFireRate;
    [SerializeField] private TMP_Text weaponReloadSpeed;
    [SerializeField] private TMP_Text weaponMagSize;
    [SerializeField] private TMP_Text weaponType;
    [SerializeField] private TMP_Text weaponTier;
    [SerializeField] private Sprite weaponImg;

    [Header("Other UI")]
    [SerializeField] TMP_Text _corruptionTierText;
    [SerializeField] Image _corruptionTierIcon;


    [SerializeField] private PlayerStats _playerStats;

    void Awake(){
        uiManagement = this;
    }
    private void Start(){
        _playerStats = FindObjectOfType<PlayerStats>();
        _playerLevel.text = "Lv. " + _playerStats.CurrentLevel.ToString();
        _playerHealthBar.maxValue = _playerStats.PlayerMaxHealth;
        _playerHealthBar.value = _playerStats.PlayerMaxHealth;
    }

    public void UpdateHealthBar(float damage){

    }

    public void UpdateAmmo(int currentAmmo, int magSize){
        _playerAmmo.text = $"{currentAmmo} / {magSize}";
    }
    public void ReloadBar(float speed){
        _reloadBar.transform.localScale = new Vector3(1, 1, 1);
        _reloadBar.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0, 1, 1), speed);
    }

    public void ShowWeaponToolTip(Weapon weaponData){
        weaponToolTipPanel.SetActive(true);
        weaponName.text = weaponData.weaponName;
        weaponDmg.text = $"Damage: {weaponData.minDamage} - {weaponData.maxDamage}";
        weaponCritChance.text = $"Crit chance: {weaponData.critChance}%";
        weaponFireRate.text = $"Fire rate: {weaponData.fireRate}";
        weaponReloadSpeed.text = $"Reload speed: {weaponData.reloadSpeed}";
        weaponMagSize.text = $"Magazine size: {weaponData.magSize}";
        if(weaponData.rarity == Weapon.Rarity.common){
            weaponName.color = Color.white;
            weaponDmg.color = Color.white;
            weaponCritChance.color = Color.white;
            weaponFireRate.color = Color.white;
            weaponReloadSpeed.color = Color.white;
            weaponMagSize.color = Color.white;
        }else if(weaponData.rarity == Weapon.Rarity.uncommon){
            weaponName.color = Color.green;
            weaponDmg.color = Color.green;
            weaponCritChance.color = Color.green;
            weaponFireRate.color = Color.green;
            weaponReloadSpeed.color = Color.green;
            weaponMagSize.color = Color.green;
        }else if(weaponData.rarity == Weapon.Rarity.rare){
            weaponName.color = Color.blue;
            weaponDmg.color = Color.blue;
            weaponCritChance.color = Color.blue;
            weaponFireRate.color = Color.blue;
            weaponReloadSpeed.color = Color.blue;
            weaponMagSize.color = Color.blue;
        }else if(weaponData.rarity == Weapon.Rarity.corrupted){
            weaponName.color = Color.red;
            weaponDmg.color = Color.red;
            weaponCritChance.color = Color.red;
            weaponFireRate.color = Color.red;
            weaponReloadSpeed.color = Color.red;
            weaponMagSize.color = Color.red;
        }else if(weaponData.rarity == Weapon.Rarity.legendary){
            weaponName.color = Color.yellow;
            weaponDmg.color = Color.yellow;
            weaponCritChance.color = Color.yellow;
            weaponFireRate.color = Color.yellow;
            weaponReloadSpeed.color = Color.yellow;
            weaponMagSize.color = Color.yellow;
        }else{
            weaponName.color = Color.magenta;
            weaponDmg.color = Color.magenta;
            weaponCritChance.color = Color.magenta;
            weaponFireRate.color = Color.magenta;
            weaponReloadSpeed.color = Color.magenta;
            weaponMagSize.color = Color.magenta;
        }
        LeanTween.scaleX(weaponToolTipPanel, 1f, 0.15f);
        LeanTween.scaleY(weaponToolTipPanel, 1f, 0.15f);
    }

    public void HideWeaponToolTip(){
        LeanTween.scaleX(weaponToolTipPanel, 0f, .05f);
        LeanTween.scaleY(weaponToolTipPanel, 0f, .05f);
        //weaponToolTipPanel.SetActive(false);
    }
}
