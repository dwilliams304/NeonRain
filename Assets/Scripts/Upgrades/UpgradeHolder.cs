using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeHolder : MonoBehaviour
{
    public PlayerUpgrades upgrade;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text buttonText;


    [SerializeField] private GameObject toolTipObject;
    [SerializeField] private TMP_Text toolTipText;



    void Start(){
        buttonText = upgradeButton.GetComponentInChildren<TMP_Text>();
        toolTipObject.SetActive(false);
    }

    public void SwitchUpgrade(PlayerUpgrades upgradeGenerated){
        upgrade = upgradeGenerated;
        buttonText.text = upgrade.upgradeName;
        toolTipText.text = upgrade.toolTipDescription;
    }

    public void OnClick(){
        upgrade.UpgradeChosen();
        toolTipObject.SetActive(false);
    }
}
