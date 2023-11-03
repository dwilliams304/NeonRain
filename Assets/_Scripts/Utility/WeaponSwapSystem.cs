using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponSwapSystem : MonoBehaviour
{
    public delegate void OnGunSwap(Gun gun, int idx);
    public delegate void OnSwordSwap(Sword sword);
    public static OnGunSwap onGunSwap;
    public static OnSwordSwap onSwordSwap;

    public static bool Swapping = false;
    private float _previousTimeScale = 1f;


    [SerializeField] private GameObject _wepSwapPanel;
    
    [SerializeField] private TMP_Text _totalText;
    [SerializeField] private List<TMP_Text> _currentWeaponInfo;
    [SerializeField] private List<TMP_Text> _otherWeaponInfo;

    [SerializeField] private Image _currentIcon;
    [SerializeField] private Image _otherIcon;

    private List<Gun> _guns = new List<Gun>();
    private int _cur = 0;
    private Gun _currentlyViewedGun;
    private Gun _currentlyEquippedGun;

    [Header("Rarity Colors")]
    [SerializeField] private Color _commonColor;
    [SerializeField] private Color _unCommonColor;
    [SerializeField] private Color _rareColor;
    [SerializeField] private Color _corruptedColor;
    [SerializeField] private Color _legendaryColor;
    [SerializeField] private Color _uniqueColor;
    

    void OnEnable(){
        LootInteractor.gunSwapInitiated += ShowPanel;
    }
    void OnDisable(){
        LootInteractor.gunSwapInitiated -= ShowPanel;
    }

    void Update(){
        if(!Swapping) {
            return;
        }
        else{
            if(Input.GetKeyDown(KeyCode.A)){
                ScrollLeft();
            }
            else if(Input.GetKeyDown(KeyCode.D)){
                ScrollRight();
            }
            else if(Input.GetKeyDown(KeyCode.C)){
                Confirm();
            }
            else if(Input.GetKeyDown(KeyCode.Escape)){
                ClosePanel();
            }
        }
    }

    void ShowPanel(List<Gun> possibleGuns, Gun currentGun){
        _cur = 0;
        Swapping = true;
        _previousTimeScale = Time.timeScale;
        _guns = possibleGuns;
        _currentlyEquippedGun = currentGun;
        _currentlyViewedGun = _guns[_cur];
        _wepSwapPanel.SetActive(true);
        Time.timeScale = 0;
        UpdateEquippedGunText();
        UpdateViewedGunText();
    }

    void ScrollLeft(){
        if(_cur < 1){
            _cur = _guns.Count - 1;
        }
        else _cur--;
        UpdateViewedGun();
    }

    void ScrollRight(){
        if(_cur == _guns.Count - 1){
            _cur = 0;
        }
        else _cur++;
        UpdateViewedGun();
    }

    void Confirm(){
        onGunSwap?.Invoke(_currentlyViewedGun, _cur);
        ClosePanel();

    }

    void ClosePanel(){
        Swapping = false;
        _wepSwapPanel.SetActive(false);
        _guns.Clear();
        Time.timeScale = _previousTimeScale;
    }

    void UpdateViewedGun(){
        _currentlyViewedGun = _guns[_cur];
        UpdateViewedGunText();
    }


    //0 = name
    //1 = type
    //2 = damage
    //3 = crit chance
    //4 = fire rate
    //5 = reload speed
    //6 = mag size

    void UpdateEquippedGunText(){
        _currentIcon.sprite = _currentlyEquippedGun.weaponSprite;
        _currentWeaponInfo[0].text = _currentlyEquippedGun.weaponName;
        _currentWeaponInfo[1].text = _currentlyEquippedGun.gunType.ToString();
        _currentWeaponInfo[2].text = _currentlyEquippedGun.minDamage.ToString() + " - " + _currentlyEquippedGun.maxDamage.ToString();
        _currentWeaponInfo[3].text = _currentlyEquippedGun.critChance.ToString() + "%";
        _currentWeaponInfo[4].text = _currentlyEquippedGun.fireRate.ToString() + "s";
        _currentWeaponInfo[5].text = _currentlyEquippedGun.reloadSpeed.ToString() + "s";
        _currentWeaponInfo[6].text = _currentlyEquippedGun.magSize.ToString();
        UpdateUIColors(_currentWeaponInfo[0], _currentIcon, _currentlyEquippedGun.rarity);
    }

    void UpdateViewedGunText(){
        UpdateTotalText();
        _otherIcon.sprite = _currentlyViewedGun.weaponSprite;
        _otherWeaponInfo[0].text = _currentlyViewedGun.weaponName;
        _otherWeaponInfo[1].text = _currentlyViewedGun.gunType.ToString();
        _otherWeaponInfo[2].text = _currentlyViewedGun.minDamage.ToString() + " - " + _currentlyViewedGun.maxDamage.ToString();
        _otherWeaponInfo[3].text = _currentlyViewedGun.critChance.ToString() + "%";
        _otherWeaponInfo[4].text = _currentlyViewedGun.fireRate.ToString() + "s";
        _otherWeaponInfo[5].text = _currentlyViewedGun.reloadSpeed.ToString() + "s";
        _otherWeaponInfo[6].text = _currentlyViewedGun.magSize.ToString();
        UpdateUIColors(_otherWeaponInfo[0], _otherIcon, _currentlyViewedGun.rarity);
    }

    void UpdateTotalText(){
        _totalText.text = (_cur + 1).ToString() + " of " + _guns.Count.ToString();
    }

    void UpdateUIColors(TMP_Text gunName, Image gunSprite, Rarity rarity){
        switch(rarity){
            case Rarity.Common:
                gunName.color = _commonColor;
                gunSprite.color = _commonColor;
                break;
            case Rarity.Uncommon:
                gunName.color = _unCommonColor;
                gunSprite.color = _unCommonColor;
                break;
            case Rarity.Rare:
                gunName.color = _rareColor;
                gunSprite.color = _rareColor; 
                break;
            case Rarity.Corrupted:
                gunName.color = _corruptedColor;
                gunSprite.color = _corruptedColor;
                break;
            case Rarity.Legendary:
                gunName.color = _legendaryColor;
                gunSprite.color = _legendaryColor;
                break;
            case Rarity.Unique:
                gunName.color = _uniqueColor;
                gunSprite.color = _uniqueColor;
                break;
        }
    }
}
