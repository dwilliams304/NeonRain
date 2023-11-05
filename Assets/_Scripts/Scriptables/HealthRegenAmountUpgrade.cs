using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Health Regen Amount")]
public class HealthRegenAmountUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        PlayerStatModifier.ChangeHealthRegenAmount(increaseAmount);
    }
}
