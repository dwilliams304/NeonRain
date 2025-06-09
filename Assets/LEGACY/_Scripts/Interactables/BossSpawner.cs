using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] private float _interval;
    [SerializeField] private int _enemiesBeforeBoss;
    [SerializeField] private List<GameObject> _spawnableEnemies;
    [SerializeField] private GameObject _bossToSpawn;

    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private int _goldCost;
    [SerializeField] private int _levelRequirement;

    bool started;
    WaitForSeconds wait;

    void Start(){
        wait = new WaitForSeconds(_interval);
        started = false;
        _canvas.SetActive(false);
        UpdateText();
    }

    bool CheckRequirements(){
        if(Inventory.Instance.PlayerGold < _goldCost || LevelSystem.Instance.CurrentLevel < _levelRequirement){
            return false;
        }
        return true;
    }

    void UpdateText(){
        if(CheckRequirements() == false){
            _infoText.text = $"Requirements not met! \n Cost: {_goldCost} \n Level: {_levelRequirement}";
        }else{
            _infoText.text = "Press [E] to start the boss fight!";
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player") {
            UpdateText();
            _canvas.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player") {
            _canvas.SetActive(false);
        }
    }

    public void Interacted()
    {
        if(!started && CheckRequirements()) {
            StartCoroutine(StartBossSpawner());
            Inventory.Instance.RemoveGold(_goldCost);
        }
    }

    private IEnumerator StartBossSpawner(){
        started = true;
        int spawned = 0;
        while(spawned < _enemiesBeforeBoss){
            yield return wait;
            Instantiate(_spawnableEnemies.RandomFromList(), transform.position, Quaternion.identity);
            spawned++;
        }
        yield return wait;
        Instantiate(_bossToSpawn, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        
    }
}
