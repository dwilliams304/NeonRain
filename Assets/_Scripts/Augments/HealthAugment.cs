using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthAugment : AugmentBase
{
    void Start(){
        AugButton = GetComponent<Button>();
        BoughtText = GetComponentInChildren<TMP_Text>();
    }

    public override void UnlockAugment()
    {
        TotalBought++;
        BoughtText.text = TotalBought.ToString();
        Debug.Log("Clicked " + this.name);
    }
}
