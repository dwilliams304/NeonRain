using UnityEngine;


public abstract class PlayerUpgrades : ScriptableObject
{
    public Sprite icon;
    public string upgradeName;
    public string toolTipDescription;
    public float increaseAmount;
    public bool isPercentUpgrade;
    public virtual void UpgradeChosen(){}
}
