using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChooser : MonoBehaviour
{
    public List<UpgradeHolder> upgradeButtons;
    public List<PlayerUpgrades> possibleUpgrades;

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

    void OpenPanel(){
        upgradePanel.SetActive(true);
        controller.SetTrigger("Enabled");
        textController.SetTrigger("Enabled");
        Time.timeScale = 0f;
    }



    public void GenerateUpgrades(){
        int roll1 = Random.Range(0, possibleUpgrades.Count);
        upgradeButtons[0].upgrade = possibleUpgrades[roll1];
        Debug.Log(possibleUpgrades[roll1]);
        upgradeButtons[0].SwitchUpgrade(possibleUpgrades[roll1]);
        usedUpgrades.Add(possibleUpgrades[roll1]);
        possibleUpgrades.RemoveAt(roll1);



        int roll2 = Random.Range(0, possibleUpgrades.Count);
        upgradeButtons[1].upgrade = possibleUpgrades[roll2];
        Debug.Log(possibleUpgrades[roll2]);

        upgradeButtons[1].SwitchUpgrade(possibleUpgrades[roll2]);

        usedUpgrades.Add(possibleUpgrades[roll2]);
        possibleUpgrades.RemoveAt(roll2);



        int roll3 = Random.Range(0, possibleUpgrades.Count);
        upgradeButtons[2].upgrade = possibleUpgrades[roll3];
        Debug.Log(possibleUpgrades[roll3]);

        upgradeButtons[2].SwitchUpgrade(possibleUpgrades[roll3]);

        usedUpgrades.Add(possibleUpgrades[roll3]);
        possibleUpgrades.RemoveAt(roll3);


    }


    public void UpgradeComplete(){
        possibleUpgrades.AddRange(usedUpgrades);
        usedUpgrades.Clear();
        controller.SetTrigger("Disabled");
        textController.SetTrigger("Disabled");
        Time.timeScale = 1f;
    }
}
