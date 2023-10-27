using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] private List<Slider> _volumeSliders;

    public void ConfirmSaveSettings(){
        foreach(Slider slider in _volumeSliders){
            PlayerPrefs.SetFloat(slider.name, slider.value);
        }
    }
}
