using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionCleanseTotem : MonoBehaviour
{
    bool playerIn = false;

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            playerIn = true;
        }
    }

    void OnTriggerStay2D(Collider2D coll){
        if(Input.GetKey(KeyCode.E)){
            CorruptionManager.Instance.ChangeCorruptionTier(0);
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        playerIn = false;
    }
}
