using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void HandleLevelIncrease();
    public delegate void HandlePlayerDeath();
    public static HandleLevelIncrease handleLevelIncrease;
    public static HandlePlayerDeath onPlayerDeath;

    [Header("Chosen Class")]
    public ClassData playerClass;

    public int PlayerMaxHealth {get; private set;} = 100;
    public int CurrentHealth {get; private set;}


    [Header("PlayerXP")]
    public int CurrentLevel = 1;
    public int CurrentPlayerXP = 0;
    public int ExperienceToNextLevel = 250;
    [SerializeField] private AnimationCurve xpScaling;


    [SerializeField] AudioSource hitSource;
    [SerializeField] ParticleSystem p;

    public bool shieldActivated = false;
    

    void Awake(){
        CurrentHealth = PlayerMaxHealth;
    }

    void OnEnable(){
        XPManager.Instance.onXPChange += IncrementExperience;
        ClassSelector.classChosen += ClassChosen;
    }
    void OnDisable(){
        XPManager.Instance.onXPChange -= IncrementExperience;
        ClassSelector.classChosen -= ClassChosen;
    }

    void Start(){
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
        p = GetComponentInChildren<ParticleSystem>();
    }

    void ClassChosen(ClassData classChosen){
        PlayerMaxHealth = classChosen.MaxHealth;
        CurrentHealth = PlayerMaxHealth;
        UIManager.uiManagement.UpdateHealthBar();
    }

    //Only public for Dev Tool
    public void IncrementExperience(int xpAmnt){
        CurrentPlayerXP += xpAmnt;
        if(CurrentPlayerXP >= ExperienceToNextLevel){
            int overflow = CurrentPlayerXP - ExperienceToNextLevel;
            if(CurrentLevel < 75){
                IncreaseLevel(overflow);
            }
        }
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }
    //Dev Tool Only
    public void DEV_IncreaseLevel(){
        IncreaseLevel(0);
        UIManager.uiManagement.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }

    void IncreaseLevel(int xpOverflow){
        PlayerMaxHealth += 10;
        p.Play();
        CurrentHealth = PlayerMaxHealth;
        CurrentLevel++;
        CurrentPlayerXP = xpOverflow;
        DifficultyScaler.Instance.CheckDifficultyScale(CurrentLevel);
        ExperienceToNextLevel = Mathf.RoundToInt(xpScaling.Evaluate(CurrentLevel));
        UIManager.uiManagement.UpdateHealthBar();
        handleLevelIncrease?.Invoke();
    }


    public void IncreaseHealth(int amount){
        CurrentHealth += amount;
        UIManager.uiManagement.UpdateHealthBar();
        if(CurrentHealth > PlayerMaxHealth){
            CurrentHealth = PlayerMaxHealth;
        }
    }

    public void TakeDamage(float damage){
        CurrentHealth -= Mathf.CeilToInt(damage * PlayerStatModifier.MOD_DamageTaken);
        if(CurrentHealth <= 0){
            onPlayerDeath?.Invoke();
        }
        UIManager.uiManagement.UpdateHealthBar();

        //AudioManager.Instance.HITSFX();
        hitSource.Play();
    }


}
