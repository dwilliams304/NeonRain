using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string weaponName = "Default Weapon";
    public float minDamage = 5f;
    public float maxDamage = 10f;
    public float critChance = 5f;
    public bool isCorrupted = false;
    public Sprite weaponSprite;
    public float fireRate = 0.5f;
    public float reloadSpeed = 0.5f;
    public int magSize = 30;
    public int projectileSpeed = 30;
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
