using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsGetter : MonoBehaviour
{
    [SerializeField] private List<VolumeSlider> _volumeSliders;

    [System.Serializable]
    private class VolumeSlider{
        public Slider slider;
        public string sliderName;
    }

    void Start(){
        foreach(VolumeSlider obj in _volumeSliders){
            obj.slider.value = PlayerPrefs.GetFloat(obj.sliderName);
            if(obj.slider.value == 0) {
                obj.slider.value = 0.5f;
            }
        }
    }
}
