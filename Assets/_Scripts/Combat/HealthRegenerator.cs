using System.Collections;
using UnityEngine;

public class HealthRegenerator : MonoBehaviour
{
    public float RegenTime {get; private set;} = 6f;
    public float RegenAmount {get; private set;} = 1f;


    private HealthSystem _health;
    
    public void ChangeRegenAmount(float amount){
        RegenAmount += amount;
    }

    public void ChangeRegenTime(float amount){
        RegenTime -= amount;
    }
    
    void Start(){
        _health = GetComponent<HealthSystem>();
        StartCoroutine(RegenerateHealth());
    }

    IEnumerator RegenerateHealth(){
        yield return new WaitForSeconds(RegenTime);
        _health.InceaseCurrentHealth(RegenAmount);
        StartCoroutine(RegenerateHealth());
    }
}
