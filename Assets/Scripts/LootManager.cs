using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootManager : MonoBehaviour
{
    public List<GameObject> CommonDrops;
    public List<GameObject> UncommonDrops;
    public List<GameObject> RareDrops;
    public List<GameObject> LegendaryDrops;
    public List<GameObject> UniqueCorruptedDrops;
    public GameObject uniqueWeapon;

    public GameObject _dev_Panel;


    private int commonDropChance = 10000;
    private int uncommonDropChance = 4000;
    private int rareDropChance = 1500;
    private int legendaryDropChance = 250;
    private int corruptedDropChance = 500;
    private int uniqueDropChance = 1;
    
    [SerializeField] private TMP_Text _dev_CommonText;
    [SerializeField] private TMP_Text _dev_UncommonText;
    [SerializeField] private TMP_Text _dev_RareText;
    [SerializeField] private TMP_Text _dev_CorruptedText;
    [SerializeField] private TMP_Text _dev_LegendaryText;
    [SerializeField] private TMP_Text _dev_Unique;
    [SerializeField] private TMP_Text _dev_TotalRolls;

    private float _dev_CommonDrops = 0;
    private float _dev_UncommonDrops = 0;
    private float _dev_RareDrops = 0;
    private float _dev_CorruptedDrops = 0;
    private float _dev_LegendaryDrops = 0;
    private float _dev_UniqueDrops = 0;


    float commAvg;
    float uncommAvg;
    float rareAvg;
    float corruptAvg;
    float legendAvg;
    float uniqueAvg;

    bool debugEnabled = false;

    private float _dev_TimesPressed = 0;
    void Start(){
        //_totalWeights = commonWeight + uncommonWeight + rareWeight + legendaryWeight + uniqueCorruptedWeight;
        _dev_TimesPressed++;
        CalculateLootDrop();
    }

    void Update(){
        if(debugEnabled){
            if(Input.GetKey(KeyCode.F)){
                _dev_TimesPressed++;
                CalculateLootDrop();
            }
        }
        if(Input.GetKeyDown(KeyCode.BackQuote)){
            debugEnabled = !debugEnabled;
            OpenPanel();
        }
    }
    public void CalculateLootDrop(){
        commAvg = _dev_CommonDrops / _dev_TimesPressed * 100;
        uncommAvg = _dev_UncommonDrops / _dev_TimesPressed * 100;
        rareAvg = _dev_RareDrops / _dev_TimesPressed * 100;
        corruptAvg = _dev_CorruptedDrops / _dev_TimesPressed * 100;
        legendAvg = _dev_LegendaryDrops / _dev_TimesPressed * 100;
        uniqueAvg = _dev_UniqueDrops / _dev_TimesPressed * 100;
        _dev_CommonText.text = $"Common: {_dev_CommonDrops} -> Avg: {commAvg}%";
        _dev_UncommonText.text = $"Uncommon: {_dev_UncommonDrops} -> Avg: {uncommAvg}%";
        _dev_RareText.text = $"Rare: {_dev_RareDrops} -> Avg: {rareAvg}%";
        _dev_CorruptedText.text = $"Corrupted: {_dev_CorruptedDrops} -> Avg: {corruptAvg}%";
        _dev_LegendaryText.text = $"Legendary: {_dev_LegendaryDrops} -> Avg: {legendAvg}%";
        _dev_Unique.text = $"Unique: {_dev_UniqueDrops} -> Avg: {uniqueAvg}%";
        int rarity = Random.Range(0, 10001);
        //Debug.Log("Calculation: " + rarity);
        _dev_TotalRolls.text = $"Total drops: {_dev_TimesPressed}";
        if(rarity <= commonDropChance && rarity > uncommonDropChance){
            _dev_CommonDrops++;
        }
        else if(rarity <= uncommonDropChance && rarity > rareDropChance){
            _dev_UncommonDrops++;
        }
        else if(rarity <= rareDropChance && rarity > corruptedDropChance){
            _dev_RareDrops++;
        }
        else if(rarity <= corruptedDropChance && rarity > legendaryDropChance){
            _dev_CorruptedDrops++;
        }
        else if(rarity <= legendaryDropChance && rarity != uniqueDropChance){
            _dev_LegendaryDrops++;
        }
        else if(rarity == uniqueDropChance){
            _dev_UniqueDrops++;
        }
    }

    void OpenPanel(){
        _dev_Panel.SetActive(debugEnabled);
    }
    public void DropGold(int minAmnt, int maxAmnt){

    }

}
