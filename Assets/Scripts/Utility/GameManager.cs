using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum CorruptionTier{
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5
    }
    private CorruptionTier corruptionTier;

    // private int tier1Breakpoint = 100;
    // private int tier2Breakpoint = 200;
    // private int tier3Breakpoint = 300;
    // private int tier4Breakpoint = 400;
    // private int tier5Breakpoint = 500;
    
    public static GameManager gameManager;
    void Awake(){
        gameManager = this;
        Time.timeScale = 1;
    }
    
    void OnEnable(){
        PlayerStats.onPlayerDeath += LoseGame;
    }
    void OnDisable(){
        PlayerStats.onPlayerDeath -= LoseGame;
    }

    public void LoseGame(){
        Time.timeScale = 0;
        UIManager.uiManagement.LoseGameUI();
    }

    void Update(){
        switch(corruptionTier){
            case CorruptionTier.Tier1:
                break;
            case CorruptionTier.Tier2:
                break;
            case CorruptionTier.Tier3:
                break;
            case CorruptionTier.Tier4:
                break;
            case CorruptionTier.Tier5:
                break;
            
        }
    }

    void CheckCorruptionTier(int corruptionAmount){
        
    }
    

}

