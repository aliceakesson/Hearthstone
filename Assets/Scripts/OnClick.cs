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

    Vector2 onBeginDragStartPos;

    public void OnBeginDrag(PointerEventData eventData)
    {

        print("On Begin Drag");

        if(tag == "Mercenary")
        {

            GameObject arrow = GameObject.Find("Arrow");

            GameObject arrowClone = Instantiate(arrow);
            GameObject triangle = arrowClone.transform.GetChild(0).gameObject;
            GameObject boxes = arrowClone.transform.GetChild(1).gameObject;

            onBeginDragStartPos = this.gameObject.GetComponent<RectTransform>().position;

            arrowClone.transform.parent = GameObject.Find("Board").transform;
            arrowClone.layer = LayerMask.NameToLayer("UI");

            RectTransform rt = arrowClone.GetComponent<RectTransform>();
            rt.position = new Vector2(onBeginDragStartPos.x, onBeginDragStartPos.y + rt.rect.height/2);

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if(tag == "Card")
        {
            Game g = GameObject.Find("Scripts").GetComponent<Game>();
            if (placeable && g.playerTurn)
            {
                GetComponent<RectTransform>().anchoredPosition += eventData.delta;
            }
        }
        else if(tag == "Mercenary")
        {

            GameObject arrowClone = GameObject.Find("Arrow(Clone)");
            GameObject triangle = arrowClone.transform.GetChild(0).gameObject;
            GameObject boxes = arrowClone.transform.GetChild(1).gameObject;

            arrowClone.GetComponent<RectTransform>().rotation = Quaternion.identity;

            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldStartPos = this.gameObject.GetComponent<RectTransform>().position;
            float arrowHeight = Mathf.Sqrt(Mathf.Pow(mousePos.x - worldStartPos.x, 2) + Mathf.Pow(mousePos.y - worldStartPos.y, 2));

            float margin = 20;
            float boxHeight = margin;
            if (arrowHeight > margin)
            {
                boxHeight = arrowHeight;
            }

            RectTransform rt = boxes.GetComponent<RectTransform>();
            float prevHeight = rt.rect.height;
            rt.sizeDelta = new Vector2(rt.rect.width, boxHeight);

            foreach (Transform box in boxes.transform)
            {
                rt = box.gameObject.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + (boxHeight - prevHeight));
            }

            rt = arrowClone.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.rect.width, boxHeight);
            rt.position = new Vector2(onBeginDragStartPos.x, onBeginDragStartPos.y + (rt.rect.height) / 2);


            //float angle = 0;
            //if (mousePos.x > worldStartPos.x) //till höger om mercenary
            //{
            //    if (mousePos.y > worldStartPos.y) //över mercenary
            //    {
            //        angle = Mathf.Atan2((mousePos.y - onBeginDragStartPos.y), (mousePos.x - onBeginDragStartPos.x)) * Mathf.Rad2Deg;
            //        angle = 90 - angle;
            //    }
            //    else if (mousePos.y < worldStartPos.y)
            //    {
            //        angle = Mathf.Atan2((onBeginDragStartPos.y - mousePos.y), (mousePos.x - onBeginDragStartPos.x)) * Mathf.Rad2Deg;
            //        angle += 90;
            //    }
            //}
            //else if (mousePos.x < worldStartPos.x)
            //{
            //    if (mousePos.y > worldStartPos.y)
            //    {
            //        angle = Mathf.Atan2((mousePos.y - onBeginDragStartPos.y), (onBeginDragStartPos.x - mousePos.x)) * Mathf.Rad2Deg;
            //        angle += 270;
            //    }
            //    else if (mousePos.y < worldStartPos.y)
            //    {
            //        angle = Mathf.Atan2((onBeginDragStartPos.x - mousePos.x), (onBeginDragStartPos.y - mousePos.x)) * Mathf.Rad2Deg;
            //        angle += 180;
            //    }
            //}

            //rt.Rotate(new Vector3(0, 0, -angle), Space.Self);

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

                Game g = GameObject.Find("Scripts").GetComponent<Game>();
                g.ImportMercenary(this.gameObject.name, 1);
                
                Destroy(this.gameObject);

                Player p = GameObject.Find("Scripts").GetComponent<Player>();

                int index = 0;
                if (transform.parent.childCount > 1)
                    index = transform.GetSiblingIndex();

                p.cardObjects.RemoveAt(index);
                g.ReloadCards(1);

            }
            else
            {
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, deckY);
            }
        }
        //else if(tag == "Mercenary")
        //{
        //    Destroy(GameObject.Find("Arrow(Clone)"));
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Game g = GameObject.Find("Scripts").GetComponent<Game>();
        if (this.gameObject.tag == "Card" && transform.parent.name == "Player Deck" && g.playerTurn)
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

        Game g = GameObject.Find("Scripts").GetComponent<Game>();
        if (this.gameObject.tag == "Card" && transform.parent.name == "Player Deck" && g.playerTurn)
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

    /// <summary>
    /// Ändrar storlek på ett kort 
    /// </summary>
    /// <param name="objectName">Namnet på kortet vars storlek ska implementeras på detta objekt</param>
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
