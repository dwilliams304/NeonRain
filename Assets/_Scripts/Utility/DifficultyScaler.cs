using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    
    public delegate void DifficultyIncreased();
    public static DifficultyIncreased diffIncreased;

#region Spawner Objects
[Header("Spawner Objects")]
    [SerializeField] private List<GameObject> StartSpawners;
    [SerializeField] private List<GameObject> SpawnerTier1;
    [SerializeField] private List<GameObject> SpawnerTier2;
    [SerializeField] private List<GameObject> SpawnerTier3;
    [SerializeField] private List<GameObject> SpawnerTier4;
    [SerializeField] private List<GameObject> SpawnerTier5;
    [SerializeField] private List<GameObject> SpawnerTier6;
    [SerializeField] private List<GameObject> SpawnerTier7;
    [SerializeField] private List<GameObject> SpawnerTier8;
    [SerializeField] private List<GameObject> SpawnerTier9;
    [SerializeField] private List<GameObject> SpawnerTier10;
#endregion

#region Tier Breakpoints
[Header("Tier Breakpoints")]
    [SerializeField] private int _tier1Break;
    [SerializeField] private int _tier2Break;
    [SerializeField] private int _tier3Break;
    [SerializeField] private int _tier4Break;
    [SerializeField] private int _tier5Break;
    [SerializeField] private int _tier6Break;
    [SerializeField] private int _tier7Break;
    [SerializeField] private int _tier8Break;
    [SerializeField] private int _tier9Break;
    [SerializeField] private int _tier10Break;
#endregion



    void OnEnable(){
        LevelSystem.onLevelChange += CheckDifficultyScale;
    }
    void OnDisable(){
        LevelSystem.onLevelChange -= CheckDifficultyScale;
    }


    void ActivateSpawners(List<GameObject> spawnerList){
        if(spawnerList.Count != 0){
            foreach(GameObject obj in spawnerList){
                obj.SetActive(true);
            }
        }
        diffIncreased?.Invoke();
    }


    void Start(){
        // ActivateSpawners(StartSpawners);
        foreach(GameObject obj in StartSpawners){
            obj.SetActive(true);
        }
    }

    void CheckDifficultyScale(int currentLevel){
        if(currentLevel == _tier10Break){
            ActivateSpawners(SpawnerTier10);
        }
        else if(currentLevel == _tier9Break){
            ActivateSpawners(SpawnerTier9);
        }
        else if(currentLevel == _tier8Break){
            ActivateSpawners(SpawnerTier8);
        }
        else if(currentLevel == _tier7Break){
            ActivateSpawners(SpawnerTier7);
        }
        else if(currentLevel == _tier6Break){
            ActivateSpawners(SpawnerTier6);
        }
        else if(currentLevel == _tier5Break){
            ActivateSpawners(SpawnerTier5);
        }
        else if(currentLevel == _tier4Break){
            ActivateSpawners(SpawnerTier4);
        }
        else if(currentLevel == _tier3Break){
            ActivateSpawners(SpawnerTier3);
        }
        else if(currentLevel == _tier2Break){
            ActivateSpawners(SpawnerTier2);
        }
        else if(currentLevel == _tier1Break){
            ActivateSpawners(SpawnerTier1);
        }
        
    }

}
