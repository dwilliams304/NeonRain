using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler current;
    [Header("Player Objects")]
    [SerializeField] private GameObject playerBulletObj;
    [SerializeField] private int playerBulletsAmnt = 50;
    private List<GameObject> pooledPlayerBullets;
    
    [Header("Enemy Objects")]
    [SerializeField] private GameObject enemyBulletObj;
    [SerializeField] private int enemyBulletsAmnt = 100;
    private List<GameObject> pooledEnemyBullets;
    [SerializeField] private GameObject damageTextObj;
    [SerializeField] private int damageTextAmnt;

    // [Header("Loot Objects")]
    // [SerializeField] private GameObject commonLootObj;
    // [SerializeField] private GameObject uncommonLootObj;
    // [SerializeField] private GameObject rareLootObj;
    // [SerializeField] private GameObject corruptedLootObj;
    // [SerializeField] private GameObject legendaryLootObj;
    // [SerializeField] private GameObject uniqueLootObj;
    //Might implement eventually

    [SerializeField] private bool willGrow;

    
    

    void Awake(){
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledPlayerBullets = new List<GameObject>();
        pooledEnemyBullets = new List<GameObject>();
        for(int i = 0; i < playerBulletsAmnt; i++){
            GameObject playerBullet = Instantiate(playerBulletObj);
            playerBullet.SetActive(false);
            pooledPlayerBullets.Add(playerBullet);
            playerBullet.transform.parent = gameObject.transform;
        }

        for(int i = 0; i < enemyBulletsAmnt; i++){
            GameObject enemyBullet = Instantiate(enemyBulletObj);
            enemyBullet.SetActive(false);
            pooledEnemyBullets.Add(enemyBullet);
            enemyBullet.transform.parent = gameObject.transform;
        }
    }

    public GameObject GetPooledPlayerBullet(){
        for(int i = 0; i < pooledPlayerBullets.Count; i++){
            if(!pooledPlayerBullets[i].activeInHierarchy){
                return pooledPlayerBullets[i];
            }
        }
        if(willGrow){
            GameObject bullet = Instantiate(playerBulletObj);
            pooledPlayerBullets.Add(bullet);
            return bullet;
        }
        return null;
    }

    public GameObject GetPooledEnemyBullet(){
        for(int i = 0; i < pooledEnemyBullets.Count; i++){
            if(!pooledEnemyBullets[i].activeInHierarchy){
                return pooledEnemyBullets[i];
            }
            if(willGrow){
                GameObject enemyBullet = Instantiate(enemyBulletObj);
                pooledEnemyBullets.Add(enemyBullet);
                return enemyBullet;
            }
        }
        return null;
    }
}
