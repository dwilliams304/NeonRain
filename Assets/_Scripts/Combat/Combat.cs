using System.Collections;
using UnityEngine;


public class Combat : MonoBehaviour
{
    public delegate void OnPlayerDeath();
    public static OnPlayerDeath onPlayerDeath;
    public static Combat Instance;
    
#region Ranged weapon variables
    private float fireRate;
    private float lastShot;
    public float FireRateMod = 1f;
    private float reloadSpeed;
    private int magSize;
    private int currentAmmo;
    private float projectileSpeed;
    private float rangedMinDmg;
    private float rangedMaxDmg;
    private int rangedWeaponRange;
    bool isRifle;
    private float rangedCritChance;
    private bool isReloading;
    private WaitForSeconds reloadSpeedWait;
    [SerializeField] ParticleSystem muzzleFlash;
#endregion

#region Melee weapon variables
    private float meleeMinDmg;
    private float meleeMaxDmg;
    private float swingCooldown;
    private float swingSpeed;
    private float lastSwing;
    private int meleeCritChance;
#endregion

    [Header("Melee Specific")]
    [SerializeField] Animation meleeSwing;
    [SerializeField] InMeleeCollider _collCheck;

    [Header("Audio Clips")]
    [SerializeField] AudioClip _gunShot;
    [SerializeField] AudioClip _swordSing;

    [Header("Death Related Stuff")]
    [SerializeField] ParticleSystem _deathPS;
    [SerializeField] SpriteRenderer _playerSprite;
    PlayerController _playerController;

#region Misc. variables
    [Header("Other")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] HealthSystem _healthSystem;
    private Inventory _inventory;
    private UIManager _uiMngr;
    
    [HideInInspector] public bool didCrit = false;
#endregion
    

    void Awake(){
        Instance = this;
    }
    void Start(){
        _inventory = GetComponent<Inventory>();
        _playerController = GetComponent<PlayerController>();
        _uiMngr = UIManager.Instance;
        // _healthSystem = GetComponent<HealthSystem>();
        AssignRangedStats(_inventory.gun);
        AssignMeleeStats(_inventory.sword);
    }

    void OnEnable(){
        _healthSystem.onDeath += Die;
    }

    void OnDisable(){
        _healthSystem.onDeath -= Die;
    }


    void Update(){
        /* Only add if I want it to where you can only hold down fire button with rifles
        But otherwise have to shoot every time if its anything but an auto.
        if(!isReloading && isRifle ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1")){
            Shoot();
        }
        */
        if(!isReloading && Input.GetButton("Fire1")){
            Shoot();
        }
        else if(Input.GetButtonDown("Fire2")){
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
        reloadSpeedWait = new WaitForSeconds(reloadSpeed); //cache this anytime we swap weapons and reload speed changes
        magSize = gun.magSize;
        currentAmmo = magSize;
        projectileSpeed = gun.projectileSpeed;
        rangedMinDmg = gun.minDamage;
        rangedMaxDmg = gun.maxDamage;
        rangedCritChance = gun.critChance;
        if(gun.gunType == GunType.AutomaticRifle){ isRifle = true; }
        else{ isRifle = false; }
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
            if(Time.time > lastShot + (fireRate * FireRateMod)){ //If the current time is greater than the last time we shot + current weapon's fire rate, we can shoot.
                muzzleFlash.Play(); //Play particle effect!
                lastShot = Time.time; //Start timer for the last time we shot
                GameObject bullet = ObjectPooler.current.GetPooledPlayerBullet(); //Grab a bullet from the bullet pool
                if(bullet == null) return; //If we don't have any bullets, do nothing (SHOULDNT HAPPEN)
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation; //Set the bullet to the firepoint's position and rotation
                bullet.SetActive(true); //Set it active
                bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * projectileSpeed, ForceMode2D.Impulse); //Add force to it
                bullet.GetComponent<Bullet>().DamageAmount = CalculateDamage(true); //Set the calculated damage to the bullet's value.
                currentAmmo--; //Remove ammo
                _uiMngr.UpdateAmmo(currentAmmo, magSize); //Change the ammo text
                SoundManager.Instance.PlayEffectAudio(_gunShot);
            }
        }
    }

    void Melee(){
        if(Time.time > lastSwing + (swingCooldown * FireRateMod)){
            SoundManager.Instance.PlayEffectAudio(_swordSing);
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
        yield return reloadSpeedWait; //Wait as long as the weapon's reload speed is
        currentAmmo = magSize; //Set our ammo to be equal to the max magazine size for the weapon
        isReloading = false; //They can reload again!
        _uiMngr.UpdateAmmo(currentAmmo, magSize); //Update UI
    }
    
    //Calculate and return the damage float, taking in a bool on whether it is a ranged attack or not
    //Needs bool to decided whether to take in melee weapon stats or ranged weapon stats
    //Better than having two separate methods that would do exactly this!
    int CalculateDamage(bool isRangedAttack){
        int critRoll = Random.Range(0, 100);
        float dmgRoll = isRangedAttack ? Random.Range(rangedMinDmg, rangedMaxDmg) : Random.Range(meleeMinDmg, meleeMaxDmg); //Check which weapon's damage stats to use
        dmgRoll *= PlayerStatModifier.MOD_DamageDone; //No matter what this will apply.

        if(critRoll <= PlayerStatModifier.MOD_CritChance + (isRangedAttack ? rangedCritChance : meleeCritChance)){ //Check which weapon's crit chance to add onto this
            didCrit = true;
            return Mathf.RoundToInt(dmgRoll *= PlayerStatModifier.MOD_CritDamage);
        }

        didCrit = false;
        return Mathf.RoundToInt(dmgRoll);
    }


    void Die(){
        StartCoroutine(HandleDeathEffects());
    }

    IEnumerator HandleDeathEffects(){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _playerController.enabled = false;
        Time.timeScale = 0.2f;
        _playerSprite.enabled = false;
        _deathPS.Play();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        onPlayerDeath?.Invoke();
    }

}
