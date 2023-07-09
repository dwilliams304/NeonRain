using UnityEngine;
using Pathfinding;

public class MeleeEnemy : EnemyAI
{
    Enemy enemy;

    float lastAttack;
    AIPath pathfinder;
    AIDestinationSetter aIDestinationSetter;

    void Start(){
        enemy = GetComponent<Enemy>();
        pathfinder = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        enemyData = enemy.enemyData;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        attackRange = enemyData.attackRange;
        attackSpeed = enemyData.attackSpeed;
        pathfinder.maxSpeed = enemyData.moveSpeed;
        pathfinder.endReachedDistance = attackRange;
        aIDestinationSetter.target = target;
    }

    void Update(){
        if(Vector2.Distance(transform.position, target.position) <= attackRange){
            if(Time.time > lastAttack + attackSpeed){
                lastAttack = Time.time;
                float dmgToDo = enemy.DoDamage();
                PlayerStats.playerStats.TakeDamage(dmgToDo);
            }
        }
    }
}
