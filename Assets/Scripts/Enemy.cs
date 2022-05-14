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

        Game g = GameObject.Find("Scripts").GetComponent<Game>();

        string cardName = GameObject.Find("Enemy Deck").transform.GetChild(index).name;
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

            mercenaries.Add(mercObject);

            float margin = 10;
            float width = mercObject.GetComponent<RectTransform>().rect.width;
            float lengthOfLine = 0;

            foreach (GameObject obj in mercenaries)
            {
                lengthOfLine += width + margin;
            }
            lengthOfLine -= margin;

            float startPosX = -lengthOfLine / 2;
            for (int i = 0; i < mercenaries.Count; i++)
            {
                float x = startPosX + i * (width + margin);
                mercenaries[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + width / 2, 0);
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
                    print("Blessing_of_Kings");
                    break; 
                default:
                    break; 
            }
        }

        if(changeMade)
        {
            cardObjects.RemoveAt(index);
            g.ReloadCards(0);
        }
        
    }

}
