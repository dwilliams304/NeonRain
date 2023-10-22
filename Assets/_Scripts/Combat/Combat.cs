using System.Collections;
using UnityEngine;


public class Combat : MonoBehaviour
{
    public static Combat Instance;
    private float lastShot;
    [SerializeField] ParticleSystem muzzleFlash;

#region Other variables
    private UIManager _uiMngr;
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


    private bool isReloading;
    public bool didCrit = false;
    
    void Awake(){
        Instance = this;
    }
    void Start(){
        _inventory = GetComponent<Inventory>();
        _uiMngr = UIManager.Instance;
        AssignWeaponStats(_inventory.weapon);
    }


    void Update(){
        if(Input.GetButton("Fire1") && !isReloading){
            Shoot();
        }
        else if(Input.GetButtonDown("Reload")){
            if(!isReloading){
                StartCoroutine(Reload());
            }
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
        _uiMngr.UpdateAmmo(currentAmmo, magSize);
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
                bullet.GetComponent<Bullet>().DamageAmount = CalculateRangedDamage();
                currentAmmo--; //Remove ammo
                _uiMngr.UpdateAmmo(currentAmmo, magSize); //Change the ammo text
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
    
    //This is the same as the melee damage calculation, just for ranged weapon
    float CalculateRangedDamage(){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = Random.Range(rangedMinDmg, rangedMaxDmg) * PlayerStatModifier.MOD_DamageDone;
        if(critRoll <= PlayerStatModifier.MOD_CritChance + rangedCritChance){
            float returnVal = Mathf.Ceil(dmgRoll *= PlayerStatModifier.MOD_CritDamage);
            didCrit = true;
            return returnVal;
        }
        didCrit = false;
        return Mathf.Ceil(dmgRoll);
    }

}
