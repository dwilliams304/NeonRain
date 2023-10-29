using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using TMPro;


public enum CorruptionTier {
    Tier0,
    Tier1,
    Tier2,
    Tier3,
    Tier4,
    Tier5
}

public class CorruptionManager : MonoBehaviour
{
    public static CorruptionManager Instance;
    public delegate void ChangeCorruptionTier(CorruptionTier newTier);
    public delegate void AddCorruption(int amount, int amountToNextTier);
    public static ChangeCorruptionTier changeCorruptionTier;
    public static AddCorruption addCorruption;

    public int currentCorruptionAmount {get; private set;} = 0;
    public int corruptionToNextTier {get; private set;} = 1000;
    [SerializeField] private AnimationCurve corruptionScaling;

    public CorruptionTier currentTier = CorruptionTier.Tier0;

    private KillTimer _killTimer;

    // public TMP_Text corrAmnt;
    // public TMP_Text corrTier;

    void Awake() => Instance = this;

    void Start() => _killTimer = GetComponent<KillTimer>();

    void Update(){
        if(Input.GetKeyDown(KeyCode.KeypadPlus)){
            IncreaseCorruptionAmount(255);
        }
        else if(Input.GetKeyDown(KeyCode.KeypadMinus)){
            DecreaseCorruptionAmount(250);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)){
            ForceIncreaseTier();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            ForceDecreaseTier();
        }
    }

    public void IncreaseCorruptionAmount(int amount){
        currentCorruptionAmount += amount;
        if(currentCorruptionAmount >= corruptionToNextTier){
            int overflow = currentCorruptionAmount - corruptionToNextTier; //if we go to the next tier -> add the leftover amount to the next tier
            if(currentTier == CorruptionTier.Tier5){
                currentCorruptionAmount = corruptionToNextTier;
            }
            else{
                GoToNextTier(currentTier, overflow);
            }
        }
        addCorruption?.Invoke(currentCorruptionAmount, corruptionToNextTier);
        // corrAmnt.text = $"{currentCorruptionAmount} / {corruptionToNextTier}";
        // corrTier.text = $"{currentTier}";
    }

    public void DecreaseCorruptionAmount(int amount){
        currentCorruptionAmount -= amount;
        if(currentCorruptionAmount < 0){
            int overflow = -currentCorruptionAmount; //same as increasing, but flip the negative
            if(currentTier == CorruptionTier.Tier0){
                currentCorruptionAmount = 0;
            }
            else{
                GoDownATier(currentTier, overflow);
            }
        }
        addCorruption?.Invoke(currentCorruptionAmount, corruptionToNextTier);
        // corrAmnt.text = $"{currentCorruptionAmount} / {corruptionToNextTier}";
        // corrTier.text = $"{currentTier}";
    }

    public void ForceIncreaseTier() => GoToNextTier(currentTier, 0);

    public void ForceDecreaseTier() => GoDownATier(currentTier, 0);
    

    void GoToNextTier(CorruptionTier tier, int overflow){
        switch(tier){
            case CorruptionTier.Tier0:
                currentTier = CorruptionTier.Tier1;
                break;

            case CorruptionTier.Tier1:
                currentTier = CorruptionTier.Tier2;
                break;

            case CorruptionTier.Tier2:
                currentTier = CorruptionTier.Tier3;
                break;

            case CorruptionTier.Tier3:
                currentTier = CorruptionTier.Tier4;
                break;

            case CorruptionTier.Tier4:
                currentTier = CorruptionTier.Tier5;
                _killTimer.StartTimer();
                break;
            //Dont include Tier 5 as you can't go beyond that tier
        }
        HandleTierChange();
        currentCorruptionAmount = overflow;
    }

    void GoDownATier(CorruptionTier tier, int overflow){
        switch(tier){
            case CorruptionTier.Tier5:
                currentTier = CorruptionTier.Tier4;
                _killTimer.StopTimer();
                break;

            case CorruptionTier.Tier4:
                currentTier = CorruptionTier.Tier3;
                break;

            case CorruptionTier.Tier3:
                currentTier = CorruptionTier.Tier2;
                break;

            case CorruptionTier.Tier2:
                currentTier = CorruptionTier.Tier1;
                break;

            case CorruptionTier.Tier1:
                currentTier = CorruptionTier.Tier0;
                break;
            //Dont include Tier 0, because you can't go below
        }
        HandleTierChange();
        currentCorruptionAmount = corruptionToNextTier - overflow;
    }

    void HandleTierChange(){
        corruptionToNextTier = Mathf.RoundToInt(corruptionScaling.Evaluate((int)currentTier));
        changeCorruptionTier?.Invoke(currentTier);
    }

}
