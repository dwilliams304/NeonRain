using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Health")]
public class HealthUpgrade : PlayerUpgrades
{

    public override void UpgradeChosen()
    {
        Debug.Log("PLEASE IMPLEMENT HEALTH UPGRADE!");
        UIManager.uiManagement.UpdateHealthBar();
    }
}
