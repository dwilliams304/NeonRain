using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NotificationManager : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private GameObject notificationObject;
    [SerializeField] private TMP_Text corruptionText;
    [SerializeField] private GameObject corruptionTextObject;


    [SerializeField] private TMP_Text goldIncreaseText;


    [SerializeField] Transform goldIncreaseLocation;


    void OnEnable(){
        PlayerStats.handleLevelIncrease += LevelUpNotification;
        // CorruptionManager.corruptionIncrease += CorruptionTierNotification;
        // XPManager.Instance.onXPChange += AddedXPText;
    }
    void OnDisable(){
        // CorruptionManager.corruptionIncrease -= CorruptionTierNotification;
        PlayerStats.handleLevelIncrease -= LevelUpNotification;
        // XPManager.Instance.onXPChange -= AddedXPText;
    }


    // void AddedXPText(int amount){
    //     GameObject temp = Instantiate(xpIncreaseTextPrefab, xpIncreaseLocation.position, Quaternion.identity);
    //     xpIncreaseText.text = $"+{amount}";
    //     StartCoroutine(DestroyTextObject(temp, 0.2f));
    // }
    void AddedGoldText(int amount){
        goldIncreaseText.text = $"+{amount}";
    }

    void LevelUpNotification(){
        notificationText.text = "Level Up!";
        StartCoroutine(DeactivateText(notificationObject, 4f));
    }

    // void CorruptionTierNotification(int tier){
    //     corruptionText.text = $"Tier {tier} entered.";
    //     StartCoroutine(DeactivateText(corruptionTextObject, 2f));
    // }




    // IEnumerator DestroyTextObject(GameObject toDestroy, float delay){
    //     yield return new WaitForSeconds(delay);
    //     Destroy(toDestroy);
    // }
    IEnumerator DeactivateText(GameObject toDeactivate, float delay){
        toDeactivate.SetActive(true);
        yield return new WaitForSeconds(delay);
        toDeactivate.SetActive(false);
    }
}
