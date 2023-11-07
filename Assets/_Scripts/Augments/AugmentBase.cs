using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AugmentBase : MonoBehaviour
{
    public string AugmentName;
    public string AugmentDescription;
    public bool Purchased;
    public int TotalAllowedToBuy;
    public int TotalBought;
    public int AugmentCost;
    public Button AugButton;
    public TMP_Text BoughtText;
    public virtual void UnlockAugment(){}
    
}
