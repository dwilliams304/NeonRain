using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable{
    public void Interacted();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactRange;

    void Update(){
        if(Input.GetButtonDown("Interact")){
            CheckInteraction();
        }
    }

    void CheckInteraction(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach(Collider2D coll in colliders){
            if(coll.gameObject.TryGetComponent(out IInteractable obj)){
                obj.Interacted();
            }
        }
    }

}
