using UnityEngine;

public enum SwordType{
    Sword,
    Saber,
    Dagger,
    Scythe
}

[CreateAssetMenu(menuName = "Weapons/Sword")]
public class Sword : Weapon
{
    public float swingSpeed = 1;
    public float swingCoolDown = 0.75f;
    public SwordType swordType = SwordType.Sword;
}
