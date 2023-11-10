using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingUIHandler : MonoBehaviour
{  
    [SerializeField] private Transform _playerXf;
    private List<Pingable> _pingables = new List<Pingable>();
    private RectTransform _rectTransform;
    private GameObject _pingPrefab;

    float _radius = 500f;

    void OnEnable(){
        PingLocator.onPingFinished += ShowPings;
        _rectTransform = GetComponent<RectTransform>();
    }

    void OnDisable(){
        PingLocator.onPingFinished -= ShowPings;
    }

    void ShowPings(List<Pingable> pings){
        _pingables.Clear();
        _pingables = pings;
        foreach(Pingable ping in _pingables){
        }
    }

    // void Update(){
    //     Vector3 fromPos = Camera.main.transform.position;
    //     fromPos.z = 0f;
    //     foreach(Pingable ping in _pingables){
    //         Vector3 dir = (ping.ReturnCurrentPos() - fromPos).normalized;
    //         _rectTransform.anchoredPosition = dir * _radius;
    //     }
    // }
}
