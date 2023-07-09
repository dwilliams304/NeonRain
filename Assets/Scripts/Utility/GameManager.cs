using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    void LoseGame(){
        Time.timeScale = 0;
    }
    

}

