using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{

    public List<GameObject> cardObjects = new List<GameObject>();
    public List<GameObject> mercenaries = new List<GameObject>();

    public int health;
    public int attack;
    public int armor;
    public int heroPowerMana; 


    /// <summary>
    /// Metod som drar ett nytt kort till handen
    /// </summary>
    public virtual void DrawCard()
    {

    //    Game g = GameObject.Find("Scripts").GetComponent<Game>();
    //    Card[] cards = Resources.LoadAll<Card>("Cards/");
    //    int index = Random.Range(0, cards.Length - 1);

    //    string name = cards[index].name;
    //    name = name.Replace(" ", "_");

    //    g.ImportCard(name, 1);

    }
}
