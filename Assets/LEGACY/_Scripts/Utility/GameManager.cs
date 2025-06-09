using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private GameObject player;

    public enum GameState {
        Loading,
        Spawning,
        Start,
        Pause,
        Lose
    }
    
    void Awake(){
        gameManager = this;
        Time.timeScale = 1;
    }
    
    void OnEnable(){
        KillTimer.timerCompleted += LoseGame;
    }
    void OnDisable(){
        KillTimer.timerCompleted -= LoseGame;
    }

    void LoseGame(){
        Time.timeScale = 0;
        player.SetActive(false);
        UIManager.Instance.gameLost = true;
    }
    

}

