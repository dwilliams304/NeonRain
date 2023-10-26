using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public delegate void ChangeCorruptionTier(CorruptionTier tier);
    public static ChangeCorruptionTier changeCorruptionTier;

    public int currentCorruptionAmount {get; private set;} = 0;
    public int corruptionToNextTier {get; private set;} = 100;
    [SerializeField] private AnimationCurve corruptionScaling;

    public CorruptionTier currentTier = CorruptionTier.Tier0;

    void Awake() => Instance = this;

    public void IncreaseCorruptionAmount(int amount){
        currentCorruptionAmount += amount;
        if(currentCorruptionAmount >= corruptionToNextTier){
            int overflow = currentCorruptionAmount - corruptionToNextTier;
            if(currentTier == CorruptionTier.Tier5){
                currentCorruptionAmount = corruptionToNextTier;
            }
            else{
                GoToNextTier(currentTier, overflow);
            }
        }
    }

    public void DecreaseCorruptionAmount(int amount){
        currentCorruptionAmount -= amount;
        if(currentCorruptionAmount < 0){
            int overflow = -currentCorruptionAmount;
            if(currentTier == CorruptionTier.Tier0){
                currentCorruptionAmount = 0;
            }
            else{
                GoDownATier(currentTier, overflow);
            }
        }
    }

    public void ForceIncreaseTier() => GoToNextTier(currentTier, 0);

    public void ForceDecreaseTier() => GoDownATier(currentTier, 0);
    

    void GoToNextTier(CorruptionTier tier, int overflow){
        currentCorruptionAmount = overflow;
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
                break;
            //Dont include Tier 5 as you can't go beyond that tier
        }
        changeCorruptionTier?.Invoke(currentTier);
    }

    void GoDownATier(CorruptionTier tier, int overflow){
        currentCorruptionAmount = corruptionToNextTier - overflow;
        switch(tier){
            case CorruptionTier.Tier5:
                currentTier = CorruptionTier.Tier4;
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
        changeCorruptionTier?.Invoke(currentTier);
    }

}
