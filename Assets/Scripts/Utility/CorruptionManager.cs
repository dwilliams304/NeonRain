using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CorruptionManager : MonoBehaviour
{
    public static CorruptionManager Instance;
    public delegate void CorruptionChangeHandler();

    [SerializeField] private Image tierIcon;
    [SerializeField] private TMP_Text tierTxt;

    [SerializeField] private Sprite tier1Icon;
    [SerializeField] private Sprite tier2Icon;
    [SerializeField] private Sprite tier3Icon;
    [SerializeField] private Sprite tier4Icon;
    [SerializeField] private Sprite tier5Icon;


    [SerializeField] private Color tier1Color;
    [SerializeField] private Color tier2Color;
    [SerializeField] private Color tier3Color;
    [SerializeField] private Color tier4Color;
    [SerializeField] private Color tier5Color;

    private enum CurrentCorruptionTier {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5
    }
    private CurrentCorruptionTier currentCorruptionTier;

    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private AnimationCurve corruptionAmountCurve;
    [SerializeField] private int currentTier = 0;
    [SerializeField] private int currentCorruptionAmount = 0;
    [SerializeField] private int corruptionToNextTier;

    private float corruption_DamageDoneMOD = 0f;
    private float corruption_DamageTakenMOD = 0f;
    private float corruption_CritDamageMultiplierMOD = 0f;
    private int corruption_CritChanceMOD = 0;
    private float corruption_MoveSpeedMOD = 0f;

    private float addedXP = 0f;
    private float addedGold = 0f;

    int corruptionOverflow = 0;

    void Awake(){
        Instance = this;
    }
    
    void Start(){
        _playerStats = PlayerStats.playerStats;
        ChangeCorruptionTier(currentTier);
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
    }


    public void AddCorruption(int amount){
        currentCorruptionAmount += amount;
        Debug.Log("Added: " + amount + " // New corruption amount: " + currentCorruptionAmount);
        if(currentCorruptionAmount > corruptionToNextTier && currentTier != 5){
            currentTier++;
            ChangeCorruptionTier(currentTier);
            corruptionOverflow = corruptionToNextTier - currentCorruptionAmount;
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


    public void ChangeCorruptionTier(int tier){
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
        tierTxt.text = "Tier 0";
        tierTxt.color = Color.white;
        // tierIcon = null;
        currentCorruptionAmount = 0;
        currentTier = 0;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
    }
    void TierOne(){
        tierTxt.text = "Tier 1";
        tierTxt.color = tier1Color;
        tierIcon.overrideSprite = tier1Icon;
        tierIcon.color = tier1Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;
    }
    void TierTwo(){
        tierTxt.text = "Tier 2";
        tierTxt.color = tier2Color;
        tierIcon.sprite = tier2Icon;
        tierIcon.color = tier2Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;

    }
    void TierThree(){
        tierTxt.text = "Tier 3";
        tierTxt.color = tier3Color;
        tierIcon.overrideSprite = tier3Icon;
        tierIcon.color = tier3Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;

    }
    void TierFour(){
        tierTxt.text = "Tier 4";
        tierTxt.color = tier4Color;
        tierIcon.overrideSprite = tier4Icon;
        tierIcon.color = tier4Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;

    }
    void TierFive(){
        tierTxt.text = "Tier 5";
        tierTxt.color = tier5Color;
        tierIcon.overrideSprite = tier5Icon;
        tierIcon.color = tier5Color;
        corruptionToNextTier = Mathf.RoundToInt(corruptionAmountCurve.Evaluate(currentTier));
        currentCorruptionAmount = 0;

    }
    
}
