using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private bool _scaleOnHover, _backOnHover, _soundOnHover = true;
    [SerializeField] private GameObject _backingImage;
    

    void Start(){
        transform.localScale = new Vector2(1f, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.UI_PlayerClickSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_soundOnHover) SoundManager.Instance.UI_PlayHoverSound();
        if(_scaleOnHover) transform.localScale = new Vector2(1.1f, 1.1f);
        if(_backOnHover && _backingImage != null) _backingImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_scaleOnHover) transform.localScale = new Vector2(1f, 1f);
        if(_backOnHover && _backingImage != null) _backingImage.SetActive(false);
    }

}
