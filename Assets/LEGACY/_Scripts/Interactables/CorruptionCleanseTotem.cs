using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorruptionCleanseTotem : MonoBehaviour, IInteractable
{

    [SerializeField] private List<GameObject> otherSpawns;
    bool interactedWith = false;
    TMP_Text text;


    void Start(){
        otherSpawns.AddRange(GameObject.FindGameObjectsWithTag("CorruptionTotem"));
        text = GetComponentInChildren<TMP_Text>();
        text.text = "Press [E] to cleanse corruption!";
    }

    public void Interacted(){
        if(!interactedWith){
            interactedWith = true;
            CorruptionManager.Instance.ForceDecreaseTier();
            // Debug.Log("Relocating!");
            text.text = "Relocating...";
            StartCoroutine(Relocate());
        }
    }

    IEnumerator Relocate(){
        ParticleSystem p = GetComponent<ParticleSystem>();
        p.Play();
        yield return new WaitForSeconds(p.main.duration);
        text.text = "Press [E] to cleanse corruption!";
        int i = Random.Range(0, otherSpawns.Count);
        GameObject whereToGo = otherSpawns[i];
        gameObject.transform.position = whereToGo.transform.position;
        gameObject.transform.rotation = whereToGo.transform.rotation;
        interactedWith = false;
    }
}
