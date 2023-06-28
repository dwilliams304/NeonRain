using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance;

    public delegate void XPChangeHandler(int amount);
    public event XPChangeHandler onXPChange;

    void Awake(){
        if(Instance != null & Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
    }

    public void AddExperience(int amount){
        onXPChange?.Invoke(amount);
    }
}
