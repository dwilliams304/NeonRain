using UnityEngine;

public class AbilityBase : ScriptableObject
{
    public new string name;
    public float coolDownTime;
    public float activeTime;
    public Sprite abilityIcon;


    public virtual void UseAbility(){}
    public virtual void AbilityComplete(){}
}
