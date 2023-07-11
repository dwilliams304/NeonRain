using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource gunShotSource;
    [SerializeField] AudioClip gunShotClip;
    [SerializeField] AudioSource hitSource;

    void Awake()
    {
        Instance = this;
    }

    public void GUNSFX(){
        gunShotSource.Play();
    }

    public void HITSFX(){
        hitSource.Play();
    }
}
