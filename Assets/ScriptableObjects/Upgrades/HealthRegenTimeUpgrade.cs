using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Health Regen Time")]
public class HealthRegenTimeUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        HealthRegenerator.Instance.RegenTime -= increaseAmount;
    }
}
