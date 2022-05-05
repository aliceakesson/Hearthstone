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
    public override void Attack(int playerIndex, int enemyIndex)
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

                playerObj.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false; 
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

                        //??
                        this.health -= this.attack;
                        playerObj.transform.GetChild(1).GetComponent<Text>().text = this.health + "";

                        GameObject weapon = GameObject.Find("Player Weapon");
                        int durability = int.Parse(weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text);
                        durability--; 
                        if(durability <= 0)
                        {

                            weapon.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(1).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(2).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(3).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;

                            this.attack = 0; 

                        }
                        else
                        {
                            weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = durability + "";
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

                playerObj.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
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
                        
                        //??
                        this.health -= this.attack;
                        playerObj.transform.GetChild(1).GetComponent<Text>().text = this.health + "";

                        GameObject weapon = GameObject.Find("Player Weapon");
                        int durability = 0;
                        try
                        {
                            durability = int.Parse(weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text);
                        } catch(MissingReferenceException mre) { return; }
                        durability--;
                        if (durability <= 0)
                        {

                            weapon.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(1).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(2).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(3).GetComponent<Image>().enabled = false;
                            weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;

                            this.attack = 0;

                        }
                        else
                        {
                            weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = durability + "";
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

    public override void UseHeroPower(string heroPowerName)
    {

        print(heroPowerName);
        GameObject heroPowerObject = GameObject.Find("Player HeroPower"); 

        Game g = GameObject.Find("Scripts").GetComponent<Game>();
        int manaLeft = g.maxMana - (GameObject.Find("Mana Text").GetComponent<Text>().text[0] - 48); //ascii
        int mana = 2;

        bool changeMade = false; 

        if (heroPowerObject.GetComponent<OnClickEvents>().clickable && manaLeft >= mana)
        {
            switch (heroPowerName)
            {
                case "Armor_Up!": // Använder switch/case för flexibilitet senare, behövs dock ej
                    try
                    {
                        GameObject armor = GameObject.Find("Player Hero").transform.GetChild(3).gameObject;
                        int currentArmor = int.Parse(armor.transform.GetChild(0).GetComponent<Text>().text);
                        if (!armor.transform.GetChild(0).GetComponent<Text>().enabled)
                        {
                            currentArmor = 0;
                            armor.GetComponent<Image>().enabled = true;
                            armor.transform.GetChild(0).GetComponent<Text>().enabled = true;
                        }
                        currentArmor+=2;
                        armor.transform.GetChild(0).GetComponent<Text>().text = currentArmor + "";

                        changeMade = true; 
                    }
                    catch (System.NullReferenceException nre) { }
                    break;
                default:
                    break;
            }
        }

        if(changeMade)
        {
            manaLeft -= mana;
            int manaUsed = g.maxMana - manaLeft;
            foreach (Transform manaObj in GameObject.Find("Mana Bar").transform)
            {
                if (manaObj.GetSiblingIndex() < manaUsed)
                {
                    manaObj.GetComponent<Image>().color = g.manaLight;
                }
            }
            GameObject.Find("Mana Text").GetComponent<Text>().text = manaUsed + "/" + g.maxMana;

            heroPowerObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            g.ReloadBorders();
        } 

    }

    public override void DealDamage(int enemyIndex, int damage)
    {
        GameObject enemy = GameObject.Find("Enemy Board").transform.GetChild(enemyIndex).gameObject;
        enemy.GetComponent<Mercenary>().health -= 2; 
        
        if(enemy.GetComponent<Mercenary>().health <= 0)
        {
            Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
            e.mercenaries.Remove(enemy);
            Game g = GameObject.Find("Scripts").GetComponent<Game>();
            g.ReloadMercenaries(0);
            Destroy(enemy);
        }
        else
        {
            enemy.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = enemy.GetComponent<Mercenary>().health + ""; 
        }
    }

}
