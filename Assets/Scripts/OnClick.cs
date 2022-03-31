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

        if (placeable)
        {
            GetComponent<RectTransform>().anchoredPosition += eventData.delta;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("On End Drag");

        if (this.gameObject.tag == "Card")
        {
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;

            if (pos.x >= boardX1 && pos.x <= boardX2 && pos.y <= boardY1 && pos.y >= boardY2)
            {
                placeable = false;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, boardY);

                Destroy(this.gameObject);

                //ChangeCardSize("Card Normal");
                //this.gameObject.transform.parent = GameObject.Find("Player Board").transform;
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

        if (this.gameObject.tag == "Card" && transform.parent.name == "Player Deck")
        {

            ChangeCardSize("Card Big");

            List<GameObject> cards = GameObject.Find("Scripts").GetComponent<Player>().cardObjects;
            int listIndex = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] == this.gameObject)
                {
                    listIndex = i;
                    break;
                }
            }

            float margin = 50;
            if (listIndex > 0)
            {
                for (int i = 0; i < listIndex; i++)
                {
                    Vector2 delta = new Vector2(-margin, 0);
                    cards[i].GetComponent<RectTransform>().anchoredPosition += delta;
                }
            }
            if (listIndex < (cards.Count - 1))
            {
                for (int i = cards.Count - 1; i > listIndex; i--)
                {
                    Vector2 delta = new Vector2(margin, 0);
                    cards[i].GetComponent<RectTransform>().anchoredPosition += delta;
                }
            }

        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (this.gameObject.tag == "Card" && transform.parent.name == "Player Deck")
        {

            ChangeCardSize("Card Normal");

            List<GameObject> cards = GameObject.Find("Scripts").GetComponent<Player>().cardObjects;
            int listIndex = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] == this.gameObject)
                {
                    listIndex = i;
                    break;
                }
            }

            float margin = 50;
            if (listIndex > 0)
            {
                for (int i = 0; i < listIndex; i++)
                {
                    Vector2 delta = new Vector2(margin, 0);
                    cards[i].GetComponent<RectTransform>().anchoredPosition += delta;
                }
            }
            if (listIndex < (cards.Count - 1))
            {
                for (int i = cards.Count - 1; i > listIndex; i--)
                {
                    Vector2 delta = new Vector2(-margin, 0);
                    cards[i].GetComponent<RectTransform>().anchoredPosition += delta;
                }
            }

        }

    }

    void ChangeCardSize(string objectName)
    {

        GameObject bigSizeCard = GameObject.Find(objectName);

        GameObject image1 = transform.GetChild(0).gameObject;
        GameObject image1Example = bigSizeCard.transform.GetChild(0).gameObject;
        Rect r = image1Example.GetComponent<RectTransform>().rect;

        image1.GetComponent<RectTransform>().sizeDelta = new Vector2(r.width, r.height);

        GameObject image2 = image1.transform.GetChild(0).gameObject;
        GameObject image2Example = image1Example.transform.GetChild(0).gameObject;
        RectTransform rt = image2Example.GetComponent<RectTransform>();

        image2.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        image2.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);

        GameObject frame = image1.transform.GetChild(1).gameObject;
        GameObject frameExample = image1Example.transform.GetChild(1).gameObject;
        r = frameExample.GetComponent<RectTransform>().rect;

        frame.GetComponent<RectTransform>().sizeDelta = new Vector2(r.width, r.height);

        GameObject name = frame.transform.GetChild(0).gameObject;
        GameObject nameExample = frameExample.transform.GetChild(0).gameObject;
        rt = nameExample.GetComponent<RectTransform>();

        name.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        name.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        name.GetComponent<Text>().fontSize = nameExample.GetComponent<Text>().fontSize;

        GameObject description = frame.transform.GetChild(1).gameObject;
        GameObject descriptionExample = frameExample.transform.GetChild(1).gameObject;
        rt = descriptionExample.GetComponent<RectTransform>();

        description.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        description.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        description.GetComponent<Text>().fontSize = descriptionExample.GetComponent<Text>().fontSize;

        GameObject mana = frame.transform.GetChild(2).gameObject;
        GameObject manaExample = frameExample.transform.GetChild(2).gameObject;
        rt = manaExample.GetComponent<RectTransform>();

        mana.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        mana.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);

        GameObject manaText = mana.transform.GetChild(0).gameObject;
        GameObject manaTextExample = manaExample.transform.GetChild(0).gameObject;
        rt = manaTextExample.GetComponent<RectTransform>();

        manaText.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        manaText.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        manaText.GetComponent<Text>().fontSize = manaTextExample.GetComponent<Text>().fontSize;

        GameObject attack = frame.transform.GetChild(3).gameObject;
        GameObject attackExample = frameExample.transform.GetChild(3).gameObject;
        rt = attackExample.GetComponent<RectTransform>();

        attack.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        attack.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);

        GameObject attackText = attack.transform.GetChild(0).gameObject;
        GameObject attackTextExample = attackExample.transform.GetChild(0).gameObject;
        rt = attackTextExample.GetComponent<RectTransform>();

        attackText.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        attackText.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        attackText.GetComponent<Text>().fontSize = attackTextExample.GetComponent<Text>().fontSize;

        GameObject health = frame.transform.GetChild(4).gameObject;
        GameObject healthExample = frameExample.transform.GetChild(4).gameObject;
        rt = healthExample.GetComponent<RectTransform>();

        health.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        health.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);

        GameObject healthText = health.transform.GetChild(0).gameObject;
        GameObject healthTextExample = healthExample.transform.GetChild(0).gameObject;
        rt = healthTextExample.GetComponent<RectTransform>();

        healthText.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        healthText.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        healthText.GetComponent<Text>().fontSize = healthTextExample.GetComponent<Text>().fontSize;

    }


}
