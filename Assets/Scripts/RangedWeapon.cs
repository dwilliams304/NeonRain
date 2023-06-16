using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{

    public float fireRate;
    public float reloadSpeed;
    public int damage;
    public abstract void Fire();
}
