using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public static Observer Instance;

    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;


    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }


    public void AddExperience(int amount){
        OnExperienceChange?.Invoke(amount);
    }

}
