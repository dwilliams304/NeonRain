using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    private CinemachineVirtualCamera _cam;
    private CinemachineBasicMultiChannelPerlin _noise;

    private float _shakeTimer;
    private float _shakeDuration;
    private float _startingIntensity;

    void Awake(){
        Instance = this;
        _cam = GetComponent<CinemachineVirtualCamera>();
        _noise = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time){
        _noise.m_AmplitudeGain = intensity;
        _shakeTimer = time;
        _shakeDuration = time;
        _startingIntensity = intensity;
    }

    void Update(){
        if(_shakeTimer > 0){
            _shakeTimer -= Time.deltaTime;
            if(_shakeTimer <= 0f){
                _noise.m_AmplitudeGain = 0f;
                Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeDuration));
            }
        }
    }
}
