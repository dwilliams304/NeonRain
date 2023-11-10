using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ping : MonoBehaviour
{
    private RectTransform _rectTransform;
    private TMP_Text _distText;
    private Transform _totemXf;
    private Transform _playerXf;


    void Start(){
        _distText = GetComponentInChildren<TMP_Text>();
        _rectTransform = GetComponent<RectTransform>();
        _playerXf = GameObject.FindGameObjectWithTag("Player").transform;
        _totemXf = FindObjectOfType<CorruptionCleanseTotem>().transform;
    }

    void Update(){
        Vector3 fromPos= Camera.main.transform.position;
        fromPos.z = 0f;
        Vector3 dir = (_totemXf.position - fromPos).normalized;
        float radius = 750f;
        _rectTransform.anchoredPosition = dir * radius;

        int dist = Mathf.RoundToInt(Vector3.Distance(_totemXf.position, _playerXf.position) / 3f);
        _distText.text = dist + "M";
    }
}
