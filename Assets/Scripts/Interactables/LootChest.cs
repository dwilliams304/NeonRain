using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour, IInteractable
{

    [SerializeField] Vector3 lootDropOffset;
    public void Interacted(){
        LootManager.lootManager.DropLoot(gameObject.transform.position + lootDropOffset, 1f);
        Destroy(gameObject);
    }
}