using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public static Combat combat;
    [SerializeField] private MeleeWeaponData _meeleeWeapon;
    [SerializeField] private RangedWeaponData _rangedWeapon;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;

    public Inventory _inventory;

    private int _baseCritChance;
    private float _critDamageMod;

    private float _meleeSwingSpeed;
    private int _meleeSwingRange;
    private float _meleeWeaponDamage;
    private float _meleeCritChance;

    private float _rangedFireRate;
    private float _rangedReloadSpeed;
    private float _rangedProjectileSpeed;
    private float _rangedDamage;
    private int _rangedWeaponRange;
    private float _rangedCritChance;

    void Awake(){
        combat = this;
    }
    void Start(){
        _inventory = GetComponent<Inventory>();
        _baseCritChance = PlayerStats.playerStats.BaseCritChance;
        _critDamageMod = PlayerStats.playerStats.CritDamageMod;
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
        _meleeCritChance = meleeWeapon.critChance;

        _rangedFireRate = rangedWeapon.fireRate;
        _rangedReloadSpeed = rangedWeapon.reloadSpeed;
        _rangedProjectileSpeed = rangedWeapon.projectileSpeed;
        _rangedDamage = rangedWeapon.damage;
        _rangedWeaponRange = rangedWeapon.weaponRange;
        _rangedCritChance = rangedWeapon.critChance;
    }

    void CorruptedWeaponMod(int corruptionGain){

    }


    public float CalculateMeleeDamage(){
        int critRoll = Random.Range(0, 100);
        float damage = _meleeWeaponDamage;
        if(critRoll <= _baseCritChance + _meleeCritChance){
            damage *= _critDamageMod;
        }
        return damage;
    }

    public float CalculateRangedDamage(){
        int critRoll = Random.Range(0, 100);
        float damage = _rangedDamage;
        if(critRoll <= _baseCritChance + _rangedCritChance){
            Debug.Log("Crit!");
            damage *= _critDamageMod;
        }
        Debug.Log("Damge calc: " + damage);
        return damage;
    }

    public void Shoot(){
        GameObject bullet = BulletPooler.current.GetPooledBullet();
        if(bullet == null) return;
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * _rangedProjectileSpeed, ForceMode2D.Impulse);
    }
    
}
