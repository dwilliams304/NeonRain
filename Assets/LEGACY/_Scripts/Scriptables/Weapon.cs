using UnityEngine;

public enum WeaponType{
    Gun,
    Sword
}

public enum Rarity{
    Common,
    Uncommon,
    Rare,
    Corrupted,
    Legendary,
    Unique
}


public abstract class Weapon : ScriptableObject
{
    public string weaponName = "Default Weapon";
    public float minDamage = 5f;
    public float maxDamage = 10f;
    public int critChance = 5;
    public bool isCorrupted = false;
    public Sprite weaponSprite;
    public int corruptionGain;
    public int currentWepTier;


    public WeaponType weaponType = WeaponType.Gun;
    public Rarity rarity = Rarity.Common;
}
