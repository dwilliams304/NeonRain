using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private List<ScriptableObject> _lootDrops;

    private FloatingHealthBar _healthBar;
    [SerializeField] private PlayerStats _playerStats;


    void Start(){
        _enemyName = _enemyData.enemyName;
        _maxHealth = _enemyData.maxHealth;
        _currentHealth = _maxHealth;
        _moveSpeed = _enemyData.moveSpeed;
        _minDamage = _enemyData.minDamage;
        _maxDamage = _enemyData.maxDamage;
        _corruptionDrop = _enemyData.corruptionDrop;
        _goldDrop = _enemyData.goldDrop;
        _lootDrops.AddRange(_enemyData.lootDrops);
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        _playerStats = FindObjectOfType<PlayerStats>();
    }


    public void ReceiveDamage(float damage){
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        if(_currentHealth <= 0){
            Destroy(gameObject);
        }
    }

}
