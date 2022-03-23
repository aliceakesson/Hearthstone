using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType {
    Spell, 
    Weapon, 
    Minion
}

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public int health;
    public int attack;

    [Range(0, 10)]
    public int mana;

    public string name;
    public string description;

    public CardType cardType;
    public Sprite image;

}
