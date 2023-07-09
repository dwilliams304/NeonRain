using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats playerStats;
    public delegate void HandleLevelIncrease();
    public delegate void HandlePlayerDeath();
    public static HandleLevelIncrease handleLevelIncrease;
    public static HandlePlayerDeath onPlayerDeath;

    [Header("Health")]
    public float PlayerMaxHealth = 100;
    public float CurrentHealth;


    [Header("PlayerXP")]
    public int CurrentLevel = 1;
    public int CurrentPlayerXP = 0;
    public int ExperienceToNextLevel = 250;
    [SerializeField] private AnimationCurve xpScaling;

    [Header("Gold")]
    [SerializeField] private int PlayerGold = 0;


    [Header("Base modifiers")]
    public int CritChanceMod = 10;
    public float DamageDoneMod = 1f;
    public float DamageTakenMod = 1f;
    public float CritDamageMod = 3f;
    public float AdditionalGoldMod = 1f;

    [SerializeField] AudioSource hitSource;
    

    void Awake(){
        CurrentHealth = PlayerMaxHealth;
        playerStats = this;
    }

    void OnEnable(){
        XPManager.Instance.onXPChange += IncrementExperience;
        HealthPotion.addHealth += IncreaseHealth;
    }
    void OnDisable(){
        XPManager.Instance.onXPChange -= IncrementExperience;
        HealthPotion.addHealth -= IncreaseHealth;
    }

    void Start(){
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }


    void AddGold(int amount){
        PlayerGold += Mathf.CeilToInt(amount * AdditionalGoldMod);
        UIManager.uiManagement.UpdateGoldUI(PlayerGold);
    }

    //Only public for Dev Tool
    public void IncrementExperience(int xpAmnt){
        CurrentPlayerXP += xpAmnt;
        if(CurrentPlayerXP >= ExperienceToNextLevel){
            int overflow = CurrentPlayerXP - ExperienceToNextLevel;
            IncreaseLevel(overflow);
        }
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }
    //Dev Tool Only
    public void DEV_IncreaseLevel(){
        IncreaseLevel(0);
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }

    void IncreaseLevel(int xpOverflow){
        PlayerMaxHealth += 25;
        CurrentHealth = PlayerMaxHealth;
        DamageDoneMod += 0.02f;
        CritChanceMod += 1;
        CurrentLevel++;
        CurrentPlayerXP = xpOverflow;
        ExperienceToNextLevel = Mathf.RoundToInt(xpScaling.Evaluate(CurrentLevel));
        UIManager.uiManagement.UpdateHealthBar();
        handleLevelIncrease?.Invoke();
    }


    void IncreaseHealth(int amount, int amntOfPots){
        CurrentHealth += amount;
        if(CurrentHealth > PlayerMaxHealth){
            CurrentHealth = PlayerMaxHealth;
        }
    }

    public void TakeDamage(float damage){
        CurrentHealth -= Mathf.Ceil(damage * DamageTakenMod);
        if(CurrentHealth <= 0){
            onPlayerDeath?.Invoke();
        }
        UIManager.uiManagement.UpdateHealthBar();
        //AudioManager.Instance.HITSFX();
        // hitSource.Play();
    }



}
