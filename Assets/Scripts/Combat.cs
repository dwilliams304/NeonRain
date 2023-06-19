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
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private LayerMask _enemyLayers;

    public Inventory _inventory;

    private int _baseCritChance;
    private float _critDamageMod;

    private float _meleeSwingSpeed;
    private int _meleeSwingRange;
    private float _meleeMinDmg;
    private float _meleeMaxDmg;
    private float _meleeCritChance;

    private float _rangedFireRate;
    private float _rangedReloadSpeed;
    private float _rangedProjectileSpeed;
    private float _rangedMinDmg;
    private float _rangedMaxDmg;
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
        _meleeMinDmg = meleeWeapon.minDamage;
        _meleeMaxDmg = meleeWeapon.maxDamage;
        _meleeCritChance = meleeWeapon.critChance;

        _rangedFireRate = rangedWeapon.fireRate;
        _rangedReloadSpeed = rangedWeapon.reloadSpeed;
        _rangedProjectileSpeed = rangedWeapon.projectileSpeed;
        _rangedMinDmg = rangedWeapon.minDamage;
        _rangedMaxDmg = rangedWeapon.maxDamage;
        _rangedWeaponRange = rangedWeapon.weaponRange;
        _rangedCritChance = rangedWeapon.critChance;
    }

    void CorruptedWeaponMod(int corruptionGain){

    }




    public void Shoot(){
        GameObject bullet = BulletPooler.current.GetPooledBullet();
        if(bullet == null) return;
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * _rangedProjectileSpeed, ForceMode2D.Impulse);
    }
    
    public void MeleeAttack(){
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_meleeAttackPoint.position, _meleeSwingRange, _enemyLayers);
        foreach(Collider2D enemy in enemiesHit){
            Debug.Log("Enemy meleed");
            enemy.GetComponent<Enemy>().ReceiveDamage(CalculateMeleeDamage());
            
        }
        
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(_meleeAttackPoint.position, _meleeSwingRange);
        
    }

    public float CalculateMeleeDamage(){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = Random.Range(_meleeMinDmg, _meleeMaxDmg);
        if(critRoll <= _baseCritChance + _meleeCritChance){
            float returnVal = Mathf.Ceil(dmgRoll *= _critDamageMod);
            Debug.Log("Crit! Dmg calc: " + returnVal);
            return returnVal;
        }
        Debug.Log("No crit! Dmg calc: " + Mathf.Ceil(dmgRoll));
        return Mathf.Ceil(dmgRoll);
    }

    public float CalculateRangedDamage(){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = Random.Range(_rangedMinDmg, _rangedMaxDmg);
        if(critRoll <= _baseCritChance + _rangedCritChance){
            float returnVal = Mathf.Ceil(dmgRoll *= _critDamageMod);
            Debug.Log("Crit! Dmg calc: " + returnVal);
            return returnVal;
        }
        Debug.Log("No crit! Dmg calc: " + Mathf.Ceil(dmgRoll));
        return Mathf.Ceil(dmgRoll);
    }
}
