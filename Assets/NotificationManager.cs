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

    [SerializeField] private TMP_Text xpIncreaseText;
    [SerializeField] private GameObject xpIncreaseTextPrefab;
    [SerializeField] private TMP_Text goldIncreaseText;


    void OnEnable(){
        PlayerStats.handleLevelIncrease += LevelUpNotification;
    }
    void OnDisable(){
        PlayerStats.handleLevelIncrease -= LevelUpNotification;
    }


    void LevelUpNotification(){
        notificationText.text = "Level Increase!";
        notificationObject.SetActive(true);
        StartCoroutine(DisableNotificationText());
    }
    IEnumerator DisableNotificationText(){
        yield return new WaitForSeconds(4f);
        notificationObject.SetActive(false);
    }


    void IncreaseXPText(int amount){
        
    }
}
