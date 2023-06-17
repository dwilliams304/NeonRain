using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{

    public string enemyName;
    public int maxHealth;
    public float moveSpeed;
    public int minDamage;
    public int maxDamage;

    public int corruptionDrop;
    public int goldDrop;
    public List<ScriptableObject> lootDrops;
}
