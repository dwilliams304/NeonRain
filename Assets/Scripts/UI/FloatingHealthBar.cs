using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void Awake(){
        cam = Camera.main;
    }

    public void UpdateHealthBar(float currentValue, float maxValue){
        _slider.maxValue = maxValue;
        _slider.value = currentValue;
    }

    void Update(){
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
    }
}
