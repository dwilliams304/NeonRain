using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Player UI")]
    [Header("Player XP/Health")]
    [SerializeField] TMP_Text _playerLevel;
    [SerializeField] Slider _playerXP;
    [SerializeField] TMP_Text _playerXPText;
    [SerializeField] TMP_Text _playerLevelText;
    [Header("Right Side Player UI")]
    [SerializeField] TMP_Text _playerAmmo;
    [SerializeField] TMP_Text _goldAmount;
    [SerializeField] TMP_Text _ammoText;
    [SerializeField] GameObject _ammoTextObject;
    [SerializeField] GameObject _dashCoolDownObject;
    [SerializeField] GameObject _swingCooldownObject;
    [SerializeField] GameObject _reloadBarObject;
    
    [Header("Pop-Up Panels")]
    [SerializeField] private GameObject loseUIPanel;
    [SerializeField] private GameObject pauseGamePanel;
    [SerializeField] private GameObject weaponSwapPanel;

    bool isPaused = false;
    public bool gameLost = false;
    public bool upgradePanelActive = false;

    void Awake(){
        Instance = this;
    }


    void OnEnable(){
        KillTimer.timerCompleted += LoseGameUI;
        Inventory.addGold += UpdateGoldUI;
    }
    void OnDisable(){
        KillTimer.timerCompleted -= LoseGameUI;
        Inventory.addGold -= UpdateGoldUI;
    }



    void Start(){
        UpdateGoldUI(0);
    }

    void Update(){
        if(Input.GetButtonDown("PauseGame")){
            if(weaponSwapPanel.activeInHierarchy){
                return;
            }
            else{
                PauseGame();
            }
        }
    }



    public void UpdateXPBar(float toNextLevel, float currentXPAmnt, int level){
        _playerXP.maxValue = toNextLevel;
        _playerXP.value = currentXPAmnt;
        _playerLevelText.text = $"Lvl. {level}";
        _playerXPText.text = $"{_playerXP.value} / {_playerXP.maxValue}";
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
    public void SwingCoolDownBar(float swingCooldownSpeed){
        _swingCooldownObject.LeanScaleY(0.04f, 0);
        _swingCooldownObject.LeanScaleY(0, swingCooldownSpeed);
    }

    public void UpdateGoldUI(int total){
        _goldAmount.text = $"Gold: {total}";
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
            Time.timeScale = 1f;
        }
        pauseGamePanel.SetActive(isPaused);
    }


}
