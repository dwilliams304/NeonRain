using UnityEngine;

public class InsideRainFilter : MonoBehaviour
{
    [SerializeField] AudioLowPassFilter rainFilter;


    void OnTriggerEnter2D(Collider2D coll){
        if(coll.CompareTag("Player")){
            rainFilter.cutoffFrequency = 2500f;
        }
    }
}
