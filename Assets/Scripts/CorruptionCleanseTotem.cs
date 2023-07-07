using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionCleanseTotem : MonoBehaviour
{
    bool playerIn = false;

    [SerializeField] private List<GameObject> otherSpawns;

    void Start(){
        otherSpawns.AddRange(GameObject.FindGameObjectsWithTag("CorruptionTotem"));
        
    }

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
                int i = Random.Range(0, otherSpawns.Count);
                GameObject whereToGo = otherSpawns[i];
                gameObject.transform.position = whereToGo.transform.position;
                gameObject.transform.rotation = whereToGo.transform.rotation;
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
