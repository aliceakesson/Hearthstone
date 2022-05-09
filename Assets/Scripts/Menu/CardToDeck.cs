using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardToDeck : MonoBehaviour, IPointerClickHandler
{

    Vector2 startPos = new Vector2(3.24f, 156.98f);
    float yMargin = 32.64f;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(transform.parent.name == "Card Options")
        {
            GameObject example = GameObject.Find("Card Deck Example");

            GameObject cardObject = GameObject.Instantiate(example);
            cardObject.transform.parent = GameObject.Find("Chosen Deck").transform.GetChild(1);
            cardObject.name = this.name;

            int cardsChosen = GameObject.Find("Scripts").GetComponent<SelectDeck>().cardsChosen;

            cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x, startPos.y - yMargin * cardsChosen);

            Card card = Resources.Load<Card>("Cards/" + this.name);

            cardObject.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = card.mana + "";
            cardObject.transform.GetChild(3).GetComponent<Text>().text = card.name + "";

            GameObject.Find("Card Count").transform.GetChild(1).GetComponent<Text>().text = (cardsChosen + 1) + "/30";
            GameObject.Find("Scripts").GetComponent<SelectDeck>().cardsChosen++;

            GameObject.Find("Scripts").GetComponent<SelectDeck>().chosenCards.Add(this.gameObject); 

        }
        else if(transform.parent.name == "Chosen Deck")
        {
            GameObject.Find("Scripts").GetComponent<SelectDeck>().cardsChosen--;
            GameObject.Find("Card Count").transform.GetChild(1).GetComponent<Text>().text = (GameObject.Find("Scripts").GetComponent<SelectDeck>().cardsChosen - 1) + "/30";

            GameObject.Find("Scripts").GetComponent<SelectDeck>().chosenCards.Remove(this.gameObject);
            ReloadChosenDeck();

            Destroy(this);
        }

    }

    void ReloadChosenDeck()
    {
        int k = 0; 
        foreach(GameObject card in GameObject.Find("Scripts").GetComponent<SelectDeck>().chosenCards)
        {
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x, startPos.y - yMargin * k);
            k++;
        }
    }
}
