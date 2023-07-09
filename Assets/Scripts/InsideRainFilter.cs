using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InsideRainFilter : MonoBehaviour
{
    [SerializeField] AudioLowPassFilter rainFilter;


    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            rainFilter.cutoffFrequency = 2500f;
        }
    }
}
