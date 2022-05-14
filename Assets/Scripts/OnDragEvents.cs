using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnDragEvents : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    float boardX1 = -296, boardX2 = 296;
    float boardY1 = 257, boardY2 = 166;

    float boardY = 203;
    float deckY = 0;

    bool placeable = true;
    public bool draggable = false; 

    Vector2 card_onBeginDragStartPos;
    Vector2 merc_onBeginDragStartPos;

    public bool elvenArcher = false;
    public bool shatteredSunCleric = false; 
    public bool stormpikeCommando = false;

    public OnDragEvents()
    {

    }
    void Update()
    {

        if(elvenArcher || shatteredSunCleric || stormpikeCommando)
        {
            merc_onBeginDragStartPos = this.gameObject.GetComponent<RectTransform>().position;

            GameObject arrowClone = GameObject.Find("Arrow(Clone)");
            GameObject triangle = arrowClone.transform.GetChild(0).gameObject;
            GameObject boxes = arrowClone.transform.GetChild(1).gameObject;

            RectTransform rt = arrowClone.GetComponent<RectTransform>();
            rt.position = merc_onBeginDragStartPos;

            arrowClone.GetComponent<RectTransform>().rotation = Quaternion.identity;

            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldStartPos = this.gameObject.GetComponent<RectTransform>().position;
            float arrowHeight = Mathf.Sqrt(Mathf.Pow(mousePos.x - worldStartPos.x, 2) + Mathf.Pow(mousePos.y - worldStartPos.y, 2));

            float margin = 20;
            if (arrowHeight < margin)
            {
                arrowHeight = margin;
            }

            rt = arrowClone.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.rect.width, arrowHeight * 2);

            rt = boxes.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.rect.width, arrowHeight);

            float prevY = rt.anchoredPosition.y;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, arrowHeight / 2);

            foreach (Transform box in boxes.transform)
            {
                rt = box.gameObject.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + (arrowHeight / 2 - prevY));
            }

            float angle = 0;
            if (mousePos.x > worldStartPos.x) //till höger om mercenary
            {
                if (mousePos.y > worldStartPos.y) //över mercenary
                {
                    angle = Mathf.Atan2((mousePos.y - merc_onBeginDragStartPos.y), (mousePos.x - merc_onBeginDragStartPos.x)) * Mathf.Rad2Deg;
                    angle = 90 - angle;
                }
                else if (mousePos.y < worldStartPos.y)
                {
                    angle = Mathf.Atan2((merc_onBeginDragStartPos.y - mousePos.y), (mousePos.x - merc_onBeginDragStartPos.x)) * Mathf.Rad2Deg;
                    angle += 90;
                }
            }
            else if (mousePos.x < worldStartPos.x)
            {
                if (mousePos.y > worldStartPos.y)
                {
                    angle = Mathf.Atan2((mousePos.y - merc_onBeginDragStartPos.y), (merc_onBeginDragStartPos.x - mousePos.x)) * Mathf.Rad2Deg;
                    angle += 270;
                }
                else if (mousePos.y < worldStartPos.y)
                {
                    angle = Mathf.Atan2((merc_onBeginDragStartPos.x - mousePos.x), (merc_onBeginDragStartPos.y - mousePos.y)) * Mathf.Rad2Deg;
                    angle += 180;
                }
            }

            arrowClone.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -angle), Space.Self);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        //print("On Begin Drag");

        if(tag == "Card")
        {
            card_onBeginDragStartPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition;

            foreach(Transform mercenary in GameObject.Find("Player Board").transform)
            {
                mercenary.GetChild(1).GetComponent<Image>().raycastTarget = false; 
                mercenary.GetChild(1).GetChild(0).GetComponent<Image>().raycastTarget = false; 
                mercenary.GetChild(1).GetChild(1).GetComponent<Image>().raycastTarget = false; 
                mercenary.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().raycastTarget = false; 
                mercenary.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().raycastTarget = false;

                mercenary.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().raycastTarget = false;
                mercenary.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().raycastTarget = false;
            }

            foreach (Transform mercenary in GameObject.Find("Enemy Board").transform)
            {
                mercenary.GetChild(0).GetComponent<Image>().raycastTarget = false;
                mercenary.GetChild(0).GetChild(0).GetComponent<Image>().raycastTarget = false;
                mercenary.GetChild(0).GetChild(1).GetComponent<Image>().raycastTarget = false;
                mercenary.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().raycastTarget = false;
                mercenary.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().raycastTarget = false;

                mercenary.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().raycastTarget = false;
                mercenary.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().raycastTarget = false;
            }
        }
        else if (tag == "Mercenary" || (tag == "Hero" && GameObject.Find("Scripts").GetComponent<Player>().attack > 0))
        {
            if(draggable)
            {
                GameObject arrow = GameObject.Find("Arrow");

                GameObject arrowClone = Instantiate(arrow);
                GameObject triangle = arrowClone.transform.GetChild(0).gameObject;
                GameObject boxes = arrowClone.transform.GetChild(1).gameObject;

                merc_onBeginDragStartPos = this.gameObject.GetComponent<RectTransform>().position;

                arrowClone.transform.parent = GameObject.Find("Board").transform;
                arrowClone.layer = LayerMask.NameToLayer("UI");

                RectTransform rt = arrowClone.GetComponent<RectTransform>();
                rt.position = merc_onBeginDragStartPos;

                if(tag == "Mercenary")
                    GetComponent<CanvasGroup>().blocksRaycasts = false;

            }

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (tag == "Card")
        {
            Game g = GameObject.Find("Scripts").GetComponent<Game>();
            if (placeable && g.playerTurn)
            {
                GetComponent<RectTransform>().anchoredPosition += eventData.delta;
            }
        }
        else if (tag == "Mercenary" || (tag == "Hero" && GameObject.Find("Scripts").GetComponent<Player>().attack > 0))
        {

            if(draggable)
            {
                GameObject arrowClone = GameObject.Find("Arrow(Clone)");
                GameObject triangle = arrowClone.transform.GetChild(0).gameObject;
                GameObject boxes = arrowClone.transform.GetChild(1).gameObject;

                arrowClone.GetComponent<RectTransform>().rotation = Quaternion.identity;

                Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 worldStartPos = this.gameObject.GetComponent<RectTransform>().position;
                float arrowHeight = Mathf.Sqrt(Mathf.Pow(mousePos.x - worldStartPos.x, 2) + Mathf.Pow(mousePos.y - worldStartPos.y, 2));

                float margin = 20;
                if (arrowHeight < margin)
                {
                    arrowHeight = margin;
                }

                RectTransform rt = arrowClone.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(rt.rect.width, arrowHeight * 2);

                rt = boxes.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(rt.rect.width, arrowHeight);

                float prevY = rt.anchoredPosition.y;
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, arrowHeight / 2);

                foreach (Transform box in boxes.transform)
                {
                    rt = box.gameObject.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + (arrowHeight / 2 - prevY));
                }

                float angle = 0;
                if (mousePos.x > worldStartPos.x) //till höger om mercenary
                {
                    if (mousePos.y > worldStartPos.y) //över mercenary
                    {
                        angle = Mathf.Atan2((mousePos.y - merc_onBeginDragStartPos.y), (mousePos.x - merc_onBeginDragStartPos.x)) * Mathf.Rad2Deg;
                        angle = 90 - angle;
                    }
                    else if (mousePos.y < worldStartPos.y)
                    {
                        angle = Mathf.Atan2((merc_onBeginDragStartPos.y - mousePos.y), (mousePos.x - merc_onBeginDragStartPos.x)) * Mathf.Rad2Deg;
                        angle += 90;
                    }
                }
                else if (mousePos.x < worldStartPos.x)
                {
                    if (mousePos.y > worldStartPos.y)
                    {
                        angle = Mathf.Atan2((mousePos.y - merc_onBeginDragStartPos.y), (merc_onBeginDragStartPos.x - mousePos.x)) * Mathf.Rad2Deg;
                        angle += 270;
                    }
                    else if (mousePos.y < worldStartPos.y)
                    {
                        angle = Mathf.Atan2((merc_onBeginDragStartPos.x - mousePos.x), (merc_onBeginDragStartPos.y - mousePos.y)) * Mathf.Rad2Deg;
                        angle += 180;
                    }
                }

                arrowClone.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -angle), Space.Self);
            }
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("On End Drag");

        if (this.gameObject.tag == "Card")
        {
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;

            Card card = Resources.Load<Card>("Cards/" + this.gameObject.name);

            if (pos.x >= boardX1 && pos.x <= boardX2 && pos.y <= boardY1 && pos.y >= boardY2)
            {

                Game g = GameObject.Find("Scripts").GetComponent<Game>();

                int manaLeft = g.maxMana - (GameObject.Find("Mana Text").GetComponent<Text>().text[0] - 48); //ascii
                int mana = Resources.Load<Card>("Cards/" + this.gameObject.name).mana; 

                if(manaLeft >= mana)
                {

                    bool changeMade = false; 
                    if(card.cardType == CardType.Weapon)
                    {
                        if(!GameObject.Find("Player Weapon").transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled)
                        {
                            g.AddWeapon(this.gameObject.name, 1);
                            changeMade = true; 
                        }
                        else
                        {
                            GetComponent<RectTransform>().anchoredPosition = card_onBeginDragStartPos;
                        }
                    }
                    else if(card.cardType == CardType.Minion)
                    {
                        g.ImportMercenary(this.gameObject.name, 1);
                        changeMade = true;
                        switch (this.gameObject.name) // battlecry events 
                        {
                            case "Elven_Archer":
                            case "Stormpike_Commando":
                                GameObject arrow = GameObject.Find("Arrow");

                                GameObject arrowClone = Instantiate(arrow);
                                GameObject triangle = arrowClone.transform.GetChild(0).gameObject;
                                GameObject boxes = arrowClone.transform.GetChild(1).gameObject;

                                merc_onBeginDragStartPos = this.gameObject.GetComponent<RectTransform>().position;

                                arrowClone.transform.parent = GameObject.Find("Board").transform;
                                arrowClone.layer = LayerMask.NameToLayer("UI");

                                RectTransform rt = arrowClone.GetComponent<RectTransform>();
                                rt.position = merc_onBeginDragStartPos;

                                GetComponent<CanvasGroup>().blocksRaycasts = false;

                                GameObject board = GameObject.Find("Player Board");
                                GameObject obj = board.transform.GetChild(board.transform.childCount-1).gameObject;

                                if(this.gameObject.name == "Elven_Archer")
                                    obj.GetComponent<OnDragEvents>().elvenArcher = true;
                                else if(this.gameObject.name == "Stormpike_Commando")
                                    obj.GetComponent<OnDragEvents>().stormpikeCommando = true;

                                break;
                            case "Acidic_Swamp_Ooze":
                                if(GameObject.Find("Enemy Weapon").transform.GetChild(0).GetComponent<Image>().enabled)
                                {
                                    GameObject weapon = GameObject.Find("Enemy Weapon");
                                    weapon.transform.GetChild(0).GetComponent<Image>().enabled = false; 
                                    weapon.transform.GetChild(1).GetComponent<Image>().enabled = false; 
                                    weapon.transform.GetChild(2).GetComponent<Image>().enabled = false; 
                                    weapon.transform.GetChild(3).GetComponent<Image>().enabled = false; 

                                    weapon.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = false; 
                                    weapon.transform.GetChild(3).GetChild(0).GetComponent<Text>().enabled = false; 
                                }
                                break;
                            case "Razorfen_Hunter":
                                g.ImportMercenary("Boar", 1);
                                break;
                            case "Shattered_Sun_Cleric":
                                if(GameObject.Find("Player Board").transform.childCount > 0)
                                {
                                    arrow = GameObject.Find("Arrow");

                                    arrowClone = Instantiate(arrow);
                                    triangle = arrowClone.transform.GetChild(0).gameObject;
                                    boxes = arrowClone.transform.GetChild(1).gameObject;

                                    merc_onBeginDragStartPos = this.gameObject.GetComponent<RectTransform>().position;

                                    arrowClone.transform.parent = GameObject.Find("Board").transform;
                                    arrowClone.layer = LayerMask.NameToLayer("UI");

                                    rt = arrowClone.GetComponent<RectTransform>();
                                    rt.position = merc_onBeginDragStartPos;

                                    GetComponent<CanvasGroup>().blocksRaycasts = false;

                                    board = GameObject.Find("Player Board");
                                    obj = board.transform.GetChild(board.transform.childCount - 1).gameObject;

                                    obj.GetComponent<OnDragEvents>().shatteredSunCleric = true;
                                }
                                else
                                {
                                    changeMade = false; 
                                }
                                break;
                            case "Stormwind_Champion":
                                if(GameObject.Find("Player Board").transform.childCount > 0)
                                {
                                    foreach(Transform mercenary in GameObject.Find("Player Board").transform)
                                    {
                                        try
                                        {
                                            int hp = int.Parse(mercenary.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                                            ++hp;
                                            mercenary.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = hp + "";

                                            int attack = int.Parse(mercenary.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text);
                                            ++attack;
                                            mercenary.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = attack + "";
                                        } catch(System.FormatException fe) { }
                                    }
                                }
                                break;
                            case "Gnomish_Inventor":
                                List<string> warriorDeckCards = new List<string>();
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < g.warriorDeck[i].Count; j++)
                                    {
                                        warriorDeckCards.Add(g.warriorDeck[i][j]);
                                    }
                                }

                                int index = Random.Range(0, warriorDeckCards.Count);
                                g.ImportCard(warriorDeckCards[index], 1);
                                break; 
                            default:
                                break;
                        }
                         
                    }
                    else if(card.cardType == CardType.Spell)
                    {
                        changeMade = true; 
                        switch(this.gameObject.name)
                        {
                            case "Cleave":
                                print("Cleave");
                                Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
                                Player p = GameObject.Find("Scripts").GetComponent<Player>();
                                List<GameObject> enemyMercenaries = e.mercenaries;
                                if (enemyMercenaries.Count == 0)
                                    changeMade = false; 
                                else if(enemyMercenaries.Count == 1)
                                {
                                    p.DealDamage(0, 2);
                                }
                                else if(enemyMercenaries.Count == 2)
                                {
                                    p.DealDamage(0, 2);
                                    p.DealDamage(1, 2);
                                }
                                else
                                {
                                    List<int> allIndexes = new List<int>();
                                    for(int i = 0; i < e.mercenaries.Count; i++)
                                    {
                                        allIndexes.Add(i); 
                                    }
                                    int n1 = allIndexes[Random.Range(0, allIndexes.Count)];

                                    allIndexes.RemoveAt(n1);
                                    int n2 = allIndexes[Random.Range(0, allIndexes.Count)];

                                    p.DealDamage(n1, 2);
                                    p.DealDamage(n2, 2);
                                }
                                break;
                            case "Execute":
                                int amountOfDamagedMinions = 0;
                                List<GameObject> damagedMinons = new List<GameObject>();
                                foreach(GameObject enemyMercenary in GameObject.Find("Scripts").GetComponent<Enemy>().mercenaries)
                                {
                                    try
                                    {
                                        int maxHealth = Resources.Load<Card>("Cards/" + enemyMercenary.name).health;
                                        int currentHealth = int.Parse(enemyMercenary.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text);
                                        if (currentHealth < maxHealth) {
                                            amountOfDamagedMinions++;
                                            damagedMinons.Add(enemyMercenary);
                                        }
                                    } catch(System.FormatException fe) { }
                                }

                                if(amountOfDamagedMinions > 0)
                                {
                                    if(amountOfDamagedMinions == 1)
                                    {
                                        GameObject.Find("Scripts").GetComponent<Enemy>().mercenaries.Remove(damagedMinons[0]);
                                        Destroy(damagedMinons[0]);
                                    }
                                    else
                                    {
                                        int minionIndex = Random.Range(0, damagedMinons.Count-1);
                                        GameObject.Find("Scripts").GetComponent<Enemy>().mercenaries.Remove(damagedMinons[minionIndex]);
                                        Destroy(damagedMinons[minionIndex]);
                                    }
                                    g.ReloadMercenaries(0);
                                }
                                else
                                {
                                    changeMade = false; 
                                }
                                break;
                            case "Shield_Block":
                                try
                                {
                                    int armor = int.Parse(GameObject.Find("Player Hero").transform.GetChild(3).GetChild(0).GetComponent<Text>().text);
                                    armor += 5;
                                    GameObject.Find("Player Hero").transform.GetChild(3).GetChild(0).GetComponent<Text>().text = armor + "";

                                    List<string> warriorDeckCards = new List<string>();
                                    for(int i = 0; i < 3; i++)
                                    {
                                        for(int j = 0; j < g.warriorDeck[i].Count; j++)
                                        {
                                            warriorDeckCards.Add(g.warriorDeck[i][j]);
                                        }
                                    }

                                    int index = Random.Range(0, warriorDeckCards.Count);
                                    g.ImportCard(warriorDeckCards[index], 1);
                                } catch(System.FormatException fe) { }
                                break; 
                            default:
                                changeMade = false; 
                                break; 
                        }
                    }

                    if(changeMade)
                    {
                        Destroy(this.gameObject);

                        Player p = GameObject.Find("Scripts").GetComponent<Player>();

                        int index = 0;
                        if (transform.parent.childCount > 1)
                            index = transform.GetSiblingIndex();

                        p.cardObjects.RemoveAt(index);
                        g.ReloadCards(1);

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

                        g.ReloadBorders();
                    }
                    else
                    {
                        GetComponent<RectTransform>().anchoredPosition = card_onBeginDragStartPos;
                    }

                }
                else
                {
                    GetComponent<RectTransform>().anchoredPosition = card_onBeginDragStartPos;
                }

            }
            else
            {
                GetComponent<RectTransform>().anchoredPosition = card_onBeginDragStartPos;
            }

            foreach (Transform mercenary in GameObject.Find("Player Board").transform)
            {
                mercenary.GetChild(1).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(1).GetChild(0).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(1).GetChild(1).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().raycastTarget = true;

                mercenary.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().raycastTarget = true;
                mercenary.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().raycastTarget = true;
            }

            foreach (Transform mercenary in GameObject.Find("Enemy Board").transform)
            {
                mercenary.GetChild(0).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(0).GetChild(0).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(0).GetChild(1).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().raycastTarget = true;
                mercenary.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().raycastTarget = true;

                mercenary.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().raycastTarget = true;
                mercenary.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().raycastTarget = false;
            }
        }
        else if (tag == "Mercenary" || (tag == "Hero" && GameObject.Find("Scripts").GetComponent<Player>().attack > 0))
        {
            if(draggable)
            {
                Destroy(GameObject.Find("Arrow(Clone)"));

                foreach (Transform enemyMerc in GameObject.Find("Enemy Board").transform)
                {
                    try
                    {
                        if (enemyMerc.GetComponent<OnClickEvents>().pointerIsOverObject)
                        {
                            Player p = GameObject.Find("Scripts").GetComponent<Player>();

                            int playerIndex = 0;
                            if (tag == "Hero")
                                playerIndex = -1;
                            else if(GameObject.Find("Player Board").transform.childCount > 1)
                                playerIndex = transform.GetSiblingIndex();

                            int enemyIndex = 0;
                            if (GameObject.Find("Enemy Board").transform.childCount > 1)
                                enemyIndex = enemyMerc.GetSiblingIndex();

                            if(!enemyMerc.transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("Taunt"))
                            {
                                bool anotherMercenaryHasTaunt = false; 
                                foreach(Transform enemyMerc2 in GameObject.Find("Enemy Board").transform)
                                {
                                    if(enemyMerc2.gameObject != enemyMerc.gameObject)
                                    {
                                        if (enemyMerc2.transform.GetChild(0).GetComponent<Image>().sprite.name.Contains("Taunt"))
                                            anotherMercenaryHasTaunt = true; 
                                    }
                                }

                                if(!anotherMercenaryHasTaunt)
                                {
                                    p.Attack(playerIndex, enemyIndex);
                                }
                            }
                            else
                            {
                                p.Attack(playerIndex, enemyIndex);
                            }

                            break;
                        }
                    }
                    catch (MissingComponentException mce) { }

                }

                bool attack = false; 
                try
                {
                    if (GameObject.Find("Enemy Hero").GetComponent<OnClickEvents>().pointerIsOverObject)
                    {
                        Player p = GameObject.Find("Scripts").GetComponent<Player>();

                        int playerIndex = 0;
                        if (tag == "Hero")
                            playerIndex = -1;
                        else if (GameObject.Find("Player Board").transform.childCount > 1)
                            playerIndex = transform.GetSiblingIndex();

                        attack = true; 
                        p.Attack(playerIndex, -1);
                    }
                }
                catch (MissingComponentException mce) { }

                if (tag == "Mercenary")
                {
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                    if (attack)
                        draggable = false; 
                }
                else
                {
                    draggable = false;
                }
              
            }
            
        }
    }

}
