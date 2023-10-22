using UnityEngine;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    public EnemyData enemyData;
    
    // [SerializeField] private string _enemyName;
    [SerializeField] float _minDamage;
    [SerializeField] float _maxDamage;
    [SerializeField] int _corruptionDrop;
    [SerializeField] int _goldDrop;
    [SerializeField] int _dropChance;
    [SerializeField] int _scoreAmnt;
    [SerializeField] float _baseLuck;
    [SerializeField] float _xpAmount;

    [SerializeField] GameObject floatingDmgTextPref;
    [SerializeField] float dmgNumberYOffset;

    [SerializeField] Color nonCritColor;
    [SerializeField] Color critColor;

    [SerializeField] Transform _player;

    HealthSystem _health;
    LevelScaler _lvlScaler;


    void OnEnable(){
        _health = GetComponent<HealthSystem>();
        _health.onDamage += ShowDamage;
        _health.onDeath += Die;

        
    }

    void OnDisable(){
        _health.onDamage -= ShowDamage;
        _health.onDeath -= Die;
    }


    void Start(){
        _lvlScaler = LevelScaler.Instance;
        _health.SetMaxHealth(enemyData.maxHealth * _lvlScaler.EnemyHealthModifier);

        _minDamage = Mathf.RoundToInt(enemyData.minDamage * _lvlScaler.EnemyDamageModifier);
        _maxDamage = Mathf.RoundToInt(enemyData.maxDamage * _lvlScaler.EnemyDamageModifier);

        _xpAmount = enemyData.xpAmount;
        _corruptionDrop = enemyData.corruptionDrop;
        _goldDrop = enemyData.goldDrop;
        _dropChance = enemyData.dropChance;
        _scoreAmnt = enemyData.score;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }




    void Die(){
        if(Extensions.Roll100(_dropChance)){
            LootManager.Instance.DropLoot(transform.position, _baseLuck);
        }
        Inventory.Instance.AddGold(_goldDrop);
        XPManager.Instance.AddExperience(Mathf.CeilToInt(_xpAmount));
        CorruptionManager.Instance.AddCorruption(_corruptionDrop);
        ScoreManager.scoreManager.AddToScore(_scoreAmnt);
        Destroy(gameObject);
    }


    float DoDamage(){
        return Mathf.RoundToInt(Random.Range(_minDamage, _maxDamage));
    }

    void ShowDamage(float dmgAmnt){
        if(floatingDmgTextPref){
            Vector3 offset = new Vector3(Random.Range(-1f, 2f), dmgNumberYOffset, 0);
            GameObject prefab = Instantiate(floatingDmgTextPref, transform.position + offset, Quaternion.identity);
            TMP_Text textComponent = prefab.GetComponentInChildren<TMP_Text>();
            textComponent.text = dmgAmnt.ToString();
            if(Combat.Instance.didCrit){
                textComponent.color = critColor;
                textComponent.fontSize = 12;
            }
            else{
                textComponent.color = nonCritColor;
                textComponent.fontSize = 8;
            }
        }
    }

}
