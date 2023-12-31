using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableSpawner : MonoBehaviour, IInteractable
{
    public List<GameObject> enemyPrefabs;
    public List<GameObject> miniBossPrefabs;


    [SerializeField] private float spawnInterval;
    [SerializeField] private int amountToSpawn = 10;
    [SerializeField] private TMP_Text spawnerText;
    int alreadySpawned = 0;
    Vector3 currentPos;

    // bool completed = false;
    bool activated = false;

    void Start(){
        currentPos = transform.position;
        spawnerText.text = "Press [E] to start!";
        //StartCoroutine(SpawnEnemies());
    }

    public void Interacted(){
        if(!activated){
            StartCoroutine(SpawnEnemies());
            activated = true;
            spawnerText.text = $"Spawning {amountToSpawn} enemies...";
        }
    }

    private IEnumerator SpawnEnemies(){
        int i = Random.Range(0, enemyPrefabs.Count);
        GameObject toSpawn = enemyPrefabs[i];
        yield return new WaitForSeconds(spawnInterval);
        Instantiate(toSpawn, new Vector3(currentPos.x + Random.Range(-5f, 5f), currentPos.y + Random.Range(-5f, 5f), 0), Quaternion.identity);
        alreadySpawned++;
        if(alreadySpawned != amountToSpawn){
            StartCoroutine(SpawnEnemies());
        }
        else if(alreadySpawned == amountToSpawn){
            GameObject miniBoss = miniBossPrefabs[Random.Range(0, miniBossPrefabs.Count)];
            Instantiate(miniBoss, new Vector3(currentPos.x + Random.Range(-5f, 5f), currentPos.y + Random.Range(-5f, 5f), 0), Quaternion.identity);
            spawnerText.text = "Mini boss incoming!";
            StopCoroutine(SpawnEnemies());
        }
        
    }
}
