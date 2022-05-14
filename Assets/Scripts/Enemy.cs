using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Humanoid
{

    public Enemy()
    {

    }

    /// <summary>
    /// Skadar en av spelarens soldater
    /// </summary>
    /// <param name="enemyIndex">Fiendens soldats plats på spelplanen räknat från vänster</param>
    /// <param name="playerIndex">Spelarens soldats plats på spelplanen räknat från vänster</param>
    public override void Attack(int enemyIndex, int playerIndex)
    {

        if (playerIndex >= 0)
        {
            if (enemyIndex >= 0)
            {
                GameObject playerObj = GameObject.Find("Player Board").transform.GetChild(playerIndex).gameObject;
                GameObject enemyObj = GameObject.Find("Enemy Board").transform.GetChild(enemyIndex).gameObject;

                try
                {
                    int attack = int.Parse(enemyObj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);

                    int playerHp = int.Parse(playerObj.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                    playerHp -= attack;
                    playerObj.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = playerHp + "";

                    if (playerHp <= 0)
                    {
                        Player p = GameObject.Find("Scripts").GetComponent<Player>();
                        p.mercenaries.Remove(playerObj);

                        Game g = GameObject.Find("Scripts").GetComponent<Game>();
                        g.ReloadMercenaries(1);

                        Destroy(playerObj);
                    }
                }
                catch (MissingComponentException mce) { }
            }
            else
            {
                if(this.attack > 0)
                {
                    GameObject playerObj = GameObject.Find("Player Board").transform.GetChild(playerIndex).gameObject;
                    GameObject enemyObj = GameObject.Find("Enemy Hero").gameObject;

                    try
                    {
                        int attack = this.attack;

                        int playerHp = int.Parse(playerObj.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                        playerHp -= attack;
                        playerObj.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = playerHp + "";

                        if (playerHp <= 0)
                        {
                            Player p = GameObject.Find("Scripts").GetComponent<Player>();
                            p.mercenaries.Remove(playerObj);

                            Game g = GameObject.Find("Scripts").GetComponent<Game>();
                            g.ReloadMercenaries(1);

                            Destroy(playerObj);
                        }

                        if(GameObject.Find("Enemy Weapon").transform.GetChild(0).GetComponent<Image>().enabled &&
                            GameObject.Find("Enemy Weapon").transform.GetChild(0).GetComponent<Image>().sprite.name == "Truesilver_Champion")
                        {
                            int ownHp = int.Parse(enemyObj.transform.GetChild(1).GetComponent<Text>().text);
                            ownHp += 2;
                            enemyObj.transform.GetChild(1).GetComponent<Text>().text = ownHp + "";
                        }

                    }
                    catch (MissingComponentException mce) { }

                }
            }
        }
        else // player index < 0
        {
            bool hasArmor = false;
            if (GameObject.Find("Player Hero").transform.GetChild(2).GetComponent<Image>().enabled)
                hasArmor = true; 

            if (enemyIndex >= 0)
            {
                GameObject enemyObj = GameObject.Find("Enemy Board").transform.GetChild(enemyIndex).gameObject;
                Player playerObj = GameObject.Find("Scripts").GetComponent<Player>();

                try
                {
                    int attack = int.Parse(enemyObj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);
                    GameObject armorObj = GameObject.Find("Player Hero").transform.GetChild(2).gameObject; 

                    if (hasArmor) {
                        try
                        {
                            int armor = int.Parse(armorObj.transform.GetChild(0).GetComponent<Text>().text);
                            if(attack < armor)
                            {
                                int i = armor - attack;
                                armorObj.transform.GetChild(0).GetComponent<Text>().text = i + "";
                            }
                            else
                            {
                                if (attack > armor)
                                {
                                    int rest = attack - armor;
                                    health -= rest;

                                    Text healthText = GameObject.Find("Player Hero").transform.GetChild(1).GetComponent<Text>();
                                    healthText.text = playerObj.health + "";
                                }

                                armorObj.transform.GetChild(0).GetComponent<Text>().text = "0";
                                armorObj.GetComponent<Image>().enabled = false;
                                armorObj.transform.GetChild(0).GetComponent<Text>().enabled = false;

                            }
                        } catch(System.FormatException fe) { }
                    }
                    else
                    {
                        playerObj.health -= attack;

                        Text healthText = GameObject.Find("Player Hero").transform.GetChild(1).GetComponent<Text>();
                        healthText.text = playerObj.health + "";
                    }

                    if (playerObj.health <= 0)
                    {
                        //Game over
                        print("Game Over");
                        GameObject.Find("Scripts").GetComponent<Game>().gameIsFinished = true;
                    }
                }
                catch (MissingComponentException mce) { }
            }
            else
            {
                if(this.attack > 0)
                {
                    GameObject enemyObj = GameObject.Find("Enemy Hero").gameObject;
                    Player playerObj = GameObject.Find("Scripts").GetComponent<Player>();

                    try
                    {
                        int attack = this.attack;
                        playerObj.health -= attack;

                        Text healthText = GameObject.Find("Player Hero").transform.GetChild(1).GetComponent<Text>();
                        healthText.text = playerObj.health + "";

                        if (playerObj.health <= 0)
                        {
                            //Game over
                            print("Game Over");
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
        int index = Random.Range(0, cards.Length - 1);

        string name = cards[index].name;
        name = name.Replace(" ", "_");

        g.ImportCard(name, 0);

    }

    public override void UseHeroPower(string heroPowerName)
    {

        switch (heroPowerName)
        {
            case "Reinforce": // Använder switch/case för flexibilitet senare, behövs dock ej 
                try
                {
                    Game g = GameObject.Find("Scripts").GetComponent<Game>();
                    g.ImportMercenary("Silver_Hand_Recruit", 0);
                    g.ReloadMercenaries(0);
                }
                catch (System.NullReferenceException nre) { }
                break;
            default:
                break;
        }


    }

    public override void DealDamage(int playerIndex, int damage)
    {
    }

    public void UseCard(int index)
    {

        print("Use Card");

        Game g = GameObject.Find("Scripts").GetComponent<Game>();
        Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();

        GameObject cardObject = GameObject.Find("Enemy Deck").transform.GetChild(index).gameObject;
        string cardName = e.cardObjects[index].name;
        Card card = Resources.Load<Card>("Cards/" + cardName);

        bool changeMade = true;

        if(card.cardType == CardType.Minion) //import mercenary
        {
            int health = card.health;
            int attack = card.attack;
            Sprite image = card.image;

            Sprite healthSprite = Resources.Load<Sprite>(g.cardsImagesURL + "Health");
            Sprite attackSprite = Resources.Load<Sprite>(g.cardsImagesURL + "Attack");

            Sprite frame;
            string imageName = "Mercenary-Minion";

            string frameName = g.cardsFramesURL + imageName;
            if (card.description.Length >= 5)
            {
                if (card.description.Contains("Taunt"))
                {
                    frameName = frameName + "-Taunt";
                }
            }

            GameObject mercObject = new GameObject(cardName, typeof(RectTransform));
            GameObject example = GameObject.Find("Mercenary Example");

            mercObject.transform.parent = GameObject.Find("Enemy Board").transform;
            mercObject.layer = LayerMask.NameToLayer("UI");

            RectTransform rt = example.GetComponent<RectTransform>();
            mercObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            mercObject.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            mercObject.tag = "Mercenary";

            mercObject.AddComponent<OnClickEvents>();
            mercObject.AddComponent<CanvasGroup>();

            mercObject.AddComponent<Mercenary>();
            mercObject.GetComponent<Mercenary>().health = health;
            mercObject.GetComponent<Mercenary>().attack = attack;

            #region Skapande av mercenary UI
            GameObject frame1 = new GameObject("Frame", typeof(RectTransform));
            rt = example.transform.GetChild(1).GetComponent<RectTransform>();
            frame1.transform.parent = mercObject.transform;
            frame1.layer = LayerMask.NameToLayer("UI");
            frame1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            frame1.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            frame1.AddComponent<Image>();
            frame1.GetComponent<Image>().sprite = Resources.Load<Sprite>(frameName + "-mask");
            frame1.AddComponent<Mask>();
            frame1.GetComponent<Mask>().showMaskGraphic = false;

            GameObject imageObj = new GameObject("Image", typeof(RectTransform));
            rt = example.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
            imageObj.transform.parent = frame1.transform;
            imageObj.layer = LayerMask.NameToLayer("UI");
            imageObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            imageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            imageObj.AddComponent<Image>();
            imageObj.GetComponent<Image>().sprite = image;

            GameObject frame2 = new GameObject("Frame Visible", typeof(RectTransform));
            rt = example.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>();
            frame2.transform.parent = frame1.transform;
            frame2.layer = LayerMask.NameToLayer("UI");
            frame2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            frame2.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            frame2.AddComponent<Image>();
            frame2.GetComponent<Image>().sprite = Resources.Load<Sprite>(frameName);

            GameObject attackObj = new GameObject("Attack", typeof(RectTransform));
            rt = example.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<RectTransform>();
            attackObj.transform.parent = frame2.transform;
            attackObj.layer = LayerMask.NameToLayer("UI");
            attackObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
            attackObj.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            attackObj.AddComponent<Image>();
            attackObj.GetComponent<Image>().sprite = attackSprite;
            attackObj.GetComponent<Image>().maskable = false;

            GameObject attackText = new GameObject("Text", typeof(RectTransform));
            rt = example.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            attackText.transform.parent = attackObj.transform;
            attackText.layer = LayerMask.NameToLayer("UI");
            attackText.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
            attackText.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            attackText.AddComponent<Text>();
            attackText.GetComponent<Text>().text = attack + "";
            attackText.GetComponent<Text>().font = Resources.Load<Font>(g.belweFontsURL + g.usedFont);
            attackText.GetComponent<Text>().fontSize = 10;
            attackText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            attackText.GetComponent<Text>().color = Color.white;
            attackText.GetComponent<Text>().maskable = false;

            GameObject healthObj = new GameObject("health", typeof(RectTransform));
            rt = example.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<RectTransform>();
            healthObj.transform.parent = frame2.transform;
            healthObj.layer = LayerMask.NameToLayer("UI");
            healthObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
            healthObj.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            healthObj.AddComponent<Image>();
            healthObj.GetComponent<Image>().sprite = healthSprite;
            healthObj.GetComponent<Image>().maskable = false;

            GameObject healthText = new GameObject("Text", typeof(RectTransform));
            rt = example.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<RectTransform>();
            healthText.transform.parent = healthObj.transform;
            healthText.layer = LayerMask.NameToLayer("UI");
            healthText.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
            healthText.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            healthText.AddComponent<Text>();
            healthText.GetComponent<Text>().text = health + "";
            healthText.GetComponent<Text>().font = Resources.Load<Font>(g.belweFontsURL + g.usedFont);
            healthText.GetComponent<Text>().fontSize = 10;
            healthText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            healthText.GetComponent<Text>().color = Color.white;
            healthText.GetComponent<Text>().maskable = false;
            #endregion

            e.mercenaries.Add(mercObject);

            float margin = 10;
            float width = mercObject.GetComponent<RectTransform>().rect.width;
            float lengthOfLine = 0;

            foreach (GameObject obj in e.mercenaries)
            {
                lengthOfLine += width + margin;
            }
            lengthOfLine -= margin;

            float startPosX = -lengthOfLine / 2;
            for (int i = 0; i < e.mercenaries.Count; i++)
            {
                float x = startPosX + i * (width + margin);
                e.mercenaries[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + width / 2, 0);
            }

            switch (cardName) //battlecry events
            {
                case "Guardian_of_Kings":
                    try
                    {
                        int hp = int.Parse(GameObject.Find("Enemy Hero").transform.GetChild(1).GetComponent<Text>().text);

                        hp += 6;
                        if (hp > 30)
                            hp = 30;
                        GameObject.Find("Enemy Hero").transform.GetChild(1).GetComponent<Text>().text = hp + "";
                    } catch(System.FormatException fe) { }
                    break;
                case "Acidic_Swamp_Ooze":
                    if(GameObject.Find("Player Weapon").transform.GetChild(0).GetComponent<Image>().enabled)
                    {
                        GameObject weapon = GameObject.Find("Player Weapon");
                        weapon.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
                        weapon.transform.GetChild(0).GetChild(1).GetComponent<Image>().enabled = false;
                        weapon.transform.GetChild(0).GetChild(2).GetComponent<Image>().enabled = false;
                        weapon.transform.GetChild(0).GetChild(3).GetComponent<Image>().enabled = false;

                        weapon.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
                        weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;
                    }
                    break;
                case "Murloc_Tidehunter":
                    if (GameObject.Find("Enemy Board").transform.childCount < 6)
                        g.ImportMercenary("Murloc_Scout", 0);
                    else
                        changeMade = false;
                    break;
                case "Razorfen_Hunter":
                    if (GameObject.Find("Enemy Board").transform.childCount < 6)
                        g.ImportMercenary("Boar", 0);
                    else
                        changeMade = false; 
                    break;
                case "Shattered_Sun_Cleric":
                    if(GameObject.Find("Enemy Board").transform.childCount > 1)
                    {
                        int chosenIndex = 0; 
                        if(GameObject.Find("Enemy Board").transform.childCount > 2)
                            chosenIndex = Random.Range(0, GameObject.Find("Enemy Board").transform.childCount-2);

                        GameObject obj = GameObject.Find("Enemy Board").transform.GetChild(chosenIndex).gameObject;

                        try
                        {
                            int hp = int.Parse(obj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                            ++hp;
                            obj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = hp + "";

                            int attackValue = int.Parse(obj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);
                            ++attackValue;
                            obj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = attackValue + "";
                        } catch(System.FormatException fe) { }
                    }
                    else
                    {
                        changeMade = false; 
                    }
                    break;
                case "Frostwolf_Warlord":
                    int count = GameObject.Find("Enemy Board").transform.childCount;
                    if (count > 1)
                    {
                        --count; 
                        for(int i = 0; i < count; i++)
                        {
                            if(i % 2 == 0) // jämna tal
                            {
                                GameObject obj = GameObject.Find("Enemy Board").transform.GetChild(i).gameObject;

                                try
                                {
                                    int hp = int.Parse(obj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                                    ++hp;
                                    obj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = hp + "";

                                    int attackValue = int.Parse(obj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);
                                    ++attackValue;
                                    obj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = attackValue + "";
                                }
                                catch (System.FormatException fe) { }
                            }
                        }
                    }
                    break;
                case "Stormwind_Champion":
                    int childCount = GameObject.Find("Enemy Board").transform.childCount;
                    if (childCount > 1)
                    {
                        --childCount; 
                        for(int i = 0; i < childCount; i++)
                        {
                           
                            GameObject obj = GameObject.Find("Enemy Board").transform.GetChild(i).gameObject;

                            try
                            {
                                int hp = int.Parse(obj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                                ++hp;
                                obj.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = hp + "";

                                int attackValue = int.Parse(obj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);
                                ++attackValue;
                                obj.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = attackValue + "";
                            }
                            catch (System.FormatException fe) { }
                            
                        }
                    }
                    break; 
                default:
                    break;
            }

            if(!changeMade)
            {
                Destroy(mercObject);
                e.mercenaries.Remove(mercObject);
                g.ReloadMercenaries(0);
            }
        }
        else if(card.cardType == CardType.Weapon)
        {
            if(GameObject.Find("Enemy Weapon").transform.GetChild(0).GetComponent<Image>().enabled)
            {
                changeMade = false;
            }
            else
            {
                g.AddWeapon(cardName, 0);
            }
        }
        else if(card.cardType == CardType.Spell)
        {
            switch(cardName)
            {
                case "Blessing_of_Kings":
                    if (GameObject.Find("Enemy Board").transform.childCount > 0)
                    {
                        int count = GameObject.Find("Enemy Board").transform.childCount;
                        int minionIndex = 0;
                        if (count > 1)
                        {
                            minionIndex = Random.Range(0, count - 1);
                        }

                        GameObject minion = GameObject.Find("Enemy Board").transform.GetChild(minionIndex).gameObject;

                        try
                        {
                            int hp = int.Parse(minion.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                            hp += 4;
                            minion.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = hp + "";

                            int attack = int.Parse(minion.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);
                            attack += 4;
                            minion.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = attack + "";
                        }
                        catch (System.FormatException fe) { }

                    }
                    else
                    {
                        changeMade = false;
                    }
                    break;
                case "Consecration":
                    if(GameObject.Find("Player Board").transform.childCount > 0)
                    {
                        foreach(Transform tr in GameObject.Find("Player Board").transform)
                        {
                            try
                            {
                                int hp = int.Parse(tr.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                                hp -= 2; 

                                if(hp <= 0)
                                {
                                    Player p = GameObject.Find("Scripts").GetComponent<Player>();
                                    p.mercenaries.Remove(tr.gameObject);

                                    Destroy(tr.gameObject);

                                    g.ReloadMercenaries(1);
                                }
                                else
                                {
                                    tr.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = hp + "";
                                }
                            } catch(System.FormatException fe) { }
                        }
                    }
                    else
                    {
                        changeMade = false; 
                    }
                    break;
                case "Hammer_of_Wrath":
                    print("Hammer of wrath");
                    if(GameObject.Find("Player Board").transform.childCount > 0)
                    {
                        int minionIndex = 0;
                        if (GameObject.Find("Player Board").transform.childCount > 1)
                            minionIndex = Random.Range(0, GameObject.Find("Player Board").transform.childCount-1);

                        GameObject minion = GameObject.Find("Player Board").transform.GetChild(minionIndex).gameObject;
                        try
                        {
                            int hp = int.Parse(minion.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                            hp -= 3;

                            if (hp <= 0)
                            {
                                Player p = GameObject.Find("Scripts").GetComponent<Player>();
                                p.mercenaries.Remove(minion);

                                Destroy(minion);

                                g.ReloadMercenaries(1);
                            }
                            else
                            {
                                minion.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = hp + "";
                            }
                        }
                        catch (System.FormatException fe) { }
                    }
                    else
                    {
                        GameObject hero = GameObject.Find("Player Hero");
                        try
                        {
                            int hp = int.Parse(hero.transform.GetChild(2).GetChild(0).GetComponent<Text>().text);
                            hp -= 3;

                            if (hp <= 0)
                            {
                                print("You Lose");
                                g.gameIsFinished = true; 
                            }
                            else
                            {
                                hero.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = hp + "";
                            }
                        }
                        catch (System.FormatException fe) { }
                    }

                    List<string> paladinDeckCards = new List<string>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < g.paladinDeck[i].Count; j++)
                        {
                            paladinDeckCards.Add(g.paladinDeck[i][j]);
                        }
                    }

                    int cardIndex = Random.Range(0, paladinDeckCards.Count);
                    g.ImportCard(paladinDeckCards[cardIndex], 0);
                    break; 
                default:
                    break; 
            }
        }

        if(changeMade)
        {
            Destroy(GameObject.Find("Enemy Deck").transform.GetChild(index).gameObject);

            e.cardObjects.RemoveAt(index);

            g.ReloadCards(0);
        }

    }

}
