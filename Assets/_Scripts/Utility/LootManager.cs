using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    [Header("Loot Pool")]
    [SerializeField] List<Gun> allGuns;
    List<Gun> _commonDrops = new List<Gun>();
    List<Gun> _uncommonDrops = new List<Gun>();
    List<Gun> _rareDrops = new List<Gun>();
    List<Gun> _legendaryDrops = new List<Gun>();
    List<Gun> _corruptedDrops = new List<Gun>();
    Gun _uniqueWeapon;


    [Header("Loot Drop Prefabs")]
    [SerializeField] GameObject _commonPrefab;
    [SerializeField] GameObject _uncommonPrefab;
    [SerializeField] GameObject _rarePrefab;
    [SerializeField] GameObject _corruptedPrefab;
    [SerializeField] GameObject _legendaryPrefab;


    int _commonDropChance = 100000;
    int _uncommonDropChance = 40000;
    int _rareDropChance = 12500;
    int _legendaryDropChance = 250;
    int _corruptedDropChance = 2500;
    int _uniqueDropChance = 1;
    
    public float AddedLuck = 0;



    void Awake() => Instance = this;

    void Start(){
        SortIntoGroups();
    }

    void SortIntoGroups(){
        foreach(Gun gun in allGuns){
            switch(gun.rarity){
                case Rarity.Common:
                _commonDrops.Add(gun);
                    break;
                case Rarity.Uncommon:
                _uncommonDrops.Add(gun);
                    break;
                case Rarity.Rare:
                _rareDrops.Add(gun);
                    break;
                case Rarity.Corrupted:
                _corruptedDrops.Add(gun);
                    break;
                case Rarity.Legendary:
                _legendaryDrops.Add(gun);
                    break;
                case Rarity.Unique:
                _uniqueWeapon = gun;
                    break;
            }
        }
    }

    public void DropLoot(Vector3 position, float additionalLuck){
        
        float rarity = Mathf.RoundToInt(Random.Range(0, 100001) / additionalLuck);

        if(rarity <= _commonDropChance && rarity > _uncommonDropChance) {
            SpawnLootObject(GenerateLootObject(_commonPrefab, _commonDrops.RandomFromList()), position);
        }

        else if(rarity <= _uncommonDropChance && rarity > _rareDropChance) {
            SpawnLootObject(GenerateLootObject(_uncommonPrefab, _uncommonDrops.RandomFromList()), position);
        }

        else if(rarity <= _rareDropChance && rarity > _corruptedDropChance) {
            SpawnLootObject(GenerateLootObject(_rarePrefab, _rareDrops.RandomFromList()), position);
        }

        else if(rarity <= _corruptedDropChance && rarity > _legendaryDropChance) {
            SpawnLootObject(GenerateLootObject(_corruptedPrefab, _corruptedDrops.RandomFromList()), position);
        }
        
        else if(rarity <= _legendaryDropChance && rarity != _uniqueDropChance) {
            SpawnLootObject(GenerateLootObject(_legendaryPrefab, _legendaryDrops.RandomFromList()), position);
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
