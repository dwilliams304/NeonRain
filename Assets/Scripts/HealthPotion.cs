using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    [SerializeField] private int amountOfPotions;

    [SerializeField] private int healthToAdd;
    
    public float HealthPotionModifier = 1f;


    public delegate void AddHealth(int healthToAdd, int amountOfPotions);
    public static AddHealth addHealth;


    void ModifiyHealthToAdd(){
        healthToAdd = Mathf.FloorToInt(healthToAdd / HealthPotionModifier);
    }
    void Start(){
        
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.H) && amountOfPotions != 0){
            amountOfPotions--;
            addHealth?.Invoke(healthToAdd, amountOfPotions);
        }
    }

}
