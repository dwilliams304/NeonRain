using UnityEngine;
using TMPro;

public class FPSUpdater : MonoBehaviour
{
    float fps;
    float updateTimer = 0.2f;

    [SerializeField] private TMP_Text fpsText;

    void Update(){
        UpdateFPSText();
    }

    void UpdateFPSText(){
        updateTimer -= Time.deltaTime;
        if(updateTimer <= 0){
            fps = 1f / Time.unscaledDeltaTime;
            fpsText.text = "FPS: " + Mathf.RoundToInt(fps);
            updateTimer = 0.2f;
        }
    }
}
