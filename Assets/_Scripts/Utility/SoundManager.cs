using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Auido Sources")]
    [SerializeField] AudioSource _musicSource, _ambienceSource, _effectsSource, _uiSource;

    [SerializeField] private float _audioWaitTime;
    private float timeSinceLastAudio;


    [Header("UI Specific Sounds")]
    [SerializeField] private AudioClip _click1;
    [SerializeField] private AudioClip _hover1;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void PlayDelayedEffect(AudioClip clip){
        if(Time.time > timeSinceLastAudio + _audioWaitTime){
            timeSinceLastAudio = Time.time;
            _effectsSource.PlayOneShot(clip);
        }
    }

    public void PlayEffectAudio(AudioClip clip){
        _effectsSource.PlayOneShot(clip);
    }

    public void PlayAmbienceAudio(AudioClip clip){
        _ambienceSource.PlayOneShot(clip);
    }

    public void UI_PlayHoverSound() => _uiSource.PlayOneShot(_hover1);
    public void UI_PlayerClickSound() => _uiSource.PlayOneShot(_click1);
}
