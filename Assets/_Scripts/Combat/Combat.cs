using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Combat : MonoBehaviour
{
    public static Combat Instance;
    private float lastShot;
    private float lastSwing;
    [SerializeField] ParticleSystem muzzleFlash;
    private UIManager _uiMngr;

#region Other variables
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _enemyLayers; //For melee combat

#endregion


#region Ranged weapon variables

    private float fireRate;
    public float FireRateMod = 1f;
    private float reloadSpeed;
    private int magSize;
    private int currentAmmo;
    private float projectileSpeed;
    private float rangedMinDmg;
    private float rangedMaxDmg;
    private int rangedWeaponRange;
    private float rangedCritChance;

#endregion

#region Melee weapon variables
    private float meleeMinDmg;
    private float meleeMaxDmg;
    private float swingCooldown;
    private float swingSpeed;
    private int meleeCritChance;
#endregion


    private bool isReloading;
    public bool didCrit = false;

    [Header("Melee Specific")]
    [SerializeField] Animation meleeSwing;
    [SerializeField] InMeleeCollider _collCheck;
    
    void Awake(){
        Instance = this;
    }
    void Start(){
        _inventory = GetComponent<Inventory>();
        _uiMngr = UIManager.Instance;
        AssignRangedStats(_inventory.gun);
        AssignMeleeStats(_inventory.sword);
    }


    void Update(){
        if(Input.GetButton("Fire1") && !isReloading){
            Shoot();
        }
        if(Input.GetButtonDown("Fire2")){
            Melee();
        }
        else if(Input.GetButtonDown("Reload")){
            if(!isReloading){
                StartCoroutine(Reload());
            }
        }
    }

    //Assign all the ranged weapon data
    public void AssignRangedStats(Gun gun){
        fireRate = gun.fireRate;
        reloadSpeed = gun.reloadSpeed;
        magSize = gun.magSize;
        currentAmmo = magSize;
        projectileSpeed = gun.projectileSpeed;
        rangedMinDmg = gun.minDamage;
        rangedMaxDmg = gun.maxDamage;
        rangedCritChance = gun.critChance;
        _uiMngr.UpdateAmmo(currentAmmo, magSize);
    }

    public void AssignMeleeStats(Sword sword){
        swingSpeed = sword.swingSpeed;
        swingCooldown = sword.swingCoolDown;
        meleeMinDmg = sword.minDamage;
        meleeMaxDmg = sword.maxDamage;
        meleeCritChance = sword.critChance;
    }




    void Shoot(){
        if(currentAmmo > 0){ //Do we have ammo?
            if(Time.time > lastShot + (fireRate * FireRateMod)){ //If the last time we shot was more than the fire rate, we can shoot.
                muzzleFlash.Play();
                lastShot = Time.time; //Start timer for the last time we shot
                GameObject bullet = ObjectPooler.current.GetPooledPlayerBullet(); //Grab a bullet from the bullet pool
                if(bullet == null) return; //If we don't have any bullets, do nothing (SHOULDNT HAPPEN)
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation; //Set the bullet to instantiate where the firing point is
                bullet.SetActive(true); //Set it active
                bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * projectileSpeed, ForceMode2D.Impulse); //Add force to it
                bullet.GetComponent<Bullet>().DamageAmount = CalculateDamage(true);
                currentAmmo--; //Remove ammo
                _uiMngr.UpdateAmmo(currentAmmo, magSize); //Change the ammo text
            }
        }
    }

    void Melee(){
        if(Time.time > lastSwing + (swingCooldown * FireRateMod)){
            lastSwing = Time.time;
            _uiMngr.SwingCoolDownBar(swingCooldown);
            meleeSwing.Play();
            for(int i = 0; i < _collCheck.enemies.Count; i++){
                _collCheck.enemies[i].DecreaseCurrentHealth(CalculateDamage(false));
            }
        }
    }


    IEnumerator Reload(){
        isReloading = true; //Can't shoot while reloading, prevent that!
        _uiMngr.ReloadBar(reloadSpeed);
        yield return new WaitForSeconds(reloadSpeed); //Wait as long as the weapon's reload speed is
        currentAmmo = magSize; //Set our ammo to be equal to the max magazine size for the weapon
        isReloading = false; //They can reload again!
        _uiMngr.UpdateAmmo(currentAmmo, magSize); //Update UI
    }
    
    //Calculate and return the damage float, taking in a bool on whether it is a ranged attack or not
    //Needs bool to decided whether to take in melee weapon stats or ranged weapon stats
    //Better than having two separate methods that would do exactly this!
    int CalculateDamage(bool isRanged){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = isRanged ? Random.Range(rangedMinDmg, rangedMaxDmg) : Random.Range(meleeMinDmg, meleeMaxDmg); //Check which weapon's damage stats to use
        dmgRoll *= PlayerStatModifier.MOD_DamageDone; //No matter what this will apply.

        if(critRoll <= PlayerStatModifier.MOD_CritChance + (isRanged ? rangedCritChance : meleeCritChance)){ //Check which weapon's crit chance to add onto this
            didCrit = true;
            return Mathf.RoundToInt(dmgRoll *= PlayerStatModifier.MOD_CritDamage);
        }

        didCrit = false;
        return Mathf.RoundToInt(dmgRoll);
    }

}