using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDUI : MonoBehaviour
{
    [SerializeField] private Slider _xpBar;
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private TMP_Text _xpIncreaseText;
    [SerializeField] private TMP_Text _goldIncreaseText;
    [SerializeField] private TMP_Text _goldActualText;
    [SerializeField] Animation _xpIncrease;
    [SerializeField] Animation _goldIncrease;

    void OnEnable(){
        LevelSystem.onXPChange += UpdateXPBar;
        LevelSystem.onLevelChange += IncreaseLevel;
        LevelSystem.addXP += ShowXPText;
        Inventory.addGold += ShowGoldText;
    }

    void OnDisable(){
        LevelSystem.onXPChange -= UpdateXPBar;
        LevelSystem.onLevelChange -= IncreaseLevel;
        LevelSystem.addXP -= ShowXPText;
        Inventory.addGold -= ShowGoldText;
    }

    void UpdateXPBar(int cur, int max){
        _xpBar.value = cur;
        _xpBar.maxValue = max;
    }

    void ShowXPText(int amount){
        _xpIncreaseText.text = "+" + amount;
        _xpIncrease.Play();
    }

    void IncreaseLevel(int level){
        _lvlText.text = "Level " + level;
    }

    void ShowGoldText(int amount, int totalGold){
        bool removing = totalGold < 0 ? true : false;
        _goldActualText.text = totalGold.ToString();
        _goldIncreaseText.text = removing ? "-" + amount : "+" + amount;
        _goldIncreaseText.color = removing ? Color.red : Color.yellow;
        _goldIncrease.Play();
    }
}
