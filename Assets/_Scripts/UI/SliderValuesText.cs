using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderValuesText : MonoBehaviour
{
    private Slider slider;
    TMP_Text text;

    void Start(){
        slider = GetComponentInParent<Slider>();
        text = GetComponent<TMP_Text>();

        if(slider == null){
            Debug.LogError("<color=red>No slider found in parent!</color>");
        }
    }

    public void ChangeSliderText(){
        text.text = slider.value.ToString() + " / " + slider.maxValue.ToString();
    }
}
