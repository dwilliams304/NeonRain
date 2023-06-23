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
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _enemyLayers;
    #endregion


    #region Ranged weapon variables
    public float rangedFireRate;
    public float rangedReloadSpeed;
    public int magSize;
    public int currentAmmo;
    public float rangedProjectileSpeed;
    public float rangedMinDmg;
    public float rangedMaxDmg;
    public int rangedWeaponRange;
    public float rangedCritChance;
    #endregion

    public bool didCrit = false;
    
    void Awake(){
        combat = this;
    }
    void Start(){
        _inventory = GetComponent<Inventory>();
        _playerController = GetComponent<PlayerController>();
        _playerStats = PlayerStats.playerStats;
        _weapon = _inventory.weapon;
        AssignWeaponStats(_weapon);
    }

    //Assign all the ranged weapon data
    public void AssignWeaponStats(Weapon weapon){
        rangedFireRate = weapon.fireRate;
        rangedReloadSpeed = weapon.reloadSpeed;
        magSize = weapon.magSize;
        currentAmmo = magSize;
        rangedProjectileSpeed = weapon.projectileSpeed;
        rangedMinDmg = weapon.minDamage;
        rangedMaxDmg = weapon.maxDamage;
        rangedWeaponRange = weapon.weaponRange;
        rangedCritChance = weapon.critChance;
        uiManager.UpdateAmmo(currentAmmo, magSize);
    }

    //If the gun is corrupted, do something -> might remove later
    void CorruptedWeaponMod(int corruptionGain){

    }




    public void Shoot(){
        if(currentAmmo > 0){ //Do we have ammo?
            if(Time.time > lastShot + rangedFireRate){ //If the last time we shot was more than the fire rate, we can shoot.
                lastShot = Time.time; //Start timer for the last time we shot
                GameObject bullet = BulletPooler.current.GetPooledBullet(); //Grab a bullet from the bullet pool
                if(bullet == null) return; //If we don't have any bullets, do nothing (SHOULDNT HAPPEN)
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation; //Set the bullet to instantiate where the firing point is
                bullet.SetActive(true); //Set it active
                bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * rangedProjectileSpeed, ForceMode2D.Impulse); //Add force to it
                currentAmmo--; //Remove ammo
                uiManager.UpdateAmmo(currentAmmo, magSize); //Change the ammo text
            }
        }
    }


    public IEnumerator Reload(){
        _playerController.isReloading = true; //Can't shoot while reloading, prevent that!
        yield return new WaitForSeconds(rangedReloadSpeed); //Wait as long as the weapon's reload speed is
        currentAmmo = magSize; //Set our ammo to be equal to the max magazine size for the weapon
        _playerController.isReloading = false; //They can reload again!
        uiManager.UpdateAmmo(currentAmmo, magSize); //Update UI
        uiManager.ReloadBar(rangedReloadSpeed);
    }
    
    //This is the same as the melee damage calculation, just for ranged weapon
    public float CalculateRangedDamage(){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = Random.Range(rangedMinDmg, rangedMaxDmg) * _playerStats.BaseDamageDone;
        if(critRoll <= _playerStats.BaseCritChance + rangedCritChance){
            float returnVal = Mathf.Ceil(dmgRoll *= _playerStats.CritDamageMod);
            //Debug.Log("Crit! Dmg calc: " + returnVal);
            didCrit = true;
            return returnVal;
        }
        //Debug.Log("No crit! Dmg calc: " + Mathf.Ceil(dmgRoll));
        didCrit = false;
        return Mathf.Ceil(dmgRoll);
    }

}
