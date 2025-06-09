using UnityEngine;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void Awake(){
        cam = Camera.main;
    }

    void Update(){
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
    }
}
