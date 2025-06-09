using UnityEngine;



public class PlayerStats : MonoBehaviour
{
    public delegate void HandleLevelIncrease(int newLevel);
    public static HandleLevelIncrease handleLevelIncrease;

    public static PlayerStats Instance;
    

    [Header("Chosen Class")]
    public ClassData playerClass;


    [SerializeField] ParticleSystem _levelUpParticles;

    private HealthSystem _health;
    private HealthRegenerator _hRegen;

    public bool shieldActivated = false;
    
    void Awake(){
        Instance = this;
    }

    void OnEnable(){
        ClassSelector.classChosen += ClassChosen;
        LevelSystem.onLevelChange += IncreaseLevel;
    }
    void OnDisable(){
        ClassSelector.classChosen -= ClassChosen;
        LevelSystem.onLevelChange -= IncreaseLevel;
    }

    void Start(){
        _health = GetComponent<HealthSystem>();
        _hRegen = GetComponent<HealthRegenerator>();

        _health.SetMaxHealth(100);

    }


    void ClassChosen(ClassData classChosen){
        // _uiMngr.UpdateHealthBar();
    }


    void IncreaseLevel(int level){
        IncreaseMaxHealth(10);
        _levelUpParticles.Play();
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
