using UnityEngine;
using TMPro;


public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] TMP_Text _dmgText;
    [SerializeField] TMP_Text _critDmgText;
    [SerializeField] TMP_Text _critChanceText;
    [SerializeField] TMP_Text _speedText;
    [SerializeField] TMP_Text _xpMultText;
    [SerializeField] TMP_Text _goldMultText;

    [SerializeField] GameObject _statsPanel;

    void OnEnable() => PlayerStatModifier.onStatChange += UpdateStatsText;
    void OnDisable() => PlayerStatModifier.onStatChange -= UpdateStatsText;

    void Start(){
        UpdateStatsText();
    }

    void UpdateStatsText(){
        _dmgText.text = (PlayerStatModifier.MOD_DamageDone * 100).ToString() + "%";
        _critDmgText.text = (PlayerStatModifier.MOD_CritDamage * 100).ToString() + "%";
        _critChanceText.text = PlayerStatModifier.MOD_CritChance.ToString() + "%";
        _speedText.text = (PlayerStatModifier.MOD_MoveSpeed * 100).ToString() + "%";
        _xpMultText.text = (PlayerStatModifier.MOD_AdditonalXP * 100).ToString() + "%";
        _goldMultText.text = (PlayerStatModifier.MOD_AdditionalGold * 100).ToString() + "%";
    }

    public void ChangePanelVisiblity(){
        if(_statsPanel.activeInHierarchy) _statsPanel.SetActive(false);
        else _statsPanel.SetActive(true);
    }
}