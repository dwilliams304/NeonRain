using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Crit Chance")]
public class CritChanceUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        PlayerStatModifier.Instance.ChangeCritChanceMod(Mathf.RoundToInt(increaseAmount));
    }
}
