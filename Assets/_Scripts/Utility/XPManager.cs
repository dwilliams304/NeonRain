using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    [SerializeField] private GameObject xpIncreaseTextPrefab;
    [SerializeField] Transform xpIncreaseLocation;
    
    public float XPModifier { get; private set; } = 1f;

    [SerializeField] Canvas UI;


    public void AddExperience(int amount){
        int xpAfterMod = Mathf.RoundToInt(amount * XPModifier);
        AddedXPText(xpAfterMod);
    }
    void AddedXPText(int amount){
        GameObject temp = Instantiate(xpIncreaseTextPrefab, xpIncreaseLocation.transform.position, Quaternion.identity);
        temp.transform.SetParent(UI.transform, true);
        TMP_Text text = temp.GetComponentInChildren<TMP_Text>();
        text.text = $"+{amount}";
    }

}
