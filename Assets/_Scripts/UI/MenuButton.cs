using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private bool _scaleOnHover, _backOnHover, _soundOnHover = true;
    [SerializeField] private GameObject _backingImage;

    [SerializeField] private Vector2 _originalSize = new Vector2(1f, 1f);
    [SerializeField] private Vector2 _toScaleTo = new Vector2(1.1f, 1.1f);
    
    void OnEnable(){
        transform.localScale = _originalSize;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.UI_PlayerClickSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_soundOnHover) SoundManager.Instance.UI_PlayHoverSound();
        if(_scaleOnHover) transform.localScale = _toScaleTo;
        if(_backOnHover && _backingImage != null) _backingImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_scaleOnHover) transform.localScale = _originalSize;
        if(_backOnHover && _backingImage != null) _backingImage.SetActive(false);
    }

}
