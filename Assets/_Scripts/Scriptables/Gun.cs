using UnityEngine;

public enum GunType{
    Pistol,
    AutomaticRifle,
    Shotgun,
    Sniper
}

[CreateAssetMenu(menuName = "Weapons/Gun")]
public class Gun : Weapon
{
    public int projectileSpeed = 30;
    public float reloadSpeed = 0.5f;
    public float fireRate = 0.5f;
    public int magSize = 30;
    public GunType gunType = GunType.Pistol;
}
