using System.Collections;
using System.Collections.Generic;
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
        PlayerStats.onPlayerDeath += Reset;
    }
    void OnDisable(){
        PlayerStats.handleLevelIncrease -= ScaleEnemies;
        PlayerStats.onPlayerDeath -= Reset;
    }


    void ScaleEnemies(){
        EnemyHealthModifier = enemyHpScaler.Evaluate(PlayerStats.playerStats.CurrentLevel);
        EnemyDamageModifier = enemyDmgScaler.Evaluate(PlayerStats.playerStats.CurrentLevel);
    }


    void Reset(){
        EnemyHealthModifier = 1f;
        EnemyDamageModifier = 1f;
    }
}
