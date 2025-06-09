using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Power")]
public class PowerUpgrade : PlayerUpgrades
{

    public override void UpgradeChosen()
    {
        PlayerStatModifier.Instance.ChangeDamageDoneMod(increaseAmount);
    }
}
