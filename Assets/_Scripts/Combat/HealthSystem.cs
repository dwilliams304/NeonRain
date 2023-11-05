using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public delegate void TakeDamage(float amount);
    public TakeDamage onDamage;

    public delegate void OnDeath();
    public OnDeath onDeath;

    [SerializeField] private Slider _healthBar;

    // //FOR TESTING
    // void Start(){
    //     SetMaxHealth(100);
    // }
    // void Update(){
    //     if(Input.GetKeyDown(KeyCode.G)){
    //         DecreaseCurrentHealth(5);
    //     }
    // }

    public void SetMaxHealth(float amount){ //This will only SET the max health, not add to its current value
        MaxHealth = Mathf.RoundToInt(amount);
        CurrentHealth = MaxHealth;
        SetHealthBarMaxValue(MaxHealth);
    }


    public void IncreaseMaxHealth(float amount){ //Add onto current maxhealth value
        MaxHealth += Mathf.RoundToInt(amount);
        CurrentHealth = MaxHealth;
        SetHealthBarMaxValue(MaxHealth);
    }

    public void InceaseCurrentHealth(float amount){
        CurrentHealth += Mathf.RoundToInt(amount);
        if(CurrentHealth > MaxHealth) {
            CurrentHealth = MaxHealth;
        }
        ChangeHealthBarValue(CurrentHealth);
    }


    public void DecreaseCurrentHealth(float amount){
        CurrentHealth -= Mathf.RoundToInt(amount);
        onDamage?.Invoke(amount);
        if(CurrentHealth <= 0){
            CurrentHealth = 0;
            ChangeHealthBarValue(0);
            onDeath?.Invoke();
        }
        else{
            ChangeHealthBarValue(CurrentHealth);
        }
    }

    void ChangeHealthBarValue(int amount){
        if(_healthBar == null){ //Allow for something to not have a healthbar
            return;
        }
        _healthBar.value = amount;
    }

    void SetHealthBarMaxValue(int amount){
        if(_healthBar == null){
            return;
        }
        _healthBar.maxValue = amount;
        _healthBar.value = amount;
        
    }

}
