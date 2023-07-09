using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CorruptionPostProcessing : MonoBehaviour
{
    public Volume postProcessVol;
    public Vignette v;
    public ChromaticAberration c;

    [SerializeField] private Color vignette0;
    [SerializeField] private Color vignette1;
    [SerializeField] private Color vignette2;
    [SerializeField] private Color vignette3;
    [SerializeField] private Color vignette4;
    [SerializeField] private Color vignette5;
    void OnEnable(){
        CorruptionManager.corruptionIncrease += ChangePostProcessing;
    }
    void OnDisable(){
        CorruptionManager.corruptionIncrease -= ChangePostProcessing;
    }

    void Start(){
        postProcessVol = GetComponent<Volume>();
        postProcessVol.profile.TryGet(out v);
        postProcessVol.profile.TryGet(out c);
    }

    void ChangePostProcessing(int tier){
        switch(tier){
            case 0:
            v.color.Override(vignette0);
            c.intensity.value = 0f;
                break;
            case 1:
            v.color.Override(vignette1);
            c.intensity.value = 0.05f;
                break;
            case 2:
            v.color.Override(vignette2);
            c.intensity.value = 0.1f;

                break;
            case 3:
            v.color.Override(vignette3);
            c.intensity.value = 0.15f;

                break;
            case 4:
            v.color.Override(vignette4);
            c.intensity.value = 0.2f;

                break;
            case 5:
            v.color.Override(vignette5);
            c.intensity.value = 0.25f;

                break;
        }
    }
}
