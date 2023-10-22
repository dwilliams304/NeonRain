using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string weaponName = "Default Weapon";
    public float minDamage = 5f;
    public float maxDamage = 10f;
    public int critChance = 5;
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
        Pistol,
        AutomaticRifle,
        Shotgun,
        Sniper
    }

    public enum Rarity{
        Common,
        Uncommon,
        Rare,
        Corrupted,
        Legendary,
        Unique
    }
    public Rarity rarity;
    public Type type;
}
