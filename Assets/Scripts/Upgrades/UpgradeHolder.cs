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
    private Image buttonImg;



    void Start(){
        buttonText = upgradeButton.GetComponentInChildren<TMP_Text>();
        toolTipObject.SetActive(false);
    }

    public void SwitchUpgrade(PlayerUpgrades upgradeGenerated){
        upgrade = upgradeGenerated;
        buttonText.text = upgrade.upgradeName;
        toolTipText.text = upgrade.toolTipDescription;
        buttonImg = upgradeButton.GetComponent<Image>();
        switch(upgrade.rarity){
            case Rarity.Common:
                buttonImg.color = Color.white;
            break;

            case Rarity.Uncommon:
                buttonImg.color = Color.green;
            break;

            case Rarity.Rare:
                buttonImg.color = Color.blue;
            break;
            
            case Rarity.Legendary:
                buttonImg.color = Color.yellow;
            break;
        }
    }

    public void OnClick(){
        upgrade.UpgradeChosen();
        toolTipObject.SetActive(false);
    }
}
