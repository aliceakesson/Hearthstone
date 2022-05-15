using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectDeck : MenuButtonEvents
{

    int cardsInMenu = 0; 
    public int cardsChosen = 0;

    Vector2 startPos = new Vector2(-235.3f, 109.9f);

    public List<GameObject> chosenCards;

    public SelectDeck()
    {

    }
    private void Start()
    {
        ImportCard("Bloodfen_Raptor");
        ImportCard("River_Crocolisk");
        ImportCard("Cleave");
        ImportCard("Execute");
        ImportCard("Fiery_War_Axe");
        ImportCard("Silver_Hand_Recruit");
    }

    void ImportCard(string cardName)
    {

        GameObject example = GameObject.Find("Card Example");

        Card card = Resources.Load<Card>("Cards/" + cardName);

        GameObject cardObject = GameObject.Instantiate(example);
        cardObject.transform.parent = GameObject.Find("Card Options").transform;
        cardObject.name = cardName; 

        float xMargin = 152.9f, yMargin = 193.82f;

        int index = cardsInMenu;
        if(cardsInMenu >= 8)
        {
            index = cardsInMenu % 8; 
        }

        int xIndex = index;
        int yIndex = 0; 

        if(index >= 4)
        {
            xIndex -= 4;
            yIndex = 1;
        }

        cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x + xMargin * xIndex, startPos.y - yMargin * yIndex);

        cardObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = card.image;

        cardObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = card.name;
        cardObject.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = card.description; 
        cardObject.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = card.mana + ""; 
        cardObject.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().text = card.attack + ""; 
        cardObject.transform.GetChild(0).GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>().text = card.health + "";

        if(card.cardType == CardType.Weapon)
        {
            cardObject.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Cards/Armor");

            cardObject.transform.GetChild(0).GetChild(1).GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Cards/Durability");
            cardObject.transform.GetChild(0).GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>().text = card.durability + "";
        }
        else if(card.cardType == CardType.Spell)
        {
            cardObject.transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Image>().enabled = false;
            cardObject.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;

            cardObject.transform.GetChild(0).GetChild(1).GetChild(4).GetComponent<Image>().enabled = false;
            cardObject.transform.GetChild(0).GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>().enabled = false;
        }

        cardObject.AddComponent<CardToDeck>();

        cardsInMenu++;

    }

    public override void PlayGame()
    {
        SwitchScene(2);
    }

}
