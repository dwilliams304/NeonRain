using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;


    [SerializeField] private float spawnInterval;
    [SerializeField] private int amountToSpawn = 10;
    int alreadySpawned = 0;
    Vector3 currentPos;

    void Start(){
        currentPos = transform.position;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies(){
        int i = Random.Range(0, enemyPrefabs.Count);
        GameObject toSpawn = enemyPrefabs[i];
        yield return new WaitForSeconds(spawnInterval);
        Instantiate(toSpawn, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
        alreadySpawned++;
        if(alreadySpawned != amountToSpawn){
            StartCoroutine(SpawnEnemies());
        }
        else{
            StopCoroutine(SpawnEnemies());
        }
        
    }
}
