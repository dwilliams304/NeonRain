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
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private int _corruptionDrop;
    [SerializeField] private int _goldDrop;
    [SerializeField] private int _dropChance;
    [SerializeField] private float baseLuck;
    [SerializeField] private float _xpAmount;

    private FloatingHealthBar _healthBar;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject floatingDmgTextPref;
    [SerializeField] private float dmgNumberYOffset;
    public Transform target;


    void Start(){
        _enemyName = enemyData.enemyName;
        _maxHealth = Mathf.Ceil(enemyData.maxHealth * LevelScaler.Instance.EnemyHealthModifier);
        _currentHealth = _maxHealth;
        _minDamage = Mathf.Ceil(enemyData.minDamage * LevelScaler.Instance.EnemyDamageModifier);
        _maxDamage = Mathf.Ceil(enemyData.maxDamage * LevelScaler.Instance.EnemyDamageModifier);
        _xpAmount = enemyData.xpAmount;
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
            EnemyDeath();
        }
    }

    void EnemyDeath(){
        int rollDice = Random.Range(0, 101);
            if(rollDice <= _dropChance){
                LootManager.lootManager.DropLoot(transform.position, baseLuck);
            }
            _playerStats.AddGold(_goldDrop);
            XPManager.Instance.AddExperience(Mathf.CeilToInt(_xpAmount));
            CorruptionManager.Instance.AddCorruption(_corruptionDrop);
            Destroy(gameObject);
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
