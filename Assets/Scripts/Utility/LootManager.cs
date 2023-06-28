using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootManager : MonoBehaviour
{
    public static LootManager lootManager;
    //public delegate void DropLoot(Vector3 position, float additionalLuck);
    //public DropLoot dropLoot;
    [Header("Loot Pool")]
    [SerializeField] private List<Weapon> CommonDrops;
    [SerializeField] private List<Weapon> UncommonDrops;
    [SerializeField] private List<Weapon> RareDrops;
    [SerializeField] private List<Weapon> LegendaryDrops;
    [SerializeField] private List<Weapon> UniqueCorruptedDrops;
    [SerializeField] private Weapon uniqueWeapon;


    [Header("Loot Drop Prefabs")]
    [SerializeField] private GameObject commonPrefab;
    [SerializeField] private GameObject uncommonPrefab;
    [SerializeField] private GameObject rarePrefab;
    [SerializeField] private GameObject corruptedPrefab;
    [SerializeField] private GameObject legendaryPrefab;
    private GameObject lootPrefab;

    public GameObject _dev_Panel;


    private int commonDropChance = 100000;
    private int uncommonDropChance = 40000;
    private int rareDropChance = 12500;
    private int legendaryDropChance = 250;
    private int corruptedDropChance = 2500;
    private int uniqueDropChance = 1;


    private DEVTools _DEVTOOLS_;


    void Awake(){
        lootManager = this;
    }
    void Start(){
        lootPrefab = new GameObject();
        _DEVTOOLS_ = DEVTools.__DEV;
        //dropLoot = SpawnLoot;
    }

    public void DropLoot(Vector3 position, float additionalLuck){
        _DEVTOOLS_.totalRolls++;

        LootObject lootObj;
        float rarity = Random.Range(0, 100001);
        rarity = Mathf.Ceil(rarity / additionalLuck);
        if(rarity <= commonDropChance && rarity > uncommonDropChance){
            int i = Random.Range(0, CommonDrops.Count);
            lootPrefab = commonPrefab;
            lootObj = lootPrefab.GetComponent<LootObject>();
            lootObj.weaponData = CommonDrops[i];
            //DEV ONLY
            _DEVTOOLS_.amntCommonDrops++;
        }
        else if(rarity <= uncommonDropChance && rarity > rareDropChance){
            int i = Random.Range(0, UncommonDrops.Count);
            lootPrefab = uncommonPrefab;
            lootObj = lootPrefab.GetComponent<LootObject>();
            lootObj.weaponData = UncommonDrops[i];
            //DEV ONLY
            _DEVTOOLS_.amntUncommonDrops++;
        }
        else if(rarity <= rareDropChance && rarity > corruptedDropChance){
            int i = Random.Range(0, RareDrops.Count);
            lootPrefab = rarePrefab;
            lootObj = lootPrefab.GetComponent<LootObject>();
            lootObj.weaponData = RareDrops[i];
            //DEV ONLY
            _DEVTOOLS_.amntRareDrops++;
        }
        else if(rarity <= corruptedDropChance && rarity > legendaryDropChance){
            int i = Random.Range(0, UniqueCorruptedDrops.Count);
            lootPrefab = corruptedPrefab;
            lootObj = lootPrefab.GetComponent<LootObject>();
            lootObj.weaponData = UniqueCorruptedDrops[i];
            //DEV ONLY
            _DEVTOOLS_.amntCorruptDrops++;
        }
        else if(rarity <= legendaryDropChance && rarity != uniqueDropChance){
            int i = Random.Range(0, LegendaryDrops.Count);
            lootPrefab = legendaryPrefab;
            lootObj = lootPrefab.GetComponent<LootObject>();
            lootObj.weaponData = LegendaryDrops[i];
            //DEV ONLY
            _DEVTOOLS_.amntLegendaryDrops++;
        }
        SpawnLootObject(lootPrefab, position);
        
    }
    //GameObject prefab, Weapon weaponDrop, Vector3 whereToDrop, Quaternion rotation
    void SpawnLootObject(GameObject lootPrefab, Vector3 whereToSpawn){
        Instantiate(lootPrefab, whereToSpawn, Quaternion.identity);
    }

}
