using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "M_Weapon", menuName = "Weapons/MeleeWeapon", order = 1)]
public class MeleeWeaponData : ScriptableObject
{
    public float swingSpeed;
    public int swingRange;
    public float minDamage;
    public float maxDamage;
    public float critChance;
    public bool isCorrupted;
    public int corruptionGain;

    public Sprite weaponSprite;
    
        public enum Rarity{
        common,
        uncommon,
        rare,
        mythic,
        legendary,
        uniqueCorrupted
    }


    public enum Type{
        dagger,
        sword,
        longsword,
    }
    public Type type;
    public Rarity rarity;

    [Range(1, 10000)]
    [Tooltip("1 - 10,000 -> higher is more likely to drop. So anywhere between a 0.01% to a 100%")]
    public int dropChance;
}
