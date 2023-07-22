using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private GameObject player;

    
    void Awake(){
        gameManager = this;
        Time.timeScale = 1;
    }
    
    void OnEnable(){
        PlayerStats.onPlayerDeath += LoseGame;
        KillTimer.timerCompleted += LoseGame;
    }
    void OnDisable(){
        PlayerStats.onPlayerDeath -= LoseGame;
        KillTimer.timerCompleted -= LoseGame;
    }

    void LoseGame(){
        Time.timeScale = 0;
        player.SetActive(false);
        UIManager.uiManagement.gameLost = true;
    }
    

}

