using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "R_Weapon", menuName = "Weapons/RangedWeapon", order = 1)]
public class RangedWeaponData : ScriptableObject
{
    public float fireRate;
    public float reloadSpeed;
    public float projectileSpeed;
    public float damage;
    public int weaponRange;
    public bool isCorrupted;
    public int corruptionGain;

    public Sprite weaponSprite;
    
    public enum Rarity{
        common,
        uncommon,
        rare,
        mythic,
        legendary,
    }

    public enum Type{
        pistol,
        automaticRifle,
        shotgun,
        sniper
    }

    public Type type;
    public Rarity rarity;
    
    [Range(1, 10000)]
    [Tooltip("1 - 10,000 -> higher is more likely to drop. So anywhere between a 0.01% to a 100%")]
    public int dropChance;

}
