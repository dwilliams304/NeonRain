using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource _musicSource, _ambienceSource, _effectsSource, _uiSource;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void PlayEffectAudio(AudioClip clip){
        _effectsSource.PlayOneShot(clip);
    }

    public void PlayAmbienceAudio(AudioClip clip){
        _ambienceSource.PlayOneShot(clip);
    }
}
