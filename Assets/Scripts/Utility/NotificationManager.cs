using System.Collections;
using UnityEngine;
using TMPro;


public class NotificationManager : MonoBehaviour
{


    [Header("Difficulty Increased")]
    [SerializeField] private GameObject diff_incr_obj;

    [Header("Tier Increased")]
    [SerializeField] private GameObject corr_incr_obj;
    [SerializeField] private TMP_Text buffText;
    [SerializeField] private TMP_Text debuffText;




    void OnEnable(){
        DifficultyScaler.diffIncreased += DifficultyIncreasedNotification;
        CorruptionManager.corruptionTierIncrease += CorruptionTierNotification;
        // XPManager.Instance.onXPChange += AddedXPText;
    }
    void OnDisable(){
        CorruptionManager.corruptionTierIncrease -= CorruptionTierNotification;
        DifficultyScaler.diffIncreased -= DifficultyIncreasedNotification;

    }




    void CorruptionTierNotification(int tier){
        StartCoroutine(ShowNotification(corr_incr_obj, 3.9f));
        switch(tier){
            case 1:
                buffText.text = "";
                debuffText.text = "";
                break;

            case 2:
                buffText.text = "";
                debuffText.text = "";
                break;

            case 3:
                buffText.text = "";
                debuffText.text = "";
                break;

            case 4:
                buffText.text = "";
                debuffText.text = "";
                break;

            case 5:
                buffText.text = "";
                debuffText.text = "";
                break;
        }
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
