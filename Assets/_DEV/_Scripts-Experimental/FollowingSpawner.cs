using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSpawner : MonoBehaviour
{
    public static FollowingSpawner Instance;

    private Transform _playerTarget;
    [SerializeField] private float _spawnInterval;

    [System.Serializable]
    public class Spawner{
        public string _testname;
        public Vector3 Offset;
        public Transform SpawnLoc;
        public List<GameObject> Spawnables;
        public float xRandRange, yRandRange;
    }

    public List<Spawner> spawners;

    [SerializeField] private List<GameObject> _spawnableEnemies;
    [SerializeField] bool _spawnersStartTheSame = false;

    void Awake() => Instance = this;

    void Start(){   
        _playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        if(_spawnersStartTheSame){
            foreach(Spawner spawner in spawners){
                spawner.Spawnables = _spawnableEnemies;
            }
        }
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Spawner spawner in spawners){
            spawner.SpawnLoc.position = _playerTarget.position + spawner.Offset;
        }
    }

    public void AddSpawnableEnemy(GameObject spawnable){
        foreach(Spawner spawner in spawners){
            spawner.Spawnables.Add(spawnable);
        }
    }



    private IEnumerator SpawnEnemies(){
        WaitForSeconds wait = new WaitForSeconds(_spawnInterval);
        yield return new WaitForSeconds(1f);
        while(true){
            yield return wait;
            foreach(Spawner spawner in spawners){
                if(GameStats.currentAmountOfEnemies < GameStats.totalAllowedEnemies){
                    float newXPos = spawner.SpawnLoc.position.x + Random.Range(-spawner.xRandRange, spawner.xRandRange + 1);
                    float newYPos = spawner.SpawnLoc.position.y + Random.Range(-spawner.yRandRange, spawner.yRandRange + 1);
                    Vector3 newLoc = new Vector3(newXPos, newYPos, 0f);
                    Collider2D coll = Physics2D.OverlapPoint((Vector2)newLoc); 
                    if (coll == null){
                        GameStats.currentAmountOfEnemies++;
                        Instantiate(spawner.Spawnables.RandomFromList(), newLoc, Quaternion.identity);
                    }
                }
            }
        }
    }
}
