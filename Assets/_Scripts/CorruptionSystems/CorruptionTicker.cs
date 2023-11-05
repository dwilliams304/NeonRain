using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionTicker : MonoBehaviour
{
    public static CorruptionTicker Instance;

    public int tickAmount = 0;
    public float tickTimer = 5f;

    bool tickingActive = false;

    void Awake() => Instance = this;

    void Update(){
        if(Input.GetKeyDown(KeyCode.H)){
            AddToTickAmount(10);
        }
    }

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
