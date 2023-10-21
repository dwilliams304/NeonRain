using UnityEngine;
using UnityEngine.UI;

public class HealthBehavior : MonoBehaviour
{

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    [SerializeField] private Slider _healthBar;

    public void SetMaxHealth(int amount){
        MaxHealth = amount;
        CurrentHealth = MaxHealth;
        SetHealthBarMaxValue(MaxHealth);
    }


    public void IncreaseMaxHealth(float amount){
        MaxHealth += Mathf.RoundToInt(amount);
        CurrentHealth = MaxHealth;
    }

    public void InceaseCurrentHealth(float amount){
        CurrentHealth += Mathf.RoundToInt(amount);
        if(CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        ChangeHealthBarValue(CurrentHealth * -1); //Flip to negative to add
    }


    public void DecreaseCurrentHealth(float amount){
        CurrentHealth -= Mathf.RoundToInt(amount);
        if(CurrentHealth <= 0){
            CurrentHealth = 0;
            Die();
        }
        ChangeHealthBarValue(CurrentHealth);
    }

    void ChangeHealthBarValue(int amount){
        _healthBar.value -= amount;
    }

    void SetHealthBarMaxValue(int amount){
        _healthBar.maxValue = amount;
        _healthBar.value = amount;

    }


    void Die(){
        Debug.LogError("Kill me please!");
        // this.gameObject.SetActive(false);
    }
}
