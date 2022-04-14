using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Humanoid
{

    /// <summary>
    /// Skadar en av fiendes soldater
    /// </summary>
    /// <param name="playerIndex">Spelarens soldats plats på spelplanen räknat från vänster</param>
    /// <param name="enemyIndex">Fiendens soldats plats på spelplanen räknat från vänster</param>
    public void Attack(int playerIndex, int enemyIndex)
    {

        if(enemyIndex >= 0)
        {
            if (playerIndex >= 0)
            {
                GameObject playerObj = GameObject.Find("Player Board").transform.GetChild(playerIndex).gameObject;
                GameObject enemyObj = GameObject.Find("Enemy Board").transform.GetChild(enemyIndex).gameObject;

                try
                {
                    int attack = playerObj.GetComponent<Mercenary>().attack;
                    enemyObj.GetComponent<Mercenary>().health -= attack;

                    Text healthText = enemyObj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
                    healthText.text = enemyObj.GetComponent<Mercenary>().health + "";

                    if (enemyObj.GetComponent<Mercenary>().health <= 0)
                    {
                        Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
                        e.mercenaries.Remove(enemyObj);

                        Game g = GameObject.Find("Scripts").GetComponent<Game>();
                        g.ReloadMercenaries(0);

                        Destroy(enemyObj);
                    }
                }
                catch (MissingComponentException mce) { }
            }
            else 
            {
                if(this.attack > 0)
                {
                    GameObject playerObj = GameObject.Find("Player Hero").gameObject;
                    GameObject enemyObj = GameObject.Find("Enemy Board").transform.GetChild(enemyIndex).gameObject;

                    try
                    {
                        int attack = this.attack;
                        enemyObj.GetComponent<Mercenary>().health -= attack;

                        Text healthText = enemyObj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
                        healthText.text = enemyObj.GetComponent<Mercenary>().health + "";

                        if (enemyObj.GetComponent<Mercenary>().health <= 0)
                        {
                            Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
                            e.mercenaries.Remove(enemyObj);

                            Game g = GameObject.Find("Scripts").GetComponent<Game>();
                            g.ReloadMercenaries(0);

                            Destroy(enemyObj);
                        }
                    }
                    catch (MissingComponentException mce) { }
                }
            }
        }
        else
        {
            
            if (playerIndex >= 0)
            {
                GameObject playerObj = GameObject.Find("Player Board").transform.GetChild(playerIndex).gameObject;
                Enemy enemyObj = GameObject.Find("Scripts").GetComponent<Enemy>();

                try
                {
                    int attack = playerObj.GetComponent<Mercenary>().attack;
                    enemyObj.health -= attack;

                    Text healthText = GameObject.Find("Enemy Hero").transform.GetChild(1).GetComponent<Text>();
                    healthText.text = enemyObj.health + "";

                    if (enemyObj.health <= 0)
                    {
                        //Game over
                        print("You Win");
                        GameObject.Find("Scripts").GetComponent<Game>().gameIsFinished = true;
                    }
                }
                catch (MissingComponentException mce) { }
            }
            else
            {
                if(this.attack > 0)
                {
                    GameObject playerObj = GameObject.Find("Player Hero").gameObject;
                    Enemy enemyObj = GameObject.Find("Scripts").GetComponent<Enemy>();

                    try
                    {
                        int attack = this.attack;
                        enemyObj.health -= attack;

                        Text healthText = GameObject.Find("Enemy Hero").transform.GetChild(1).GetComponent<Text>();
                        healthText.text = enemyObj.health + "";

                        if (enemyObj.health <= 0)
                        {
                            //Game over
                            print("You Win");
                            GameObject.Find("Scripts").GetComponent<Game>().gameIsFinished = true;
                        }
                    }
                    catch (MissingComponentException mce) { }
                }
            }
            
        }
        
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
