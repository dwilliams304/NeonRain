using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CurrentCorruptionTier {
    Tier1,
    Tier2,
    Tier3,
    Tier4,
    Tier5
}

public class CorruptionManager : MonoBehaviour
{
    public static CorruptionManager Instance;
    
    //Events
    public delegate void CorruptionTierIncrease(int newTier);
    public delegate void CorruptionCleansed();
    public delegate void ModifyMoveSpeed(float amnt);
    public delegate void StartKillTimer();
    public delegate void StopKillTimer();
    public static StopKillTimer stopKillTimer;
    public static StartKillTimer startKillTimer;
    public static CorruptionTierIncrease corruptionTierIncrease;
    public static CorruptionCleansed corruptionCleansed;
    public static ModifyMoveSpeed moveSpeedModifier;
    
    [Header("Assign Global Volume")]
    [SerializeField] CorruptionPostProcessing postProcess;
    
    [Header("Corruption UI Elements")]
    [SerializeField] private Image tierIcon;
    [SerializeField] private TMP_Text tierTxt;
    [SerializeField] private TMP_Text buffInfo;
    [SerializeField] private TMP_Text debuffInfo;
    [SerializeField] private Slider corruptionBar;
    
    [Header("Corruption Tier Icons")]
    [SerializeField] private Sprite tier0Icon;
    [SerializeField] private Sprite tier1Icon;
    [SerializeField] private Sprite tier2Icon;
    [SerializeField] private Sprite tier3Icon;
    [SerializeField] private Sprite tier4Icon;
    [SerializeField] private Sprite tier5Icon;

    [Header("Corruption text/image colors")]
    [SerializeField] private Color tier1Color;
    [SerializeField] private Color tier2Color;
    [SerializeField] private Color tier3Color;
    [SerializeField] private Color tier4Color;
    [SerializeField] private Color tier5Color;
    
    [Header("Corruption Required Curve")]
    [SerializeField] private AnimationCurve corruptionAmountCurve;

    [Header("Modified Stats -> Only Needed During Tests")]
    [SerializeField] private float addedDamageDone = 0f;
    [SerializeField] private float addedDamageTaken = 0f;
    [SerializeField] private float addedMoveSpeed = 0f;
    [SerializeField] private float addedXP = 0f;
    // [SerializeField] private float addedGold = 0f;
    [SerializeField] private float addedLuck = 0f;

    //Private variables
    private PlayerStats _playerStats;
    private int currentTier = 0;
    private int currentCorruptionAmount = 0;
    private int corruptionToNextTier;
    // XPManager _xpManager;
    
    private CurrentCorruptionTier currentCorruptionTier;
    int corruptionOverflow = 0;

    void Awake(){
        Instance = this;
    }
    
    void Start(){
        _playerStats = PlayerStats.playerStats;
        ChangeCorruptionTier(currentTier);
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        corruptionBar.maxValue = corruptionToNextTier;
        corruptionBar.value = currentCorruptionAmount;
        // _xpManager = XPManager.Instance;
        
    }

    //Only needs public because of the DevMenu
    public void AddCorruption(int amount){
        currentCorruptionAmount += amount;
        if(currentCorruptionAmount >= corruptionToNextTier && currentTier != 5){
            currentTier++;
            ChangeCorruptionTier(currentTier);
            corruptionOverflow = corruptionToNextTier - currentCorruptionAmount;
        }
        if(currentTier == 5){
            corruptionBar.value = corruptionToNextTier;
        }
        else{
            corruptionBar.maxValue = corruptionToNextTier;
            corruptionBar.value = currentCorruptionAmount;
        }
    }
    public void RemoveCorruption(int amount){
        currentCorruptionAmount -= amount;
        if(currentCorruptionAmount - amount < 0){
            if(currentTier != 0){
                currentTier--;
            }
            
        }
    }

    public void DEV_AddTier(){
        if(currentTier < 5){
            currentTier++;
            ChangeCorruptionTier(currentTier);
        }
    }
    public void ChangeCorruptionTier(int tier){
        corruptionTierIncrease?.Invoke(tier);
        switch(tier){
            case 0:
                TierZero();
                break;
            case 1:
                TierOne();
                break;
            case 2:
                TierTwo();
                break;
            case 3:
                TierThree();
                break;
            case 4:
                TierFour();
                break;
            case 5:
                TierFive();
                break;
        }
    }

    void TierZero(){
        //UI Changes
        tierTxt.text = "Tier 0";
        tierTxt.color = Color.white;
        tierIcon.overrideSprite = tier0Icon;
        tierIcon.color = Color.white;
        currentCorruptionAmount = 0;
        currentTier = 0;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        corruptionBar.maxValue = corruptionToNextTier;
        corruptionBar.value = currentCorruptionAmount;
        stopKillTimer?.Invoke();
        
        //Stat changes
        _playerStats.DamageDoneMod -= addedDamageDone;
        _playerStats.DamageTakenMod -= addedDamageTaken;
        XPManager.Instance.XPModifier -= addedXP;
        moveSpeedModifier?.Invoke(-addedMoveSpeed);

        //Reset all values
        addedLuck = 0;
        addedDamageDone = 0;
        addedDamageTaken = 0;
        addedXP = 0;
        addedMoveSpeed = 0;
    }
    void TierOne(){
        //UI Changes
        tierTxt.text = "Tier 1";
        tierTxt.color = tier1Color;
        tierIcon.overrideSprite = tier1Icon;
        tierIcon.color = tier1Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;
        //Stat changes
        //Add to temp var
        addedDamageDone += 0.05f; //5%
        addedDamageTaken += 0.05f; //5%
        addedXP += 0.05f; //5%
        addedLuck += 0.01f; //1%


        _playerStats.DamageDoneMod += 0.05f;
        _playerStats.DamageTakenMod += 0.05f;
        XPManager.Instance.XPModifier += 0.05f;
        LootManager.lootManager.AddedLuck += 0.01f;
        
    }
    void TierTwo(){
        //UI Changes
        tierTxt.text = "Tier 2";
        tierTxt.color = tier2Color;
        tierIcon.overrideSprite = tier2Icon;
        tierIcon.color = tier2Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;
        //Stat changes
        addedDamageDone += 0.05f; //10%
        addedDamageTaken += 0.05f; //10%
        addedXP += 0.2f; //25%
        addedLuck += 0.01f; //2%

        _playerStats.DamageDoneMod += 0.05f;
        _playerStats.DamageTakenMod += 0.05f;
        XPManager.Instance.XPModifier += 0.2f;

    }
    void TierThree(){
        //UI Changes
        tierTxt.text = "Tier 3";
        tierTxt.color = tier3Color;
        tierIcon.overrideSprite = tier3Icon;
        tierIcon.color = tier3Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;
        //Stat changes
        addedDamageDone += 0.15f; //25%
        addedDamageTaken += 0.2f; //30%
        addedXP += 0.25f; //50%
        addedLuck += 0.01f; //3%

        _playerStats.DamageDoneMod += 0.15f;
        _playerStats.DamageTakenMod += 0.2f;
        XPManager.Instance.XPModifier += 0.25f;
        
    }
    void TierFour(){
        //UI Changes
        tierTxt.text = "Tier 4";
        tierTxt.color = tier4Color;
        tierIcon.overrideSprite = tier4Icon;
        tierIcon.color = tier4Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;
        //Stat changes
        addedDamageDone += 0.25f; //50%
        addedDamageTaken += 0.45f; //75%
        addedXP += 0.5f; //100%

        _playerStats.DamageDoneMod += 0.25f;
        _playerStats.DamageTakenMod += 0.45f;
        XPManager.Instance.XPModifier += 0.5f;

    }
    void TierFive(){
        //UI Changes
        tierTxt.text = "Tier 5";
        tierTxt.color = tier5Color;
        tierIcon.overrideSprite = tier5Icon;
        tierIcon.color = tier5Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;
        corruptionBar.value = corruptionToNextTier;   
        startKillTimer?.Invoke();
        //Stat changes

        addedDamageDone += 0.5f; //100%
        addedDamageTaken += 1.25f; //200%
        addedXP += 1f; //200%
        addedMoveSpeed += 3f;

        _playerStats.DamageDoneMod += 0.5f;
        _playerStats.DamageTakenMod += 0.75f;
        moveSpeedModifier?.Invoke(addedMoveSpeed);
        XPManager.Instance.XPModifier += 1f;

    }



    public void ShowCorruptionToolTip(){
        switch (currentTier){
            case 0:
                buffInfo.text = "No buffs at Tier 0";
                debuffInfo.text = "No debuffs at Tier 0";
                break;


            case 1:
                buffInfo.text = $"+5% Damage\n+5% XP Gain \n+1% Luck \n (NOT FINAL)";
                debuffInfo.text = $"+5% Damage Taken \n (NOT FINAL)";
                break;


            case 2:
                buffInfo.text = $"+10% Damage Done \n+25% XP Gain \n+2% Luck \n (NOT FINAL)";
                debuffInfo.text = $"+10% Damage Taken \n (NOT FINAL)";
                break;


            case 3:
                buffInfo.text = $"+25% Damage Done \n +50% XP Gain \n+3% Luck \n (NOT FINAL)";
                debuffInfo.text = $"+30% Damage Taken \n (NOT FINAL)";
                break;


            case 4:
                buffInfo.text = $"+50% Damage Done \n+100% XP Gain \n+3% Luck \n (NOT FINAL)";
                debuffInfo.text = $"+75% Damage Taken \n (NOT FINAL)";
                break;


            case 5:
                buffInfo.text = $"+100% Damge Done \n+200% XP Gain \n+3% Luck \n (NOT FINAL)";
                debuffInfo.text = $"+150% Damage Taken \n (NOT FINAL)";
                break;
        }
    }
    
}
