using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler current;
    [Header("Player Objects")]
    [SerializeField] private GameObject playerBulletObj;
    [SerializeField] private int playerBulletsAmnt = 50;
    [SerializeField] private GameObject sniperBulletObj;
    [SerializeField] private int sniperBulletsAmnt = 25;
    private List<GameObject> pooledPlayerBullets;
    private List<GameObject> pooledSniperBullets;
    
    [Header("Enemy Objects")]
    [SerializeField] private GameObject enemyBulletObj;
    [SerializeField] private int enemyBulletsAmnt = 100;
    private List<GameObject> pooledEnemyBullets;
    [SerializeField] private GameObject damageTextObj;
    [SerializeField] private int damageTextAmnt;

    [SerializeField] private bool willGrow;

    [Header("Impact effects")]
    [SerializeField] private GameObject bulletImpactPrefab;
    [SerializeField] private int impactAmount;
    private List<GameObject> pooledBulletImpacts;

    
    

    void Awake(){
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledPlayerBullets = new List<GameObject>();
        pooledSniperBullets = new List<GameObject>();

        pooledBulletImpacts = new List<GameObject>();
        pooledEnemyBullets = new List<GameObject>();
        for(int i = 0; i < playerBulletsAmnt; i++){
            GameObject playerBullet = Instantiate(playerBulletObj);
            playerBullet.transform.parent = gameObject.transform;

            playerBullet.SetActive(false);
            pooledPlayerBullets.Add(playerBullet);
        }
        for(int i = 0; i < sniperBulletsAmnt; i++){
            GameObject sniperBullet = Instantiate(sniperBulletObj);
            sniperBullet.transform.parent = gameObject.transform;

            sniperBullet.SetActive(false);
            pooledSniperBullets.Add(sniperBullet);
        }

        for(int i = 0; i < enemyBulletsAmnt; i++){
            GameObject enemyBullet = Instantiate(enemyBulletObj);
            enemyBullet.transform.parent = gameObject.transform;

            enemyBullet.SetActive(false);
            pooledEnemyBullets.Add(enemyBullet);
        }

        for(int i = 0; i < impactAmount; i++){
            GameObject impact = Instantiate(bulletImpactPrefab);
            impact.transform.parent = gameObject.transform;

            impact.SetActive(false);
            pooledBulletImpacts.Add(impact);
        }
    }

    public GameObject GetPooledPlayerBullet(GunType gunType, float dmg, bool wasCrit){
        GameObject bullet;
        switch(gunType){
            case GunType.Sniper:
                for(int i = 0; i < pooledSniperBullets.Count; i++){
                    if(!pooledSniperBullets[i].activeInHierarchy){
                        SniperBullet s = pooledSniperBullets[i].GetComponent<SniperBullet>();
                        s.DamageAmount = dmg;
                        s.isCrit = wasCrit;
                        return pooledSniperBullets[i];
                    }
                }
                if(willGrow){
                    bullet = Instantiate(sniperBulletObj);
                    bullet.GetComponent<SniperBullet>().DamageAmount = dmg;
                    pooledSniperBullets.Add(bullet);
                    return bullet;
                }
                return null;
            default:
                for(int i = 0; i < pooledPlayerBullets.Count; i++){
                    if(!pooledPlayerBullets[i].activeInHierarchy){
                        pooledPlayerBullets[i].GetComponent<Bullet>().DamageAmount = dmg;
                        Bullet b = pooledPlayerBullets[i].GetComponent<Bullet>();
                        b.DamageAmount = dmg;
                        b.isCrit = wasCrit;
                        return pooledPlayerBullets[i];
                    }
                }
                if(willGrow){
                    bullet = Instantiate(playerBulletObj);
                    bullet.GetComponent<Bullet>().DamageAmount = dmg;
                    pooledPlayerBullets.Add(bullet);
                    return bullet;
                }
                return null;
        }
        
    }

    public GameObject GetPooledSniperBullet(){
        for(int i = 0; i < pooledSniperBullets.Count; i++){
            if(!pooledSniperBullets[i].activeInHierarchy){
                return pooledSniperBullets[i];
            }
        }
        if(willGrow){
            GameObject bullet = Instantiate(sniperBulletObj);
            pooledSniperBullets.Add(bullet);
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

    public GameObject GetPooledBulletImpact(){
        for(int i = 0; i < pooledBulletImpacts.Count; i++){
            if(!pooledBulletImpacts[i].activeInHierarchy){
                return pooledBulletImpacts[i];
            }
            if(willGrow){
                GameObject impact = Instantiate(bulletImpactPrefab);
                pooledBulletImpacts.Add(impact);
                return impact;
            }
        }
        return null;
    }
}
