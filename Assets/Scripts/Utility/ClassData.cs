using UnityEngine;


[CreateAssetMenu(menuName = "Classes/Default Class")]
public class ClassData : ScriptableObject
{
    
    [Header("Class Name")]
    public string className = "Default Name";

    [Header("Health")]
    public int maxHealth = 100;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float dashSpeed = 30f;
    public float dashCooldown = 1f;

    [Header("Base Modifiers")]
    public int CritChance = 10;
    public float DamageDone = 1f;
    public float DamageTaken = 1f;
    public float CritMultiplier = 3f;
    public float GoldMod = 1f;

}
