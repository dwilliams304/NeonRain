using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Fire Rate")]
public class FireRateUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        PlayerStatModifier.Instance.ChangeFireRateMod(-increaseAmount);
    }
}
