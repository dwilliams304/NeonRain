using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Gold")]
public class GoldIncreaseUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        PlayerStatModifier.ChangeAdditionalGoldMod(increaseAmount);
    }
}
