using UnityEngine;
using TMPro;

public class VersionText : MonoBehaviour
{
    
    private TMP_Text _text;
    void Start(){
        _text = GetComponent<TMP_Text>();
        _text.text = Application.version;
    }
}
