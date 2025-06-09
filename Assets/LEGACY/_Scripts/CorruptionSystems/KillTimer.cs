using UnityEngine;
using TMPro;

public class KillTimer : MonoBehaviour
{
    [Header("Timer Components")]
    [SerializeField] GameObject timerTextObject;
    [SerializeField] TMP_Text timerText;

    [Header("Timer Settings")]
    public float currentTime;

    bool timerActive;

    public delegate void TimerCompleted();
    public static TimerCompleted timerCompleted;



    void Update(){
        if(!timerActive){
            return;
        }else{
            currentTime = currentTime -= Time.deltaTime;
            timerText.text = currentTime.ToString("0.00");
            if(currentTime <= 0){
                StopTimer();
                timerCompleted?.Invoke();
            }
        }
    }

    public void StartTimer(){
        timerTextObject.SetActive(true);
        timerActive = true;
        currentTime = 90f;

    }
    public void StopTimer(){
        timerTextObject.SetActive(false);
        timerActive = false;
        currentTime = 90f;
        
    }

}
