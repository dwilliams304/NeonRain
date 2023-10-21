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


    [SerializeField] AudioSource hitSource;
    [SerializeField] ParticleSystem p;

    private UIManager _uiMngr;
    private HealthBehavior _health;

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
        _health = GetComponent<HealthBehavior>();
        _uiMngr.UpdateXPBar(ExperienceToNextLevel, CurrentPlayerXP, CurrentLevel);
        p = GetComponentInChildren<ParticleSystem>();
    }

    void ClassChosen(ClassData classChosen){
        _uiMngr.UpdateHealthBar();
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
        // PlayerMaxHealth += 10;
        // CurrentHealth = PlayerMaxHealth;
        p.Play();
        CurrentLevel++;
        CurrentPlayerXP = xpOverflow;
        DifficultyScaler.Instance.CheckDifficultyScale(CurrentLevel);
        ExperienceToNextLevel = Mathf.RoundToInt(xpScaling.Evaluate(CurrentLevel));
        _uiMngr.UpdateHealthBar();
        handleLevelIncrease?.Invoke();
    }


    public void ChangeHealth(float amount){
        _health.IncreaseMaxHealth(amount);
    }

}
