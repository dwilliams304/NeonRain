using System.Collections;
using UnityEngine;

public class HealthRegenerator : MonoBehaviour
{
    public float RegenTime = 6f;
    public float RegenAmount = 1f;

    public static HealthRegenerator Instance;

    void Awake(){
        Instance = this;
    }
    
    void Start(){
        StartCoroutine(RegenerateHealth());
    }

    IEnumerator RegenerateHealth(){
        yield return new WaitForSeconds(RegenTime);
        PlayerStats.playerStats.IncreaseHealth(RegenAmount);
        StartCoroutine(RegenerateHealth());
    }
}
