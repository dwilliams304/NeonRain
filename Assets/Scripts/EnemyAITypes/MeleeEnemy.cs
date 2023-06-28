using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyAI
{
    Enemy enemy;

    float lastAttack;

    void Start(){
        enemy = GetComponent<Enemy>();
        enemyData = enemy.enemyData;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = enemyData.moveSpeed;
        attackRange = enemyData.attackRange;
        attackSpeed = enemyData.attackSpeed;
    }

    void Update(){
        if(Vector2.Distance(transform.position, target.position) > attackRange){
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }else if(Vector2.Distance(transform.position, target.position) <= attackRange){
            if(Time.time > lastAttack + attackSpeed){
                lastAttack = Time.time;
                float dmgToDo = enemy.DoDamage();
                PlayerStats.playerStats.TakeDamage(dmgToDo);
            }
        }
    }
}
