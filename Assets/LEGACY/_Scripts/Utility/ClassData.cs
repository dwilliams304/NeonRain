using UnityEngine;


[CreateAssetMenu(menuName = "Classes/Default Class")]
public class ClassData : ScriptableObject
{
    
    [Header("Class Name")]
    public string ClassName = "Default Name";

    [Header("Health")]
    public int MaxHealth = 100;

    [Header("Movement")]
    public float MoveSpeed = 8f;
    public float DashSpeed = 30f;
    public float DashCooldown = 1f;

    [Header("Base Modifiers")]
    public int CritChance = 10;
    public float DamageDone = 1f;
    public float DamageTaken = 1f;
    public float CritMultiplier = 3f;
    public float GoldMod = 1f;
    public float FireRateMod = 1f;

    [Header("Starter Ability")]
    public AbilityBase StartingAbility;

}
