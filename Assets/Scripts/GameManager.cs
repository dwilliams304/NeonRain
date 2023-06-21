using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum CorruptionTier{
        Tier0,
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5
    }

    private int tier1Breakpoint = 100;
    private int tier2Breakpoint = 200;
    private int tier3Breakpoint = 300;
    private int tier4Breakpoint = 400;
    private int tier5Breakpoint = 500;
    
    private PlayerStats playerStats;
    public static GameManager gameManager;
    void Awake(){
        gameManager = this;
    }
    void Start(){
        playerStats = PlayerStats.playerStats;
    }

    public void CheckCorruptionTier(){
        int currentCorruption = playerStats.PlayerCorruptionLevel;
        if(currentCorruption >= tier5Breakpoint){
            CorruptionTierFive();
        }
        else if(currentCorruption >= tier4Breakpoint){
            CorruptionTierFour();
        }
        else if(currentCorruption >= tier3Breakpoint){
            CorruptionTierThree();
        }
        else if(currentCorruption >= tier2Breakpoint){
            CorruptionTierTwo();
        }
        else if(currentCorruption >= tier1Breakpoint){
            CorruptionTierOne();
        }
        else{
            CorruptionTierZero();
        }
    }


    public void CorruptionTierZero(){
        Debug.Log("Corruption Tier 0!");
    }

    public void CorruptionTierOne(){
        Debug.Log("Corruption Tier 1!");

    }

    public void CorruptionTierTwo(){
        Debug.Log("Corruption Tier 2!");

    }

    public void CorruptionTierThree(){
        Debug.Log("Corruption Tier 3!");

    }

    public void CorruptionTierFour(){
        Debug.Log("Corruption Tier 4!");

    }

    public void CorruptionTierFive(){
        Debug.Log("Corruption Tier 5!");

    }

    
    
    
}

