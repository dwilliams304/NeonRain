using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Player UI")]
    [SerializeField] TMP_Text _playerAmmo;
    [SerializeField] TMP_Text _ammoText;
    [SerializeField] GameObject _ammoTextObject;
    [SerializeField] GameObject _dashCoolDownObject;
    [SerializeField] GameObject _swingCooldownObject;
    [SerializeField] GameObject _reloadBarObject;
    
    [Header("Pop-Up Panels")]
    [SerializeField] private GameObject loseUIPanel;

    public bool gameLost = false;
    public bool upgradePanelActive = false;

    void Awake(){
        Instance = this;
    }


    void OnEnable(){
        KillTimer.timerCompleted += LoseGameUI;
    }
    void OnDisable(){
        KillTimer.timerCompleted -= LoseGameUI;
    }


    public void UpdateAmmo(int currentAmmo, int magSize){
        _playerAmmo.text = $"{currentAmmo}/{magSize}";
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

    void LoseGameUI(){
        loseUIPanel.SetActive(true);
        Time.timeScale = 0f;
    }



}
