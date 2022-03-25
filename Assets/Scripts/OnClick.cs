using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    float boardX1 = -296, boardX2 = 296;
    float boardY1 = 257, boardY2 = 166;

    float boardY = 203;
    float deckY = 0;

    bool placeable = true; 

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {

        if(placeable)
        {
            GetComponent<RectTransform>().anchoredPosition += eventData.delta;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("On End Drag");

        if(this.gameObject.tag == "Card")
        {
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;

            if(pos.x >= boardX1 && pos.x <= boardX2 && pos.y <= boardY1 && pos.y >= boardY2)
            {
                placeable = false; 
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, boardY);
            }
            else
            {
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, deckY);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {


    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
