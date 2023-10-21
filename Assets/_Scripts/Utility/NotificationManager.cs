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
                buffText.text = "+5% Damage\n+5% XP Gain \n+1% Luck \n (NOT FINAL)";
                debuffText.text = "+5% Damage Taken \n (NOT FINAL)";
                break;

            case 2:
                buffText.text = "+10% Damage Done \n+25% XP Gain \n+2% Luck \n (NOT FINAL)";
                debuffText.text = "+10% Damage Taken \n (NOT FINAL)";
                break;

            case 3:
                buffText.text = "+25% Damage Done \n +50% XP Gain \n+3% Luck \n (NOT FINAL)";
                debuffText.text = "+30% Damage Taken \n (NOT FINAL)";
                break;

            case 4:
                buffText.text = "+50% Damage Done \n+100% XP Gain \n+3% Luck \n (NOT FINAL)";
                debuffText.text = "+75% Damage Taken \n (NOT FINAL)";
                break;

            case 5:
                buffText.text = "+100% Damge Done \n+200% XP Gain \n+3% Luck \n (NOT FINAL)";
                debuffText.text = "+150% Damage Taken \n (NOT FINAL)";
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
