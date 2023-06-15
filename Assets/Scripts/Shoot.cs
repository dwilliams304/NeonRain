using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _bullet;
    public float force;


    void Update(){
        if(Input.GetButtonDown("Fire1")){
            Fire();
        }
    }


    void Fire(){
        GameObject bullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(_firePoint.up * force, ForceMode2D.Impulse);
    }
}
