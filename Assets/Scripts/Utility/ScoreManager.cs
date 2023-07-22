using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;



    public static ScoreManager scoreManager;

    [SerializeField] TMP_Text inGameScoreText;
    [SerializeField] TMP_Text leaderboardScoreText;


    void Awake(){
        scoreManager = this;
    }
    void OnEnable(){
        PlayerStats.onPlayerDeath += UpdateEndGameScoreText;
        KillTimer.timerCompleted += UpdateEndGameScoreText;
    }
    void OnDisable(){
        PlayerStats.onPlayerDeath -= UpdateEndGameScoreText;
        KillTimer.timerCompleted -= UpdateEndGameScoreText;
    }


    public void AddToScore(int amount){
        currentScore += amount;
        UpdateScoreText(currentScore);
    }

    void UpdateScoreText(int amount){
        inGameScoreText.text = "Score: " + amount.ToString();
    }

    void UpdateEndGameScoreText(){
        leaderboardScoreText.text = currentScore.ToString();
    }
}
