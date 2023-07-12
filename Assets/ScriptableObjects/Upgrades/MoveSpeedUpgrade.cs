using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Speed")]
public class MoveSpeedUpgrade : PlayerUpgrades
{

    public override void UpgradeChosen()
    {
        PlayerController.Instance.MoveSpeed += increaseAmount;
    }
}
