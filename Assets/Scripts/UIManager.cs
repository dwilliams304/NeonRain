using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] TMP_Text _playerLevel;
    [SerializeField] Slider _playerHealthBar;
    [SerializeField] Slider _playerXP;
    [SerializeField] TMP_Text _playerAmmo;


    [Header("Boss UI")]
    [SerializeField] TMP_Text _bossName;
    [SerializeField] Slider _bossHealthBar;


    [Header("Other UI")]
    [SerializeField] TMP_Text _corruptionTierText;
    [SerializeField] Image _corruptionTierIcon;


    [SerializeField] private PlayerStats _playerStats;

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
}
