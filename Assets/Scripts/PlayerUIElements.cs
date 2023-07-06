using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIElements : MonoBehaviour
{   
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
    }
}
