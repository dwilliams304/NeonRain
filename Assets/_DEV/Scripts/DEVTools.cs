using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEVTools : MonoBehaviour
{
    public static DEVTools __DEV;
    private LootManager _lootManager;
    private PlayerStats _playerStats;
    private Combat _combat;


    [SerializeField] private GameObject _devToolsUIPanel;
    public bool devToolsEnabled = false;
    [SerializeField] TMP_Text lastAction;

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
    [SerializeField] TMP_Dropdown EnemySpawnDropDown;

    [SerializeField] private List<GameObject> spawners;
    bool spawnersActive = true;

#endregion


#region Player Stats
    [SerializeField] TMP_Text playerDamageDone;
    [SerializeField] TMP_Text playerDamageTaken;
    [SerializeField] TMP_Text playerXPIncrease;
    [SerializeField] TMP_Text playerLuckModifier;
    [SerializeField] TMP_Text playerGoldModifier;
    [SerializeField] TMP_Text playerCritChanceModifier;
    [SerializeField] TMP_Text playerCritMultilpierModifier;
    [SerializeField] TMP_Text playerMoveSpeed;
    [SerializeField] TMP_Text playerCurrentHealth;
    [SerializeField] TMP_Text playerCurrentXP;





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
                UpdateUIPanel();
            }

            if(Input.GetKeyDown(KeyCode.V)){
                SpawnEnemy(mousePos);
                UpdateLastActionText("Spawned enemy.");
            }
            if(Input.GetKeyDown(KeyCode.G)){
                _lootManager.DropLoot(mousePos, 1);
                UpdateLastActionText("Spawned loot.");
                UpdateUIPanel();
            }
        }
        if(Input.GetButtonDown("DebugMenu")){
            devToolsEnabled = !devToolsEnabled;
            OpenDevPanel();
        }

    }


    void OpenDevPanel(){
        _devToolsUIPanel.SetActive(devToolsEnabled);
    }

    public void UpdateUIPanel(){
        //Loot UI Update
        commonTxt.text = $"Common: {amntCommonDrops} --> Avg: {amntCommonDrops / totalRolls * 100}%";
        uncommonTxt.text = $"Uncommon: {amntUncommonDrops} --> Avg: {amntUncommonDrops / totalRolls * 100}%";
        rareTxt.text = $"Rare: {amntRareDrops} --> Avg: {amntRareDrops / totalRolls * 100}%";
        corruptedTxt.text = $"Corrupted: {amntCorruptDrops} --> Avg: {amntCorruptDrops / totalRolls * 100}%";
        legendaryTxt.text = $"Legendary: {amntLegendaryDrops} --> Avg: {amntLegendaryDrops / totalRolls * 100}%";
        totalDrops.text = $"Total drops: {totalRolls}";

        //Player stats Update
        playerDamageDone.text = $"Damage done: {PlayerStats.playerStats.DamageDoneMod}x";
        playerDamageTaken.text = $"Damage taken: {PlayerStats.playerStats.DamageTakenMod}x";
        playerXPIncrease.text = $"XP Increase: {XPManager.Instance.XPModifier}x";
        playerLuckModifier.text = $"N/A";
        playerGoldModifier.text = $"Gold modifier: {PlayerStats.playerStats.AdditionalGoldMod}x";
        playerCritChanceModifier.text = $"Crit chance: {PlayerStats.playerStats.CritChanceMod}%";
        playerCritMultilpierModifier.text = $"Crit multiplier: {PlayerStats.playerStats.CritDamageMod}x";
        // playerMoveSpeed.text = $"Move speed: {PlayerController.MoveSpeed}";
        playerCurrentHealth.text = $"Current health: {PlayerStats.playerStats.CurrentHealth}";
        playerCurrentXP.text = $"Current XP: {PlayerStats.playerStats.CurrentPlayerXP}";
    }
    
    
    void SpawnEnemy(Vector3 position){ //Enemy in dropdown menu MUST match index in the list! -> Can be not so great in the future (but it's dev only who cares??!!)
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
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach(GameObject enemy in enemies){
            Destroy(enemy);
        }
    }


    public void UpdateLastActionText(string text){
        lastAction.text = text;
    }
    public void SpawnerToggledText(){
        lastAction.text = $"Spawners toggled. Spawner active status: {spawnersActive}";
    }
}
