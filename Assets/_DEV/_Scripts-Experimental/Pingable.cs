using UnityEngine;

public class Pingable : MonoBehaviour
{
    [SerializeField] private Sprite _pingIcon;
    
    public Vector3 ReturnCurrentPos(){
        return transform.position;
    }

    public Sprite ReturnIcon(){
        return _pingIcon;
    }
}
