using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Speed")]
public class MoveSpeedUpgrade : PlayerUpgrades
{

    public override void UpgradeChosen()
    {
        PlayerStatModifier.Instance.ChangeMoveSpeedMod(increaseAmount);
    }
}
