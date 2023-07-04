using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    [SerializeField] private float spawnInterval;
    Vector3 currentPos;

    void Start(){
        currentPos = transform.position;
        //StartCoroutine(SpawnEnemies());
    }
    void OnEnable(){
        StartCoroutine(SpawnEnemies());
    }
    void OnDisable(){
        StopCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies(){
        int i = Random.Range(0, enemyPrefabs.Count);
        GameObject toSpawn = enemyPrefabs[i];
        yield return new WaitForSeconds(spawnInterval);
        Instantiate(toSpawn, new Vector3(currentPos.x + Random.Range(-5f, 5f), currentPos.y + Random.Range(-5f, 5f), 0), Quaternion.identity);
        StartCoroutine(SpawnEnemies());
    }
}
