using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClick : Game, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("On Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("On Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("On End Drag");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("On Pointer Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        print("On Pointer Enter");

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        print("On Pointer Exit");

    }
}
