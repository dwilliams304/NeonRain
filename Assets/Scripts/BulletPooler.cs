using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{

    public static BulletPooler current;
    public GameObject bulletObj;
    public int bulletAmnt;
    public bool willGrow;

    private List<GameObject> pooledBullets;

    void Awake(){
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledBullets = new List<GameObject>();
        for(int i = 0; i < bulletAmnt; i++){
            GameObject bullet = Instantiate(bulletObj);
            bullet.SetActive(false);
            pooledBullets.Add(bullet);
        }
    }

    public GameObject GetPooledBullet(){
        for(int i = 0; i < pooledBullets.Count; i++){
            if(!pooledBullets[i].activeInHierarchy){
                return pooledBullets[i];
            }
        }
        
        if(willGrow){
            GameObject bullet = Instantiate(bulletObj);
            pooledBullets.Add(bullet);
            return bullet;
        }
        
        return null;
    }
}
