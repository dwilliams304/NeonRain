using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PurchasableDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private int _goldCost;
    private TMP_Text _infoText;

    [SerializeField] private List<GameObject> _linkedDoors;

    void Start(){
        _infoText = GetComponentInChildren<TMP_Text>();
        _infoText.text = $"Press [E] to open door! \n (Cost: {_goldCost} gold)";
        _infoText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            _infoText.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            _infoText.enabled = false;
        }
    }

    public void Interacted()
    {
        if(Inventory.Instance.PlayerGold < _goldCost){
            _infoText.text = "Not enough gold!";
        }
        else{
            foreach(GameObject door in _linkedDoors){
                door.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

}
