using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CardType {
    Spell, 
    Weapon, 
    Minion
}

[CreateAssetMenu]
public class Card : ScriptableObject
{
    
    Sprite frame;
    Sprite manaSprite, healthSprite, attackSprite;

    string cardsURL = "Images/Cards/";
    

    void Start()
    {

        manaSprite = Resources.Load<Sprite>(cardsURL + "Mana");
        healthSprite = Resources.Load<Sprite>(cardsURL + "Health");
        attackSprite = Resources.Load<Sprite>(cardsURL + "Attack");

        frame = Resources.Load<Sprite>(cardsURL + "Frame-minion-neutral"); //ändra sen för att anpassa till typ av kort

    }

    void Update()
    {

    }

}
