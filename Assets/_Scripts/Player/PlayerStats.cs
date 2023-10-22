using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void HandleLevelIncrease();
    public static HandleLevelIncrease handleLevelIncrease;

    public static PlayerStats Instance;

    [Header("Chosen Class")]
    public ClassData playerClass;


    [Header("PlayerXP")]
    public int CurrentLevel = 1;
    public int CurrentPlayerXP = 0;
    public int ExperienceToNextLevel = 250;
    [SerializeField] private AnimationCurve xpScaling;

    [SerializeField] ParticleSystem p;

    private UIManager _uiMngr;
    private HealthSystem _health;
    private HealthRegenerator _hRegen;

    public bool shieldActivated = false;
    
    void Awake(){
        Instance = this;
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
        _uiMngr = UIManager.Instance;
        _health = GetComponent<HealthSystem>();
        _hRegen = GetComponent<HealthRegenerator>();


        _uiMngr.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }

    void ClassChosen(ClassData classChosen){
        // _uiMngr.UpdateHealthBar();
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
        _uiMngr.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }
    //Dev Tool Only
    public void DEV_IncreaseLevel(){
        IncreaseLevel(0);
        _uiMngr.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
    }

    void IncreaseLevel(int xpOverflow){
        IncreaseMaxHealth(10);
        CurrentLevel++;
        CurrentPlayerXP = xpOverflow;
        DifficultyScaler.Instance.CheckDifficultyScale(CurrentLevel);
        ExperienceToNextLevel = Mathf.RoundToInt(xpScaling.Evaluate(CurrentLevel));
        handleLevelIncrease?.Invoke();
        p.Play();
    }


#region For Player Upgrades

    public void IncreaseMaxHealth(float amount){
        _health.IncreaseMaxHealth(amount);
    }   

    public void IncreaseHealthRegenAmount(float amount){
        _hRegen.ChangeRegenAmount(amount);
    }

    public void DecreaseHealthRegenTime(float amount){
        _hRegen.ChangeRegenTime(amount);
    }

#endregion
}
