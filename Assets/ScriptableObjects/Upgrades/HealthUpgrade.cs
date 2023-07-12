using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Health")]
public class HealthUpgrade : PlayerUpgrades
{

    public override void UpgradeChosen()
    {
        PlayerStats.playerStats.PlayerMaxHealth += increaseAmount;
        PlayerStats.playerStats.CurrentHealth += increaseAmount;
        UIManager.uiManagement.UpdateHealthBar();
    }
}
