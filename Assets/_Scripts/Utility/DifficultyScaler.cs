using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    
    public delegate void DifficultyIncreased();
    public static DifficultyIncreased diffIncreased;

#region Spawner Objects
[Header("Spawner Objects")]
    [SerializeField] private GameObject _spawnableTier1;
    [SerializeField] private GameObject _spawnableTier2;
    [SerializeField] private GameObject _spawnableTier3;
    [SerializeField] private GameObject _spawnableTier4;
    [SerializeField] private GameObject _spawnableTier5;
    [SerializeField] private GameObject _spawnableTier6;
    [SerializeField] private GameObject _spawnableTier7;
    [SerializeField] private GameObject _spawnableTier8;
    [SerializeField] private GameObject _spawnableTier9;
    [SerializeField] private GameObject _spawnableTier10;
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


    private FollowingSpawner _spawner;
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
        _spawner = FollowingSpawner.Instance;
    }

    void CheckDifficultyScale(int currentLevel){
        if(currentLevel == _tier10Break){
            _spawner.AddSpawnableEnemy(_spawnableTier10);
        }
        else if(currentLevel == _tier9Break){
            _spawner.AddSpawnableEnemy(_spawnableTier9);
        }
        else if(currentLevel == _tier8Break){
            _spawner.AddSpawnableEnemy(_spawnableTier8);
        }
        else if(currentLevel == _tier7Break){
            _spawner.AddSpawnableEnemy(_spawnableTier7);
        }
        else if(currentLevel == _tier6Break){
            _spawner.AddSpawnableEnemy(_spawnableTier6);
        }
        else if(currentLevel == _tier5Break){
            _spawner.AddSpawnableEnemy(_spawnableTier5);
        }
        else if(currentLevel == _tier4Break){
            _spawner.AddSpawnableEnemy(_spawnableTier4);
        }
        else if(currentLevel == _tier3Break){
            _spawner.AddSpawnableEnemy(_spawnableTier3);
        }
        else if(currentLevel == _tier2Break){
            _spawner.AddSpawnableEnemy(_spawnableTier2);
        }
        else if(currentLevel == _tier1Break){
            _spawner.AddSpawnableEnemy(_spawnableTier1);
        }
        
    }

}
