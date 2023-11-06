using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    public delegate void GunSwapInitiated(List<GameObject> guns, Gun currentGun);
    public static GunSwapInitiated gunSwapInitiated;

    private List<GameObject> possibleWeapons = new List<GameObject>();

    [SerializeField] private float interactRange;

    
    void Update(){
        if(Input.GetButtonDown("Interact") && !WeaponSwapSystem.Swapping){
            CheckInteraction();
        }
    }

    void CheckInteraction(){
        possibleWeapons.Clear();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach(Collider2D coll in colliders){
            if(coll.gameObject.TryGetComponent(out IInteractable obj)){
                obj.Interacted();
            }
            else if(coll.TryGetComponent(out LootObject gun)){
                possibleWeapons.Add(gun.gameObject);
            }
        }
        if(possibleWeapons.Count > 0){
            gunSwapInitiated?.Invoke(possibleWeapons, Inventory.Instance.gun);
        }
    }
}
