using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    
    [SerializeField] private string _enemyName;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _corruptionDrop;
    [SerializeField] private int _goldDrop;
    [SerializeField] private int _dropChance;
    [SerializeField] private float baseLuck;
    [SerializeField] private List<ScriptableObject> _lootDrops;

    private FloatingHealthBar _healthBar;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject floatingDmgTextPref;


    void Start(){
        _enemyName = _enemyData.enemyName;
        _maxHealth = _enemyData.maxHealth;
        _currentHealth = _maxHealth;
        _moveSpeed = _enemyData.moveSpeed;
        _minDamage = _enemyData.minDamage;
        _maxDamage = _enemyData.maxDamage;
        _corruptionDrop = _enemyData.corruptionDrop;
        _goldDrop = _enemyData.goldDrop;
        _dropChance = _enemyData.dropChance;
        _lootDrops.AddRange(_enemyData.lootDrops);
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        _playerStats = FindObjectOfType<PlayerStats>();
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
            Destroy(gameObject);
        }
    }


    void ShowDamage(string text){
        if(floatingDmgTextPref){
            Vector3 offset = new Vector3(Random.Range(-1, 2), 1, 0);
            GameObject prefab = Instantiate(floatingDmgTextPref, transform.position + offset, Quaternion.identity);
            prefab.GetComponentInChildren<TMP_Text>().text = text;
        }
    }

}
