using System.Collections;
using UnityEngine;

public class CorruptionTicker : MonoBehaviour
{
    public static CorruptionTicker Instance;

    public int tickAmount = 0;
    public float tickTimer = 5f;

    bool tickingActive = false;

    void Awake() => Instance = this;


    public void AddToTickAmount(int amnt){
        tickAmount += amnt;
        if(!tickingActive){
            StartCoroutine(TickCorruption());
            tickingActive = true;
        }
    }


    IEnumerator TickCorruption(){
        yield return new WaitForSeconds(tickTimer);
        CorruptionManager.Instance.IncreaseCorruptionAmount(tickAmount);
        if(tickAmount != 0){
            StartCoroutine(TickCorruption());
        }
        else{
            tickingActive = false;
            StopCoroutine(TickCorruption());
        }
    }
}
