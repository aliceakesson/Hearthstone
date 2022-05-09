using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardToDeck : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject example = GameObject.Find("Card Deck Example");
        Vector2 startPos = new Vector2(3.24f, 156.98f);
        float yMargin = 32.64f; 

        GameObject cardObject = GameObject.Instantiate(example);
        cardObject.transform.parent = GameObject.Find("Chosen Deck").transform.GetChild(1);
        cardObject.name = this.name;

        int cardsChosen = GameObject.Find("Scripts").GetComponent<SelectDeck>().cardsChosen; 

        cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x, startPos.y - yMargin * cardsChosen);

        Card card = Resources.Load<Card>("Cards/" + this.name);

        cardObject.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = card.mana + "";
        cardObject.transform.GetChild(3).GetComponent<Text>().text = card.name + "";

        GameObject.Find("Card Count").transform.GetChild(1).GetComponent<Text>().text = (cardsChosen+1) + "/30";
        GameObject.Find("Scripts").GetComponent<SelectDeck>().cardsChosen++;

    }
}
