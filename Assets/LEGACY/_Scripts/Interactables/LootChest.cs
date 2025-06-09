using TMPro;
using UnityEngine;

public class LootChest : MonoBehaviour, IInteractable
{
    [SerializeField] private int _goldCost;
    [SerializeField] private float _addtlLuck = 1f;
    [SerializeField] Vector3 lootDropOffset;

    private TMP_Text infoText;

    void Start(){
        infoText = gameObject.GetComponentInChildren<TMP_Text>();
        infoText.text = $"Press [E] to open chest. \n (Cost: {_goldCost} gold)";
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            infoText.enabled = true;
            infoText.text = $"Press [E] to open chest. \n (Cost: {_goldCost} gold)";
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            infoText.enabled = false;
        }
    }
    
    public void Interacted(){
        if(Inventory.Instance.PlayerGold >= _goldCost){
            Inventory.Instance.RemoveGold(_goldCost);
            LootManager.Instance.DropLoot(gameObject.transform.position + lootDropOffset, _addtlLuck);
            Destroy(gameObject);
        }
        else{
            infoText.text = "Not enough gold!";
        }
    }
}
