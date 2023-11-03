using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    [Header("Loot Pool")]
    [SerializeField] List<Gun> _commonDrops;
    [SerializeField] List<Gun> _uncommonDrops;
    [SerializeField] List<Gun> _rareDrops;
    [SerializeField] List<Gun> _legendaryDrops;
    [SerializeField] List<Gun> _corruptedDrops;
    [SerializeField] Gun _uniqueWeapon;


    [Header("Loot Drop Prefabs")]
    [SerializeField] GameObject _commonPrefab;
    [SerializeField] GameObject _uncommonPrefab;
    [SerializeField] GameObject _rarePrefab;
    [SerializeField] GameObject _corruptedPrefab;
    [SerializeField] GameObject _legendaryPrefab;
    

    public GameObject _dev_Panel;


    int _commonDropChance = 100000;
    int _uncommonDropChance = 40000;
    int _rareDropChance = 12500;
    int _legendaryDropChance = 250;
    int _corruptedDropChance = 2500;
    int _uniqueDropChance = 1;
    
    public float AddedLuck = 0;

    DEVTools _DEVTOOLS_;


    void Awake() => Instance = this;

    void Start(){
        _DEVTOOLS_ = DEVTools.__DEV;
    }

    public void DropLoot(Vector3 position, float additionalLuck){
        _DEVTOOLS_.totalRolls++;
        
        float rarity = Mathf.RoundToInt(Random.Range(0, 100001) / additionalLuck);

        if(rarity <= _commonDropChance && rarity > _uncommonDropChance) {
            SpawnLootObject(GenerateLootObject(_commonPrefab, _commonDrops.RandomFromList()), position);
            _DEVTOOLS_.amntCommonDrops++; //DEV ONLY
        }

        else if(rarity <= _uncommonDropChance && rarity > _rareDropChance) {
            SpawnLootObject(GenerateLootObject(_uncommonPrefab, _uncommonDrops.RandomFromList()), position);
            _DEVTOOLS_.amntUncommonDrops++; //DEV ONLY
        }

        else if(rarity <= _rareDropChance && rarity > _corruptedDropChance) {
            SpawnLootObject(GenerateLootObject(_rarePrefab, _rareDrops.RandomFromList()), position);
            _DEVTOOLS_.amntRareDrops++; //DEV ONLY
        }

        else if(rarity <= _corruptedDropChance && rarity > _legendaryDropChance) {
            SpawnLootObject(GenerateLootObject(_corruptedPrefab, _corruptedDrops.RandomFromList()), position);
            _DEVTOOLS_.amntCorruptDrops++; //DEV ONLY
        }
        
        else if(rarity <= _legendaryDropChance && rarity != _uniqueDropChance) {
            SpawnLootObject(GenerateLootObject(_legendaryPrefab, _legendaryDrops.RandomFromList()), position);
            _DEVTOOLS_.amntLegendaryDrops++; //DEV ONLY
        }
    }

    GameObject GenerateLootObject(GameObject prefab, Gun gunToSet){
        LootObject lootObj = prefab.GetComponent<LootObject>();
        lootObj.weaponData = gunToSet;
        return prefab;
    }

    void SpawnLootObject(GameObject lootPrefab, Vector3 whereToSpawn){
        Instantiate(lootPrefab, whereToSpawn, Quaternion.identity);
    }

}
