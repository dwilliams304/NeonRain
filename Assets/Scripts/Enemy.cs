using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    
    [SerializeField] private string _enemyName;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _corruptionDrop;
    [SerializeField] private int _goldDrop;
    [SerializeField] private List<ScriptableObject> _lootDrops;


    void Start(){
        _enemyName = _enemyData.enemyName;
        _maxHealth = _enemyData.maxHealth;
        _currentHealth = _maxHealth;
        _moveSpeed = _enemyData.moveSpeed;
        _minDamage = _enemyData.minDamage;
        _maxDamage = _enemyData.maxDamage;
        _corruptionDrop = _enemyData.corruptionDrop;
        _goldDrop = _enemyData.goldDrop;
        _lootDrops.AddRange(_enemyData.meleeDrops);
        _lootDrops.AddRange(_enemyData.rangedDrops);
    }


    public void ReceiveDamage(int damage){
        _currentHealth -= damage;
    }

}
