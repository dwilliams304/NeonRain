using UnityEngine;

public class LevelScaler : MonoBehaviour
{
    public static LevelScaler Instance;
    public float EnemyHealthModifier = 1f;
    public float EnemyDamageModifier = 1f;
    public float EnemyXPDropModifier = 1f;
    [SerializeField] private AnimationCurve enemyHpScaler;
    [SerializeField] private AnimationCurve enemyDmgScaler;
    [SerializeField] private AnimationCurve xpDropScaler;



    void Awake(){
        Instance = this;
    }

    void OnEnable(){
        LevelSystem.onLevelChange += ScaleEnemies;
    }
    void OnDisable(){
        LevelSystem.onLevelChange -= ScaleEnemies;
    }


    void ScaleEnemies(int level){
        EnemyHealthModifier = enemyHpScaler.Evaluate(level);
        EnemyDamageModifier = enemyDmgScaler.Evaluate(level);
        EnemyXPDropModifier = xpDropScaler.Evaluate(level);
    }


    void Reset(){
        EnemyHealthModifier = 1f;
        EnemyDamageModifier = 1f;
        EnemyXPDropModifier = 1f;
    }
}
