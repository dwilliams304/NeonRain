using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Fire Rate")]
public class FireRateUpgrade : PlayerUpgrades
{
    public override void UpgradeChosen()
    {
        Combat.Instance.FireRateMod -= increaseAmount;
    }
}
