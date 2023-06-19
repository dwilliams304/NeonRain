using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private MeleeWeaponData _meeleeWeapon;
    [SerializeField] private RangedWeaponData _rangedWeapon;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;

    public Inventory _inventory;


    private float _meleeSwingSpeed;
    private float _meleeSwingRange;
    private float _meleeWeaponDamage;

    private float _rangedFireRate;
    private float _rangedReloadSpeed;
    private float _rangedProjectileSpeed;
    private float _rangedDamage;
    private int _rangedWeaponRange;


    void Start(){
        _inventory = GetComponent<Inventory>();
        _meeleeWeapon = _inventory.meleeWeapon;
        _rangedWeapon = _inventory.rangedWeapon;
        AssingWeaponStats(_meeleeWeapon, _rangedWeapon);
        if(_meeleeWeapon.isCorrupted){
            CorruptedWeaponMod(_meeleeWeapon.corruptionGain);
        }
        else if(_rangedWeapon.isCorrupted){
            CorruptedWeaponMod(_rangedWeapon.corruptionGain);
        }
    }


    void AssingWeaponStats(MeleeWeaponData meleeWeapon, RangedWeaponData rangedWeapon){
        _meleeSwingSpeed = meleeWeapon.swingSpeed;
        _meleeSwingRange = meleeWeapon.swingRange;
        _meleeWeaponDamage = meleeWeapon.damage;

        _rangedFireRate = rangedWeapon.fireRate;
        _rangedReloadSpeed = rangedWeapon.reloadSpeed;
        _rangedProjectileSpeed = rangedWeapon.projectileSpeed;
        _rangedDamage = rangedWeapon.damage;
        _rangedWeaponRange = rangedWeapon.weaponRange;
    }

    void CorruptedWeaponMod(int corruptionGain){

    }


    public void Shoot(){
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * _rangedProjectileSpeed, ForceMode2D.Impulse);
    }
    
}
