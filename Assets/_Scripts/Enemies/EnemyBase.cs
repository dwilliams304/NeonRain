using UnityEngine;
using TMPro;
using Pathfinding;

public class EnemyBase : MonoBehaviour
{
    public EnemyData enemyData;
    
    // [SerializeField] private string _enemyName;
    [SerializeField] float _minDamage;
    [SerializeField] float _maxDamage;
    [SerializeField] float _baseLuck;
    [SerializeField] float _attackRange;
    int _goldDrop;
    int _dropChance;
    int _scoreAmnt;

    float lastAtk;
    float atkSpeed;
    [SerializeField] GameObject floatingDmgTextPref;
    [SerializeField] float dmgNumberYOffset;

    [SerializeField] Color nonCritColor;
    [SerializeField] Color critColor;

    [SerializeField] Transform _player;
    HealthSystem _playerHealth;

    HealthSystem _health;
    LevelScaler _lvlScaler;

    DamageFlash _damageFlash;

    private AIPath path;
    // public AIDestinationSetter setter;



    void OnEnable(){
        _health = GetComponent<HealthSystem>();
        _health.onDamage += ShowDamage;
        _health.onDeath += Die;
    }

    void OnDisable(){
        _health.onDamage -= ShowDamage;
        _health.onDeath -= Die;
    }

    void Awake(){
        path = GetComponent<AIPath>();
    }
    void Start(){
        _lvlScaler = LevelScaler.Instance;
        _damageFlash = GetComponent<DamageFlash>();
        _health.SetMaxHealth(enemyData.maxHealth * _lvlScaler.EnemyHealthModifier);

        _minDamage = Mathf.RoundToInt(enemyData.minDamage * _lvlScaler.EnemyDamageModifier);
        _maxDamage = Mathf.RoundToInt(enemyData.maxDamage * _lvlScaler.EnemyDamageModifier);
        _attackRange = enemyData.attackRange;

        path.maxSpeed = enemyData.moveSpeed;
        
        _goldDrop = enemyData.goldDrop;
        _dropChance = enemyData.dropChance;
        _scoreAmnt = enemyData.score;
        atkSpeed = enemyData.attackSpeed;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<AIDestinationSetter>().target = _player;
        _playerHealth = _player.GetComponent<HealthSystem>();
    }

    void Update(){
        if(Vector2.Distance(transform.position, _player.position) < _attackRange){
            if(Time.time > lastAtk + atkSpeed){
                lastAtk = Time.time;
                _playerHealth.DecreaseCurrentHealth(DoDamage());
            }
        }
    }

    


    void Die(){
        if(Extensions.Roll100(_dropChance)){
            LootManager.Instance.DropLoot(transform.position, _baseLuck);
        }
        Inventory.Instance.AddGold(_goldDrop);
        LevelSystem.Instance.AddExperience(Mathf.RoundToInt(enemyData.xpAmount));
        CorruptionManager.Instance.IncreaseCorruptionAmount(enemyData.corruptionDrop);
        // CorruptionManager.Instance.AddCorruption(_corruptionDrop);
        ScoreManager.scoreManager.AddToScore(_scoreAmnt);
        GameStats.enemiesKilled++;
        Destroy(gameObject);
    }


    int DoDamage(){
        return Mathf.RoundToInt(Random.Range(_minDamage, _maxDamage));
    }

    void ShowDamage(float dmgAmnt){
        _damageFlash.CallDamageFlash();
        if(floatingDmgTextPref){
            Vector3 offset = new Vector3(Random.Range(-1f, 2f), dmgNumberYOffset, 0);
            GameObject prefab = Instantiate(floatingDmgTextPref, transform.position + offset, Quaternion.identity);
            TMP_Text textComponent = prefab.GetComponentInChildren<TMP_Text>();
            textComponent.text = dmgAmnt.ToString();
            GameStats.damageDone += dmgAmnt;
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
