using UnityEngine;

public class LevelScaler : MonoBehaviour
{
    public static LevelScaler Instance;
    public float EnemyHealthModifier = 1f;
    public float EnemyDamageModifier = 1f;
    [SerializeField] private AnimationCurve enemyHpScaler;
    [SerializeField] private AnimationCurve enemyDmgScaler;



    void Awake(){
        Instance = this;
    }

    void OnEnable(){
        PlayerStats.handleLevelIncrease += ScaleEnemies;
    }
    void OnDisable(){
        PlayerStats.handleLevelIncrease -= ScaleEnemies;
    }


    void ScaleEnemies(){
        EnemyHealthModifier = enemyHpScaler.Evaluate(PlayerStats.Instance.CurrentLevel);
        EnemyDamageModifier = enemyDmgScaler.Evaluate(PlayerStats.Instance.CurrentLevel);
    }


    void Reset(){
        EnemyHealthModifier = 1f;
        EnemyDamageModifier = 1f;
    }
}