using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{

    public string enemyName = "Default Enemy";
    public float maxHealth = 100f;
    public float moveSpeed = 7.5f;
    public float minDamage = 3f;
    public float maxDamage = 6f;
    public float attackSpeed = 0.5f;
    public float attackRange = 1f;


    public int xpAmount = 10;
    public int dropChance = 5;
    public int corruptionDrop = 10;
    public int goldDrop = 25;
}
