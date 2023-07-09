using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance;

    public delegate void XPChangeHandler(int amount);
    public event XPChangeHandler onXPChange;
    [SerializeField] private TMP_Text xpIncreaseText;
    [SerializeField] private GameObject xpIncreaseTextPrefab;
    [SerializeField] Transform xpIncreaseLocation;
    public float XPModifier = 1f;

    [SerializeField] Canvas UI;

    void Awake(){
        if(Instance != null & Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
    }

    public void AddExperience(int amount){
        int xpAfterMod = Mathf.CeilToInt(amount * XPModifier);
        onXPChange?.Invoke(xpAfterMod);
        AddedXPText(xpAfterMod);
    }
    void AddedXPText(int amount){
        GameObject temp = Instantiate(xpIncreaseTextPrefab, xpIncreaseLocation.transform.position, Quaternion.identity);
        temp.transform.parent = UI.transform;
        TMP_Text text = temp.GetComponentInChildren<TMP_Text>();
        text.text = $"+{amount}";
    }

}
