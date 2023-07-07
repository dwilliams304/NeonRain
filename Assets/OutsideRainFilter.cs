using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideRainFilter : MonoBehaviour
{
    [SerializeField] AudioLowPassFilter rainFilter;


    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            rainFilter.cutoffFrequency = 22000f;
        }
    }
}
