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
    [SerializeField] private TMP_Text buffInfo;
    [SerializeField] private TMP_Text debuffInfo;
    [SerializeField] private Slider corruptionBar;

    [SerializeField] private Sprite tier0Icon;
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

    [SerializeField] private float addedDamageDone = 0f;
    [SerializeField] private float addedDamageTaken = 0f;
    [SerializeField] private float addedMoveSpeed = 0f;
    [SerializeField] private float addedXP = 0f;
    [SerializeField] private float addedGold = 0f;
    [SerializeField] private float addedLuck = 0f;

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
    }


    public void AddCorruption(int amount){
        currentCorruptionAmount += amount;
        if(currentCorruptionAmount > corruptionToNextTier && currentTier != 5){
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
        
        //Stat changes
        _playerStats.DamageDoneMod -= addedDamageDone;
        _playerStats.DamageTakenMod -= addedDamageTaken;
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
        addedDamageTaken += 0.05f;
        addedXP += 0.05f;
        addedLuck += 0.01f; //1%


        _playerStats.DamageDoneMod += 0.05f;
        _playerStats.DamageTakenMod += 0.05f;
        _playerStats.XPModifier += 0.05f;
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
        addedDamageTaken += 0.05f;
        addedLuck += 0.01f; //2%

        _playerStats.DamageDoneMod += 0.05f;
        _playerStats.DamageTakenMod += 0.05f;

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
        addedDamageDone += 0.05f;
        addedDamageTaken += 0.05f;
        addedLuck += 0.01f;
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
        //Stat changes

    }



    public void ShowCorruptionToolTip(){
        switch (currentTier){
            case 0:
                buffInfo.text = "No buffs at Tier 0";
                debuffInfo.text = "No debuffs at Tier 0";
                break;


            case 1:
                buffInfo.text = $"Tier 1 buffs go here\n+Buff 1 \n+Buff 2 \n+Buff 3";
                debuffInfo.text = $"Tier 1 debuffs go here\n-Debuff 1 \n-Debuff 2 \n-Debuff 3";
                break;


            case 2:
                buffInfo.text = $"Tier 2 buffs go here\n+Buff 1 \n+Buff 2 \n+Buff 3";
                debuffInfo.text = $"Tier 2 debuffs go here\n-Debuff 1 \n-Debuff 2 \n-Debuff 3";
                break;


            case 3:
                buffInfo.text = $"Tier 3 buffs go here\n+Buff 1 \n+Buff 2 \n+Buff 3";
                debuffInfo.text = $"Tier 3 debuffs go here\n-Debuff 1 \n-Debuff 2 \n-Debuff 3";
                break;


            case 4:
                buffInfo.text = $"Tier 4 buffs go here\n+Buff 1 \n+Buff 2 \n+Buff 3";
                debuffInfo.text = $"Tier 4 debuffs go here\n-Debuff 1 \n-Debuff 2 \n-Debuff 3";
                break;


            case 5:
                buffInfo.text = $"Tier 5 buffs go here\n+Buff 1 \n+Buff 2 \n+Buff 3";
                debuffInfo.text = $"Tier 5 debuffs go here\n-Debuff 1 \n-Debuff 2 \n-Debuff 3";
                break;
        }
    }
    
}
