using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Crit Damage")]
public class CritDamageUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        PlayerStatModifier.ChangeCritDamageMod(increaseAmount);
    }
}
