using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionCleanseTotem : MonoBehaviour
{
    bool playerIn = false;

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            playerIn = true;
            Debug.Log("Player in! Value:" + playerIn);
        }
    }

    void OnTriggerStay2D(Collider2D coll){
        if(playerIn){
            if(Input.GetKey(KeyCode.E)){
                CorruptionManager.Instance.ChangeCorruptionTier(0);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            playerIn = false;
            Debug.Log("Player out! Value:" + playerIn);
        }
    }
}
