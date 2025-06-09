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
        CorruptionManager.changeCorruptionTier += ChangePostProcessing;
    }
    void OnDisable(){
        CorruptionManager.changeCorruptionTier -= ChangePostProcessing;
    }

    void Start(){
        postProcessVol = GetComponent<Volume>();
        postProcessVol.profile.TryGet(out v);
        postProcessVol.profile.TryGet(out c);
    }

    void ChangePostProcessing(CorruptionTier tier){
        switch(tier){
            case CorruptionTier.Tier0:
            v.color.Override(vignette0);
            c.intensity.value = 0f;
                break;
                
            case CorruptionTier.Tier1:
            v.color.Override(vignette1);
            c.intensity.value = 0.05f;
                break;

            case CorruptionTier.Tier2:
            v.color.Override(vignette2);
            c.intensity.value = 0.1f;
                break;

            case CorruptionTier.Tier3:
            v.color.Override(vignette3);
            c.intensity.value = 0.15f;
                break;

            case CorruptionTier.Tier4:
            v.color.Override(vignette4);
            c.intensity.value = 0.2f;
                break;

            case CorruptionTier.Tier5:
            v.color.Override(vignette5);
            c.intensity.value = 0.25f;
                break;
        }
    }
}
