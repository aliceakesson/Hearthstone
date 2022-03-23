using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    readonly string cardsURL = "Cards/";
    readonly string cardsImagesURL = "Images/Cards/";

    void Start()
    {
        ImportCard("Bloodfen_Raptor");
    }

    void Update()
    {
        
    }

    void ImportCard(string cardName)
    {

        Card card = Resources.Load<Card>(cardsURL + cardName);

        int hp = card.health;
        int attack = card.attack;
        int mana = card.mana;

        string name = card.name;
        string description = card.description;

        Sprite image = card.image; 
        CardType cardType = card.cardType;

        Sprite manaSprite = Resources.Load<Sprite>(cardsURL + "Mana");
        Sprite healthSprite = Resources.Load<Sprite>(cardsURL + "Health");
        Sprite attackSprite = Resources.Load<Sprite>(cardsURL + "Attack");

        Sprite frame = Resources.Load<Sprite>(cardsURL + "Frame-minion-neutral"); //ändra sen för att anpassa till typ av kort

        GameObject canvas = GameObject.Find("Canvas");
        GameObject cardObject = new GameObject();

        cardObject.transform.parent = canvas.transform; 


    }
}
