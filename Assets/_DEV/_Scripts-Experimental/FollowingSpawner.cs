using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSpawner : MonoBehaviour
{
    public static FollowingSpawner Instance;

    private Transform _playerTarget;
    [SerializeField] private float _startingDelay = 5f;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private int _toSpawnEachInterval;


    
    public bool IsSpawning = true;
    private int _amountSpawned;
    private float _newXPos;
    private float _newYPos;
    private Vector3 _newPos = new Vector3(0f, 0f, 0f);

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

    void Update()
    {
        transform.position = _playerTarget.position;
    }

    public void AddSpawnableEnemy(GameObject spawnable){
        foreach(Spawner spawner in spawners){
            spawner.Spawnables.Add(spawnable);
        }
    }



    private IEnumerator SpawnEnemies(){
        //Set all the variables we are going to use
        WaitForSeconds wait = new WaitForSeconds(_spawnInterval);
        yield return new WaitForSeconds(_startingDelay);
        while(IsSpawning){ //While we are set to spawn...
            yield return wait; //Wait the set amount of time from the interval
            _amountSpawned = 0; //Set i to 0
            while(_amountSpawned < _toSpawnEachInterval){ //While the current amount we have spawned this cycle is less than our want -> do this
                foreach(Spawner spawner in spawners){ //Loop through all the spawners
                    if(GameStats.currentAmountOfEnemies < GameStats.totalAllowedEnemies){ //If we don't have too many enemies
                        _newXPos = spawner.SpawnLoc.position.x + Random.Range(-spawner.xRandRange, spawner.xRandRange + 1); //Generate a random pos
                        _newYPos = spawner.SpawnLoc.position.y + Random.Range(-spawner.yRandRange, spawner.yRandRange + 1);
                        _newPos.x = _newXPos;
                        _newPos.y = _newYPos;
                        Collider2D coll = Physics2D.OverlapPoint((Vector2)_newPos); 
                        if (coll == null && _amountSpawned < _toSpawnEachInterval){
                            _amountSpawned++;
                            GameStats.currentAmountOfEnemies++;
                            Instantiate(spawner.Spawnables.RandomFromList(), _newPos, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}
