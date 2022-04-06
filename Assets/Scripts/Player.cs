using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Humanoid
{
    
    void Start()
    {
        
    }

    
    void Update()
    {

    }

    public void Attack(int playerIndex, int enemyIndex)
    {
        print("Attack: " + enemyIndex);

        GameObject playerObj = GameObject.Find("Player Board").transform.GetChild(playerIndex).gameObject;
        GameObject enemyObj = GameObject.Find("Enemy Board").transform.GetChild(enemyIndex).gameObject;

        try
        {
            int attack = playerObj.GetComponent<Mercenary>().attack; 
            enemyObj.GetComponent<Mercenary>().health -= attack;

            Text healthText = enemyObj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
            healthText.text = enemyObj.GetComponent<Mercenary>().health + ""; 

            if(enemyObj.GetComponent<Mercenary>().health <= 0)
            {
                Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
                e.mercenaries.Remove(enemyObj);

                Game g = GameObject.Find("Scripts").GetComponent<Game>();
                g.ReloadMercenaries(0);

                Destroy(enemyObj);
            }
        } catch(MissingComponentException mce) { }
        
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
