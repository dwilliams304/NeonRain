using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float minDamage;
    public float maxDamage;
    public float critChance;
    public bool isCorrupted;
    public Sprite weaponSprite;
    public float fireRate;
    public float reloadSpeed;
    public int magSize;
    public int projectileSpeed;
    public int weaponRange;
    public int corruptionGain;
    public int currentWepTier;

    public enum Type{
        pistol,
        automaticRifle,
        shotgun,
        sniper
    }

    public enum Rarity{
        common,
        uncommon,
        rare,
        corrupted,
        legendary,
        unique
    }
    public Rarity rarity;
    public Type type;
}
