using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DEVTools : MonoBehaviour
{
    public static DEVTools __DEV;
    private LootManager _lootManager;
    private PlayerStats _playerStats;
    private Combat _combat;


    [SerializeField] private GameObject _devToolsUIPanel;
    public bool devToolsEnabled = false;

#region DEV Loot Panel vars
    [Header("Loot Drop Panel UI Elems")]
    [SerializeField] private TMP_Text commonTxt;
    [SerializeField] private TMP_Text uncommonTxt;
    [SerializeField] private TMP_Text rareTxt;
    [SerializeField] private TMP_Text corruptedTxt;
    [SerializeField] private TMP_Text legendaryTxt;
    [SerializeField] private TMP_Text uniqueTxt;
    [SerializeField] private TMP_Text totalDrops;

    public float amntCommonDrops = 0;
    public float amntUncommonDrops = 0;
    public float amntRareDrops = 0;
    public float amntCorruptDrops = 0;
    public float amntLegendaryDrops = 0;

    public float totalRolls;
#endregion


#region AI Tools
    public List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> spawners;
    // private List<GameObject> enemies;
    bool spawnersActive = true;
    [SerializeField] TMP_Dropdown EnemySpawnDropDown;

#endregion


    private Camera mainCam;
    private Vector2 mousePos;
    void Awake(){
        __DEV = this;
    }
    void Start(){
        _lootManager = LootManager.lootManager;
        _playerStats = PlayerStats.playerStats;
        mainCam = Camera.main;
        spawners.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));
    }



    void Update(){
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);
        if(devToolsEnabled){
            if(Input.GetKey(KeyCode.F)){
                UpdateLootUIPanel();
            }

            if(Input.GetKeyDown(KeyCode.Alpha1)){
                SpawnEnemy(mousePos);
            }
            if(Input.GetKeyDown(KeyCode.R)){
                _lootManager.DropLoot(mousePos, 1);
                UpdateLootUIPanel();
            }
        }
        if(Input.GetKeyDown(KeyCode.BackQuote)){
            devToolsEnabled = !devToolsEnabled;
            OpenDevPanel();
        }

    }


    void OpenDevPanel(){
        _devToolsUIPanel.SetActive(devToolsEnabled);
    }

    void UpdateLootUIPanel(){
        commonTxt.text = $"Common: {amntCommonDrops} --> Avg: {amntCommonDrops / totalRolls * 100}%";
        uncommonTxt.text = $"Uncommon: {amntUncommonDrops} --> Avg: {amntUncommonDrops / totalRolls * 100}%";
        rareTxt.text = $"Rare: {amntRareDrops} --> Avg: {amntRareDrops / totalRolls * 100}%";
        corruptedTxt.text = $"Corrupted: {amntCorruptDrops} --> Avg: {amntCorruptDrops / totalRolls * 100}%";
        legendaryTxt.text = $"Legendary: {amntLegendaryDrops} --> Avg: {amntLegendaryDrops / totalRolls * 100}%";
        totalDrops.text = $"Total drops: {totalRolls}";
    }
    
    
    // public void SwitchEnemyToSpawn(GameObject selectedEnemy){
    //     enemyPrefab = selectedEnemy;
    // }
    void SpawnEnemy(Vector3 position){
        GameObject enemy = enemyPrefabs[EnemySpawnDropDown.value];
        Instantiate(enemy, position, Quaternion.identity);
    }


    public void ToggleSpawners(){
        spawnersActive = !spawnersActive;
        foreach(GameObject spawner in spawners){
            spawner.SetActive(spawnersActive);
        }
    }
    public void RemoveAllEnemies(){
        //enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach(GameObject enemy in enemies){
            Destroy(enemy);
        }
    }
}
