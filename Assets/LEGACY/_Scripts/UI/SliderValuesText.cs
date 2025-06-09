using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderValuesText : MonoBehaviour
{
    private Slider slider;
    TMP_Text text;

    void Awake()
    {
        slider = GetComponentInParent<Slider>();
        text = GetComponent<TMP_Text>();
        slider.onValueChanged.AddListener(delegate{ChangeSliderText();});
        text.text = slider.value.ToString() + " / " + slider.maxValue.ToString();
        if (slider == null)
        {
            Debug.LogError("<color=red>No slider found in parent!</color>");
        }

    }

    void ChangeSliderText(){
        text.text = slider.value.ToString() + " / " + slider.maxValue.ToString();
    }
}
