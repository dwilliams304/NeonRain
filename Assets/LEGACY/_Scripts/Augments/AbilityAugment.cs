using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityAugment : AugmentBase
{
    void Start(){
        AugButton = GetComponent<Button>();
    }

    public override void UnlockAugment()
    {
        if(!Purchased){
            Purchased = true;
            Debug.Log("Newly purchased!");
        }
        else Debug.Log("Already purchased, can't do it again boss.");
    }
}
