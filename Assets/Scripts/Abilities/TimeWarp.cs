using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/TimeWarp")]
public class TimeWarp : AbilityBase
{
    public override void UseAbility()
    {
        Time.timeScale = 0.5f;
    }


    public override void AbilityComplete()
    {
        Time.timeScale = 1f;
    }
}
