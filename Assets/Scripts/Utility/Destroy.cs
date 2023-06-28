using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis());    
    }
    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    
}
