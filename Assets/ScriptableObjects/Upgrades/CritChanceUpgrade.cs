using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Crit Chance")]
public class CritChanceUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        PlayerStats.playerStats.CritChanceMod += increaseAmount;
    }
}
