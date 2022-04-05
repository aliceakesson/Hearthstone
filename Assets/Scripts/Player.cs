using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Humanoid
{
    
    void Start()
    {
        
    }

    
    void Update()
    {

    }

    public void Attack(int index)
    {
        print("Attack: " + index);

        GameObject obj = GameObject.Find("Enemy Board").transform.GetChild(index).gameObject;
        
    }

    public override void DrawCard()
    {

        Game g = GameObject.Find("Scripts").GetComponent<Game>();
        Card[] cards = Resources.LoadAll<Card>("Cards/");
        int index = Random.Range(0, cards.Length-1);

        string name = cards[index].name;
        name = name.Replace(" ", "_");

        g.ImportCard(name, 1);

    }
}
