using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionTester : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField] private GameManager gameManager;
    void Start(){
        playerStats = PlayerStats.playerStats;
        //gameManager = GameManager.gameManager;
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            AddCorruption();
            gameManager.CheckCorruptionTier();
        }
    }



    void AddCorruption(){
        playerStats.PlayerCorruptionLevel += 10;
        Debug.Log("Corruption added! New corruption level: " + playerStats.PlayerCorruptionLevel);
    }
}
