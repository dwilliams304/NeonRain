using System.Collections;
using UnityEngine;


public class Combat : MonoBehaviour
{
    public static Combat combat;
    private float lastShot;
    [SerializeField] AudioSource gunSFX;
    [SerializeField] AudioClip gunShot;

#region Other variables

    [SerializeField] private UIManager uiManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _enemyLayers;

#endregion


#region Ranged weapon variables

    private float fireRate;
    private float reloadSpeed;
    private int magSize;
    private int currentAmmo;
    private float projectileSpeed;
    private float rangedMinDmg;
    private float rangedMaxDmg;
    private int rangedWeaponRange;
    private float rangedCritChance;

#endregion


    private bool isReloading;
    public bool didCrit = false;
    
    void Awake(){
        combat = this;
    }
    void Start(){
        _inventory = GetComponent<Inventory>();
        _playerStats = PlayerStats.playerStats;
        AssignWeaponStats(_inventory.weapon);
    }


    void Update(){
        if(Input.GetButton("Fire1") && !isReloading){
            Shoot();
        }
        else if(Input.GetButtonDown("Reload")){
            StartCoroutine(Reload());
        }
    }

    //Assign all the ranged weapon data
    public void AssignWeaponStats(Weapon weapon){
        fireRate = weapon.fireRate;
        reloadSpeed = weapon.reloadSpeed;
        magSize = weapon.magSize;
        currentAmmo = magSize;
        projectileSpeed = weapon.projectileSpeed;
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
            if(Time.time > lastShot + fireRate){ //If the last time we shot was more than the fire rate, we can shoot.
                //AudioManager.Instance.GUNSFX();
                // gunSFX.Play();
                lastShot = Time.time; //Start timer for the last time we shot
                GameObject bullet = ObjectPooler.current.GetPooledPlayerBullet(); //Grab a bullet from the bullet pool
                if(bullet == null) return; //If we don't have any bullets, do nothing (SHOULDNT HAPPEN)
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation; //Set the bullet to instantiate where the firing point is
                bullet.SetActive(true); //Set it active
                bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * projectileSpeed, ForceMode2D.Impulse); //Add force to it
                currentAmmo--; //Remove ammo
                uiManager.UpdateAmmo(currentAmmo, magSize); //Change the ammo text
            }
        }
    }


    public IEnumerator Reload(){
        isReloading = true; //Can't shoot while reloading, prevent that!
        uiManager.ReloadBar(reloadSpeed);
        yield return new WaitForSeconds(reloadSpeed); //Wait as long as the weapon's reload speed is
        currentAmmo = magSize; //Set our ammo to be equal to the max magazine size for the weapon
        isReloading = false; //They can reload again!
        uiManager.UpdateAmmo(currentAmmo, magSize); //Update UI
    }
    
    //This is the same as the melee damage calculation, just for ranged weapon
    public float CalculateRangedDamage(){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = Random.Range(rangedMinDmg, rangedMaxDmg) * _playerStats.DamageDoneMod;
        if(critRoll <= _playerStats.CritChanceMod + rangedCritChance){
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
