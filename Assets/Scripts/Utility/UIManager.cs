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
    
    [Header("Other UI")]
    [SerializeField] TMP_Text _corruptionTierText;
    [SerializeField] Image _corruptionTierIcon;
    
    [Header("Pop-Up Panels")]
    [SerializeField] private GameObject loseUIPanel;
    [SerializeField] private GameObject pauseGamePanel;
    [SerializeField] private GameObject weaponSwapPanel;

    float currentTimeScale;
    bool isPaused = false;

    private PlayerStats _playerStats;

    void Awake(){
        uiManagement = this;
    }


    void OnEnable(){
        HealthPotion.addHealth += UpdateHealthPotionUI;
        PlayerStats.onPlayerDeath += LoseGameUI;
        KillTimer.timerCompleted += LoseGameUI;
        // PlayerStats.handleLevelIncrease += AbilityUpgrades;
    }
    void OnDisable(){
        HealthPotion.addHealth -= UpdateHealthPotionUI;
        PlayerStats.onPlayerDeath -= LoseGameUI;
        KillTimer.timerCompleted -= LoseGameUI;
        // PlayerStats.handleLevelIncrease -= AbilityUpgrades;

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
        Time.timeScale = 0f;
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


}
