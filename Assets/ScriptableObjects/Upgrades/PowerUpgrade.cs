using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Upgrades/ +Power")]
public class PowerUpgrade : PlayerUpgrades
{


    public override void UpgradeChosen()
    {
        Debug.Log("Clicked");
    }
}
