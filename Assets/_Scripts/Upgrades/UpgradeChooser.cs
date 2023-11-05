using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeChooser : MonoBehaviour
{
    public List<UpgradeHolder> upgradeButtons;
    public List<PlayerUpgrades> possibleUpgrades;

    private List<PlayerUpgrades> commonUpgrades = new List<PlayerUpgrades>();
    private List<PlayerUpgrades> uncommonUpgrades = new List<PlayerUpgrades>();
    private List<PlayerUpgrades> rareUpgrades = new List<PlayerUpgrades>();
    private List<PlayerUpgrades> legendaryUpgrades = new List<PlayerUpgrades>();

    private List<PlayerUpgrades> usedUpgrades = new List<PlayerUpgrades>();

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Animator controller;
    [SerializeField] private Animator textController;

    [SerializeField] TMP_Text rerollCostText;
    [SerializeField] GameObject notEnoughGoldText;
    [SerializeField] Button rerollButton;
    [SerializeField] AnimationCurve rerollCostIncrease;

    int reRollCost;
    int amntOfRerolls = 0;
    float currentScale = 0f;


    void OnEnable(){
        PlayerStats.handleLevelIncrease += GenerateUpgrades;
        PlayerStats.handleLevelIncrease += OpenPanel;
    }
    void OnDisable(){
        PlayerStats.handleLevelIncrease -= GenerateUpgrades;
        PlayerStats.handleLevelIncrease -= OpenPanel;
    }

    void Start(){
        SortUpgrades();
    }

    void OpenPanel(int x){
        upgradePanel.SetActive(true);
        controller.SetTrigger("Enabled");
        textController.SetTrigger("Enabled");
        currentScale = Time.timeScale;
        Time.timeScale = 0f;
        UIManager.Instance.upgradePanelActive = true;
    }

    void SortUpgrades(){
        foreach(PlayerUpgrades upgrade in possibleUpgrades){
            switch(upgrade.rarity){
                case Rarity.Common:
                    commonUpgrades.Add(upgrade);
                break;
                
                case Rarity.Uncommon:
                    uncommonUpgrades.Add(upgrade);
                break;
                
                case Rarity.Rare:
                    rareUpgrades.Add(upgrade);
                break;
                
                case Rarity.Legendary:
                    legendaryUpgrades.Add(upgrade);
                break;
            }
        }
        possibleUpgrades.Clear();
    }


    int RollForRarity(){
        return Random.Range(0, 201); //Self explanatory
    }

    List<PlayerUpgrades> ReturnedList(int roll){ //Return a list based off of a given roll
        if(roll <= 70 && roll > 15){ 
            return uncommonUpgrades;
        }else if(roll <= 15 && roll > 5){ 
            return rareUpgrades;
        }else if(roll <= 5){
            return legendaryUpgrades; 
        }else{
            return commonUpgrades; 
        }
    }

    int RollForChoice(List<PlayerUpgrades> list){ //Return a random upgrade from a given list
        return Random.Range(0, list.Count);
    }


    public void ReRoll(){
        reRollCost = Mathf.RoundToInt(100 * PlayerStats.Instance.CurrentLevel / 2);
        Inventory.Instance.RemoveGold(reRollCost);
        amntOfRerolls++;
        GenerateUpgrades(0);
    }

    public void GenerateUpgrades(int x){
        ClearUpgrades();
        SortUpgrades();
        int i = 0;
        reRollCost = Mathf.RoundToInt(100 * PlayerStats.Instance.CurrentLevel / 3 * rerollCostIncrease.Evaluate(amntOfRerolls));
        rerollCostText.text = "cost: " + reRollCost.ToString() + "g";
        if(Inventory.Instance.PlayerGold >= reRollCost){
            rerollButton.interactable = true;
            notEnoughGoldText.SetActive(false);
        }else{
            rerollButton.interactable = false;
            notEnoughGoldText.SetActive(true); 
        }
        while(i < 3){ //Generate 3 upgrades
            List<PlayerUpgrades> upgradeList = ReturnedList(RollForRarity());
            int choiceRoll = RollForChoice(upgradeList);
            upgradeButtons[i].SwitchUpgrade(upgradeList[choiceRoll]);
            usedUpgrades.Add(upgradeList[choiceRoll]);
            upgradeList.RemoveAt(choiceRoll);
            i++;
        }
    }

    void ClearUpgrades(){
        possibleUpgrades.AddRange(usedUpgrades); 
        usedUpgrades.Clear();
    }


    public void UpgradeComplete(){
        //Reset all the lists to use again
        ClearUpgrades();
        SortUpgrades();
        controller.SetTrigger("Disabled");
        textController.SetTrigger("Disabled");
        Time.timeScale = currentScale;
        amntOfRerolls = 0;
        UIManager.Instance.upgradePanelActive = false;
    }
}
