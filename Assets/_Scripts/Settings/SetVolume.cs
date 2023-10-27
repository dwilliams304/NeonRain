using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMasterLevel(float sliderValue){
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetEffectsLevel(float sliderValue){
        mixer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetAmbienceLevel(float sliderValue){
        mixer.SetFloat("AmbienceVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetMusicLevel(float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetUILevel(float sliderValue){
        mixer.SetFloat("UIVolume", Mathf.Log10(sliderValue) * 20);
    }
}
