using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredyEnemy : EnemyAI
{
    Enemy enemy;

    float lastAttack;

    public GameObject bullet;
    public float projectileSpeed = 40f;

    private Transform player;
    [SerializeField] private Transform _firePoint;

    void Start(){
        enemy = GetComponent<Enemy>();
        enemyData = enemy.enemyData;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = enemyData.moveSpeed;
        attackRange = enemyData.attackRange;
        attackSpeed = enemyData.attackSpeed;
    }

    void Update(){
        RotateTowards(player.position);
        if(Vector2.Distance(transform.position, player.position) < attackRange){
            transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        }else if(Vector2.Distance(transform.position, player.position) >= attackRange){
            if(Time.time > lastAttack + attackSpeed){
                lastAttack = Time.time;
                GameObject temp = Instantiate(bullet, _firePoint.position, _firePoint.rotation);
                temp.GetComponent<EnemyProjectile>().damage = enemy.DoDamage();
                temp.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * projectileSpeed, ForceMode2D.Impulse);
            }
        }
    }

    void RotateTowards(Vector2 target){
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var offset = -90f;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
