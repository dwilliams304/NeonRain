using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevelUI : MonoBehaviour
{
    [SerializeField] private Slider _xpBar;
    [SerializeField] private TMP_Text _lvlText;

    void OnEnable(){
        LevelSystem.onXPChange += IncreaseExperience;
        LevelSystem.onLevelChange += IncreaseLevel;
    }

    void OnDisable(){
        LevelSystem.onXPChange -= IncreaseExperience;
        LevelSystem.onLevelChange -= IncreaseLevel;
    }


    void IncreaseExperience(int cur, int max){
        _xpBar.value = cur;
        _xpBar.maxValue = max;
    }

    void IncreaseLevel(int level){
        _lvlText.text = "Level " + level;
    }
}
