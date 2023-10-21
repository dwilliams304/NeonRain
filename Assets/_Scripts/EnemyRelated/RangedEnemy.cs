using UnityEngine;

public class RangedEnemy : EnemyAI
{
    Enemy enemy;

    float lastAttack;

    // public GameObject bullet;
    public float projectileSpeed = 40f;
    public float maxDistance = 15f;

    private Transform player;
    [SerializeField] private Transform _firePoint;

    void Start(){
        enemy = GetComponent<Enemy>();
        enemyData = enemy.enemyData;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackRange = enemyData.attackRange;
        attackSpeed = enemyData.attackSpeed;
    }

    void Update(){
        RotateTowards(player.position);
        if(Vector2.Distance(transform.position, player.position) <= maxDistance){
            if(Time.time > lastAttack + attackSpeed){
                lastAttack = Time.time;
                GameObject bullet = ObjectPooler.current.GetPooledEnemyBullet(); //Grab a bullet from the bullet pool
                if(bullet == null) return; //If we don't have any bullets, do nothing (SHOULDNT HAPPEN)
                bullet.transform.position = _firePoint.position;
                bullet.transform.rotation = _firePoint.rotation; //Set the bullet to instantiate where the firing point is
                bullet.SetActive(true); //Set it active
                bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * projectileSpeed, ForceMode2D.Impulse); //Add force to it
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
