using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Health")]
public class HealthUpgrade : PlayerUpgrades
{

    public override void UpgradeChosen()
    {
        PlayerStats.Instance.IncreaseMaxHealth(increaseAmount);
    }
}
