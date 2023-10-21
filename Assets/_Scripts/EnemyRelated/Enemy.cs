using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    
    // [SerializeField] private string _enemyName;
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private int _corruptionDrop;
    [SerializeField] private int _goldDrop;
    [SerializeField] private int _dropChance;
    [SerializeField] private int _scoreAmnt;
    [SerializeField] private float baseLuck;
    [SerializeField] private float _xpAmount;

    [SerializeField] private GameObject floatingDmgTextPref;
    [SerializeField] private float dmgNumberYOffset;

    [SerializeField] private Color nonCritColor;
    [SerializeField] private Color critColor;

    private HealthBehavior _health;
    private LevelScaler _lvlScaler;


    void OnEnable(){
        _health = GetComponent<HealthBehavior>();
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

        _minDamage = Mathf.Ceil(enemyData.minDamage * _lvlScaler.EnemyDamageModifier);
        _maxDamage = Mathf.Ceil(enemyData.maxDamage * _lvlScaler.EnemyDamageModifier);

        _xpAmount = enemyData.xpAmount;
        _corruptionDrop = enemyData.corruptionDrop;
        _goldDrop = enemyData.goldDrop;
        _dropChance = enemyData.dropChance;
        _scoreAmnt = enemyData.score;
    }




    void Die(){
        int rollDice = Random.Range(0, 101);
        if(rollDice <= _dropChance){
            LootManager.lootManager.DropLoot(transform.position, baseLuck);
        }
        Inventory.Instance.AddGold(_goldDrop);
        XPManager.Instance.AddExperience(Mathf.CeilToInt(_xpAmount));
        CorruptionManager.Instance.AddCorruption(_corruptionDrop);
        ScoreManager.scoreManager.AddToScore(_scoreAmnt);
        Destroy(gameObject);
    }


    public float DoDamage(){
        float dmgRoll = Mathf.Ceil(Random.Range(_minDamage, _maxDamage));
        return dmgRoll;
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
