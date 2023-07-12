using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/TimeWarp")]
public class TimeWarp : AbilityBase
{

    public override void UseAbility()
    {
        Time.timeScale = 0.5f;

        PlayerController.Instance.MoveSpeed *= 2f;
    }


    public override void AbilityComplete()
    {
        PlayerController.Instance.MoveSpeed /= 2;
        Time.timeScale = 1f;
    }
}
