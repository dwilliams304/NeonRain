using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData){
        
    }
    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData){

    }
}
