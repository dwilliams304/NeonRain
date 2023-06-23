using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    
    [SerializeField] private string _enemyName;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _corruptionDrop;
    [SerializeField] private int _goldDrop;
    [SerializeField] private int _dropChance;
    [SerializeField] private float baseLuck;

    private FloatingHealthBar _healthBar;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject floatingDmgTextPref;
    [SerializeField] private float dmgNumberYOffset;


    public Transform target;

    void Start(){
        _enemyName = enemyData.enemyName;
        _maxHealth = enemyData.maxHealth;
        _currentHealth = _maxHealth;
        _minDamage = enemyData.minDamage;
        _maxDamage = enemyData.maxDamage;
        _corruptionDrop = enemyData.corruptionDrop;
        _goldDrop = enemyData.goldDrop;
        _dropChance = enemyData.dropChance;
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        _playerStats = PlayerStats.playerStats;
    }



    public void ReceiveDamage(float damage){
        ShowDamage(damage.ToString());
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        if(_currentHealth <= 0){
            int rollDice = Random.Range(0, 101);
            if(rollDice <= _dropChance){
                LootManager.lootManager.DropLoot(transform.position, transform.rotation, baseLuck);
            }
            _playerStats.AddGold(_goldDrop);
            _playerStats.AddCorruption(_corruptionDrop);
            Destroy(gameObject);
        }
    }


    public float DoDamage(){
        float dmgRoll = Mathf.Ceil(Random.Range(_minDamage, _maxDamage));
        return dmgRoll;
    }

    void ShowDamage(string text){
        if(floatingDmgTextPref){
            Vector3 offset = new Vector3(Random.Range(-1f, 2f), dmgNumberYOffset, 0);
            GameObject prefab = Instantiate(floatingDmgTextPref, transform.position + offset, Quaternion.identity);
            TMP_Text textComponent = prefab.GetComponentInChildren<TMP_Text>();
            textComponent.text = text;
            if(Combat.combat.didCrit){
                textComponent.color = Color.red;
                textComponent.fontSize = 12;
            }
            else{
                textComponent.color = Color.yellow;
                textComponent.fontSize = 8;
            }
        }
    }

}
