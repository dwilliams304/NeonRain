using UnityEngine;
using UnityEngine.UI;

public class SwapGunIcon : MonoBehaviour
{
    private Image _icon;

    void Start() => _icon = GetComponent<Image>();

    void OnEnable() => WeaponSwapSystem.onGunSwap += SwapIcons;
    void OnDisable() => WeaponSwapSystem.onGunSwap -= SwapIcons;

    void SwapIcons(Gun gun) {
        _icon.sprite = gun.weaponSprite;
        _icon.color = gun.color;
    }

}