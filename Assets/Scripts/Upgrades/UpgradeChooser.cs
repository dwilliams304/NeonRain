using System.Collections.Generic;
using UnityEngine;

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

    void OpenPanel(){
        upgradePanel.SetActive(true);
        controller.SetTrigger("Enabled");
        textController.SetTrigger("Enabled");
        Time.timeScale = 0f;
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
        return Random.Range(0, 101); //Self explanatory
    }

    List<PlayerUpgrades> ReturnedList(int roll){ //Return a list based off of a given roll
        if(roll <= 40 && roll > 15){ //25%
            return uncommonUpgrades;
        }else if(roll <= 15 && roll > 5){ //10%
            return rareUpgrades;
        }else if(roll <= 5){
            return legendaryUpgrades; //5%
        }else{
            return commonUpgrades; //60%
        }
    }

    int RollForChoice(List<PlayerUpgrades> list){ //Return a random upgrade from a given list
        return Random.Range(0, list.Count);
    }




    public void GenerateUpgrades(){
        int i = 0;
        while(i < 3){ //Generate 3 upgrades
            List<PlayerUpgrades> upgradeList = ReturnedList(RollForRarity());
            int choiceRoll = RollForChoice(upgradeList);
            upgradeButtons[i].SwitchUpgrade(upgradeList[choiceRoll]);
            usedUpgrades.Add(upgradeList[choiceRoll]);
            upgradeList.RemoveAt(choiceRoll);
            i++;
        }
    }


    public void UpgradeComplete(){
        //Reset all the lists to use again
        possibleUpgrades.AddRange(usedUpgrades); 
        usedUpgrades.Clear();
        SortUpgrades();
        controller.SetTrigger("Disabled");
        textController.SetTrigger("Disabled");
        Time.timeScale = 1f;
    }
}
