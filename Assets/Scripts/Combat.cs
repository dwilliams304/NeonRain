using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public static Combat combat;
    private float lastShot;

    #region Other variables
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private MeleeWeaponData _meeleeWeapon;
    [SerializeField] private RangedWeaponData _rangedWeapon;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private LayerMask _enemyLayers;
    #endregion

    #region Melee weapon variables
    private float _meleeSwingSpeed;
    private int _meleeSwingRange;
    private float _meleeMinDmg;
    private float _meleeMaxDmg;
    private float _meleeCritChance;
    #endregion

    #region Ranged weapon variables
    private float _rangedFireRate;
    private float _rangedReloadSpeed;
    private int _magSize;
    private int _currentAmmo;
    private float _rangedProjectileSpeed;
    private float _rangedMinDmg;
    private float _rangedMaxDmg;
    private int _rangedWeaponRange;
    private float _rangedCritChance;
    #endregion

    

    void Awake(){
        combat = this;
    }
    
    
    void Start(){
        _inventory = GetComponent<Inventory>();
        _playerController = GetComponent<PlayerController>();
        _playerStats = PlayerStats.playerStats;
        _meeleeWeapon = _inventory.meleeWeapon;
        _rangedWeapon = _inventory.rangedWeapon;
        AssignMeleeStats(_meeleeWeapon);
        AssingRangedStats(_rangedWeapon);
        if(_meeleeWeapon.isCorrupted){
            CorruptedWeaponMod(_meeleeWeapon.corruptionGain);
        }
        else if(_rangedWeapon.isCorrupted){
            CorruptedWeaponMod(_rangedWeapon.corruptionGain);
        }
    }

    //Assign all the melee weapon data
    void AssignMeleeStats(MeleeWeaponData meleeWeapon){
        _meleeSwingSpeed = meleeWeapon.swingSpeed;
        _meleeSwingRange = meleeWeapon.swingRange;
        _meleeMinDmg = meleeWeapon.minDamage;
        _meleeMaxDmg = meleeWeapon.maxDamage;
        _meleeCritChance = meleeWeapon.critChance;
    }

    //Assign all the ranged weapon data
    void AssingRangedStats(RangedWeaponData rangedWeapon){
        _rangedFireRate = rangedWeapon.fireRate;
        _rangedReloadSpeed = rangedWeapon.reloadSpeed;
        _magSize = rangedWeapon.magSize;
        _currentAmmo = _magSize;
        _rangedProjectileSpeed = rangedWeapon.projectileSpeed;
        _rangedMinDmg = rangedWeapon.minDamage;
        _rangedMaxDmg = rangedWeapon.maxDamage;
        _rangedWeaponRange = rangedWeapon.weaponRange;
        _rangedCritChance = rangedWeapon.critChance;
    }

    //If the gun is corrupted, do something -> might remove later
    void CorruptedWeaponMod(int corruptionGain){

    }




    public void Shoot(){
        if(_currentAmmo > 0){ //Do we have ammo?
            if(Time.time > lastShot + _rangedFireRate){ //If the last time we shot was more than the fire rate, we can shoot.
                lastShot = Time.time; //Start timer for the last time we shot
                GameObject bullet = BulletPooler.current.GetPooledBullet(); //Grab a bullet from the bullet pool
                if(bullet == null) return; //If we don't have any bullets, do nothing (SHOULDNT HAPPEN)
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation; //Set the bullet to instantiate where the firing point is
                bullet.SetActive(true); //Set it active
                bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * _rangedProjectileSpeed, ForceMode2D.Impulse); //Add force to it
                _currentAmmo--; //Remove ammo
                uiManager.UpdateAmmo(_currentAmmo, _magSize); //Change the ammo text
            }
        }
    }


    public IEnumerator Reload(){
        _playerController.isReloading = true; //Can't shoot while reloading, prevent that!
        yield return new WaitForSeconds(_rangedReloadSpeed); //Wait as long as the weapon's reload speed is
        _currentAmmo = _magSize; //Set our ammo to be equal to the max magazine size for the weapon
        _playerController.isReloading = false; //They can reload again!
        uiManager.UpdateAmmo(_currentAmmo, _magSize); //Update UI
    }
    
    //WILL CHANGE!!!
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

    //Do calculation for damage
    //BaseDamageDone, and CritDamageMod are NOT set in Start because they can be modified at any time.
    public float CalculateMeleeDamage(){
        int critRoll = Random.Range(0, 100); //Roll something for crit
        float dmgRoll = Random.Range(_meleeMinDmg, _meleeMaxDmg) * _playerStats.BaseDamageDone; //Roll something for damage -> Rolls between base weapon damage, then multiplies by damage done mod.
        if(critRoll <= _playerStats.BaseCritChance + _meleeCritChance){ //If what we rolled for the crit, do something (effective crit chance = base crit + weapon's crit chance)
            float returnVal = Mathf.Ceil(dmgRoll *= _playerStats.CritDamageMod); //Round up and multiply the damage done by the crit modifier (base is 3x damage)
            Debug.Log("Crit! Dmg calc: " + returnVal); //test
            return returnVal; //Return the damage we're doing
        }
        Debug.Log("No crit! Dmg calc: " + Mathf.Ceil(dmgRoll));
        return Mathf.Ceil(dmgRoll); //If we didn't crit, just do the damage that we rolled and do nothing else
    }

    //This is the same as the melee damage calculation, just for ranged weapon
    public float CalculateRangedDamage(){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = Random.Range(_rangedMinDmg, _rangedMaxDmg) * _playerStats.BaseDamageDone;
        if(critRoll <= _playerStats.BaseCritChance + _rangedCritChance){
            float returnVal = Mathf.Ceil(dmgRoll *= _playerStats.CritDamageMod);
            Debug.Log("Crit! Dmg calc: " + returnVal);
            return returnVal;
        }
        Debug.Log("No crit! Dmg calc: " + Mathf.Ceil(dmgRoll));
        return Mathf.Ceil(dmgRoll);
    }
}
