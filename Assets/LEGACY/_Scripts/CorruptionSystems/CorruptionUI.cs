using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CorruptionUI : MonoBehaviour
{
    [SerializeField] private Slider _corruptionBar;
    [SerializeField] private TMP_Text _corruptionTierText;
    [SerializeField] private Image _corruptionTierIcon;
    
    [System.Serializable]
    public class CorruptionInfo{
        public Color color;
        public Sprite icon;
    }

    [SerializeField] private List<CorruptionInfo> corruptionInfo;

    void OnEnable(){
        CorruptionManager.addCorruption += ChangeBarAmount;
        CorruptionManager.changeCorruptionTier += ChangeTierDisplay;
    }
    void OnDisable(){
        CorruptionManager.addCorruption -= ChangeBarAmount;
        CorruptionManager.changeCorruptionTier -= ChangeTierDisplay;
    }

    void ChangeBarAmount(int cur, int max){
        _corruptionBar.maxValue = max;
        _corruptionBar.value = cur;
    }

    void ChangeTierDisplay(CorruptionTier tier){
        int i = (int)tier;
        _corruptionTierText.text = "Tier " + i;
        _corruptionTierIcon.sprite = corruptionInfo[i].icon;
        _corruptionTierText.color = corruptionInfo[i].color;
        _corruptionTierIcon.color = corruptionInfo[i].color;
    }
}
