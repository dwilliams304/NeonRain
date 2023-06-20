using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public List<GameObject> CommonDrops;
    public List<GameObject> UncommonDrops;
    public List<GameObject> RareDrops;
    public List<GameObject> LegendaryDrops;
    public List<GameObject> UniqueCorruptedDrops;



    public int commonWeight = 55;
    public int uncommonWeight = 30;
    public int rareWeight = 10;
    public int legendaryWeight = 1;
    public int uniqueCorruptedWeight = 4;

    [SerializeField] private int _totalWeights;
    [SerializeField] private int commonPct;
    [SerializeField] private int uncommonPct;
    [SerializeField] private int rarePct;
    [SerializeField] private int legendaryPct;
    [SerializeField] private int uniqueCorruptedPct;
    

    void Start(){
        _totalWeights = commonWeight + uncommonWeight + rareWeight + legendaryWeight + uniqueCorruptedWeight;
        CalculateLootDrop();
    }
    public void CalculateLootDrop(){
        int rarity = Random.Range(0, _totalWeights);
        
        Debug.Log("Calculation: " + rarity);
    }

}
