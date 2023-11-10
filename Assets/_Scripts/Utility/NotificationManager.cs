using System.Collections;
using UnityEngine;
using TMPro;


public class NotificationManager : MonoBehaviour
{


    [Header("Difficulty Increased")]
    [SerializeField] private GameObject diff_incr_obj;

    [Header("Tier Increased")]
    [SerializeField] private GameObject corr_incr_obj;




    void OnEnable(){
        DifficultyScaler.diffIncreased += DifficultyIncreasedNotification;
        CorruptionManager.onCorruptierTierIncrease += CorruptionTierNotification;
        // XPManager.Instance.onXPChange += AddedXPText;
    }
    void OnDisable(){
        // CorruptionManager.corruptionTierIncrease -= CorruptionTierNotification;
        DifficultyScaler.diffIncreased -= DifficultyIncreasedNotification;
        CorruptionManager.onCorruptierTierIncrease -= CorruptionTierNotification;

    }




    void CorruptionTierNotification(){
        StartCoroutine(ShowNotification(corr_incr_obj, 3.9f));
    }



    void DifficultyIncreasedNotification(){
        StartCoroutine(ShowNotification(diff_incr_obj, 3.9f));
    }


    // IEnumerator DestroyTextObject(GameObject toDestroy, float delay){
    //     yield return new WaitForSeconds(delay);
    //     Destroy(toDestroy);
    // }
    IEnumerator ShowNotification(GameObject toDeactivate, float delay){
        toDeactivate.SetActive(true);
        yield return new WaitForSeconds(delay);
        toDeactivate.SetActive(false);
    }
}
