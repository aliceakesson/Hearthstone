using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Humanoid
{

    /// <summary>
    /// Skadar en av spelarens soldater
    /// </summary>
    /// <param name="enemyIndex">Fiendens soldats plats på spelplanen räknat från vänster</param>
    /// <param name="playerIndex">Spelarens soldats plats på spelplanen räknat från vänster</param>
    public void Attack(int enemyIndex, int playerIndex)
    {
        print("Attack: " + playerIndex);

        GameObject enemyObj = GameObject.Find("Enemy Board").transform.GetChild(enemyIndex).gameObject;
        GameObject playerObj = GameObject.Find("Player Board").transform.GetChild(playerIndex).gameObject;

        try
        {
            int attack = enemyObj.GetComponent<Mercenary>().attack;
            playerObj.GetComponent<Mercenary>().health -= attack;

            Text healthText = playerObj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
            healthText.text = playerObj.GetComponent<Mercenary>().health + "";

            if (playerObj.GetComponent<Mercenary>().health <= 0)
            {
                Player p = GameObject.Find("Scripts").GetComponent<Player>();
                p.mercenaries.Remove(playerObj);

                Game g = GameObject.Find("Scripts").GetComponent<Game>();
                g.ReloadMercenaries(0);
                g.ReloadMercenaries(1);
                
                Destroy(playerObj);

            }
        }
        catch (MissingComponentException mce) { }

    }

    public override void DrawCard()
    {
        Game g = GameObject.Find("Scripts").GetComponent<Game>();
        Card[] cards = Resources.LoadAll<Card>("Cards/");
        int index = Random.Range(0, cards.Length - 1);

        string name = cards[index].name;
        name = name.Replace(" ", "_");

        g.ImportCard(name, 0);

    }
}
