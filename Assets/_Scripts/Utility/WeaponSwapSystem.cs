using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponSwapSystem : MonoBehaviour
{
    public delegate void OnGunSwap(Gun gun);
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

    [SerializeField] private List<GameObject> lootObjs = new List<GameObject>();
    [SerializeField] private List<Gun> _guns = new List<Gun>();
    private int _cur = 0;
    private Gun _currentlyViewedGun;
    private Gun _currentlyEquippedGun;
    

    void OnEnable(){
        Interactor.gunSwapInitiated += ShowPanel;
    }
    void OnDisable(){
        Interactor.gunSwapInitiated -= ShowPanel;
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

    void ShowPanel(List<GameObject> possibleGuns, Gun currentGun){
        Swapping = true;
        _previousTimeScale = Time.timeScale;
        lootObjs = possibleGuns;
        foreach(GameObject obj in possibleGuns){
            if(obj.TryGetComponent<LootObject>(out LootObject gun)){
                _guns.Add(gun.weaponData);
            }
        }
        _currentlyEquippedGun = currentGun;
        _currentlyViewedGun = _guns[_cur];
        _wepSwapPanel.SetActive(true);
        Time.timeScale = 0;
        UpdateEquippedGunText();
        UpdateViewedGunText();
    }

    void ClosePanel(){
        Swapping = false;
        _cur = 0;
        _wepSwapPanel.SetActive(false);
        _guns.Clear();
        lootObjs.Clear();
        Time.timeScale = _previousTimeScale;
    }

    public void ScrollLeft(){
        if(_cur < 1)  _cur = _guns.Count - 1;
        else _cur--;
        UpdateViewedGun();
    }

    public void ScrollRight(){
        if(_cur == _guns.Count - 1) _cur = 0;
        else _cur++;
        UpdateViewedGun();
    }

    void Confirm(){
        onGunSwap?.Invoke(_currentlyViewedGun);
        Destroy(lootObjs[_cur]);
        ClosePanel();
    }

    void UpdateViewedGun(){
        _currentlyViewedGun = _guns[_cur];
        UpdateViewedGunText();
    }

    void UpdateEquippedGunText(){
        _currentIcon.sprite = _currentlyEquippedGun.weaponSprite;
        _currentIcon.color = _currentlyEquippedGun.color;
        UpdateWeaponUI(_currentWeaponInfo, _currentlyEquippedGun);
    }

    void UpdateViewedGunText(){
        UpdateTotalText();
        _otherIcon.sprite = _currentlyViewedGun.weaponSprite;
        _otherIcon.color = _currentlyViewedGun.color;
        UpdateWeaponUI(_otherWeaponInfo, _currentlyViewedGun);
    }

    void UpdateTotalText(){
        _totalText.text = (_cur + 1).ToString() + " of " + _guns.Count.ToString();
    }

    //0 = name
    //1 = type
    //2 = damage
    //3 = crit chance
    //4 = fire rate
    //5 = reload speed
    //6 = mag size
    void UpdateWeaponUI(List<TMP_Text> textElems, Gun gun){
        textElems[0].color = gun.color;
        textElems[0].text = gun.weaponName;
        textElems[1].text = gun.gunType.ToString();
        textElems[2].text = gun.minDamage.ToString() + " - " + gun.maxDamage.ToString();
        textElems[3].text = gun.critChance.ToString() + "%";
        textElems[4].text = gun.fireRate.ToString() + "s";
        textElems[5].text = gun.reloadSpeed.ToString() + "s";
        textElems[6].text = gun.magSize.ToString();
    }
}
