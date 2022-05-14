using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.Rendering.PostProcessing; 

/// <summary>
/// "Main"-klassen, här implementeras all gameplay
/// </summary>
public class Game : MonoBehaviour
{

    public readonly string cardsURL = "Cards/";
    public readonly string cardsImagesURL = "Images/Cards/";
    public readonly string cardsFramesURL = "Images/Cards/Frames/";
    public readonly string belweFontsURL = "Fonts/belwe/";

    public readonly string usedFont = "belwe bold bt";

    public bool playerTurn = false;
    bool prevPlayerTurn = false;
    public bool gameIsFinished = false;
    public Color buttonNotClickable;
    public Color buttonClickable;

    public int maxMana = 1;
    public Color manaLight, manaDark;

    public List<List<string>> warriorDeck = new List<List<string>>();
    public List<List<string>> paladinDeck = new List<List<string>>();

    Enemy e = new Enemy();
    Player p = new Player();

    public GameObject enemyDeck;

    public Canvas defaultCanvas, gameEndedCanvas;

    public Game()
    {

    }

    void Start()
    {
        print("Start");

        List<string> warriorDeck_spell = new List<string>();
        List<string> warriorDeck_weapon = new List<string>();
        List<string> warriorDeck_minion = new List<string>();

        List<string> paladinDeck_spell = new List<string>();
        List<string> paladinDeck_weapon = new List<string>();
        List<string> paladinDeck_minion = new List<string>();

        p = GameObject.Find("Scripts").GetComponent<Player>();
        e = GameObject.Find("Scripts").GetComponent<Enemy>();

        warriorDeck_spell.Add("Cleave");
        warriorDeck_spell.Add("Execute");
        warriorDeck_spell.Add("Shield_Block");
        warriorDeck.Add(warriorDeck_spell);

        warriorDeck_weapon.Add("Fiery_War_Axe");
        warriorDeck_weapon.Add("Archanite_Reaper");
        warriorDeck.Add(warriorDeck_weapon);

        warriorDeck_minion.Add("Kor'kron_Elite");
        warriorDeck_minion.Add("Elven_Archer");
        warriorDeck_minion.Add("Acidic_Swamp_Ooze");
        warriorDeck_minion.Add("Bloodfen_Raptor");
        warriorDeck_minion.Add("River_Crocolisk");
        warriorDeck_minion.Add("Razorfen_Hunter");
        warriorDeck_minion.Add("Shattered_Sun_Cleric");
        warriorDeck_minion.Add("Chillwind_Yeti");
        warriorDeck_minion.Add("Gnomish_Inventor");
        warriorDeck_minion.Add("Sen'jin_Shieldmasta");
        warriorDeck_minion.Add("Stormpike_Commando");
        warriorDeck_minion.Add("Boulderfist_Ogre");
        warriorDeck_minion.Add("Stormwind_Champion");
        warriorDeck.Add(warriorDeck_minion);

        paladinDeck_spell.Add("Blessing_of_Kings");
        paladinDeck_spell.Add("Consecration");
        paladinDeck_spell.Add("Hammer_of_Wrath");
        paladinDeck.Add(paladinDeck_spell);

        paladinDeck_weapon.Add("Truesilver_Champion");
        paladinDeck.Add(paladinDeck_weapon);

        paladinDeck_minion.Add("Guardian_of_Kings");
        paladinDeck_minion.Add("Acidic_Swamp_Ooze");
        paladinDeck_minion.Add("Bloodfen_Raptor");
        paladinDeck_minion.Add("Murloc_Tidehunter");
        paladinDeck_minion.Add("River_Crocolisk");
        paladinDeck_minion.Add("Razorfen_Hunter");
        paladinDeck_minion.Add("Shattered_Sun_Cleric");
        paladinDeck_minion.Add("Chillwind_Yeti");
        paladinDeck_minion.Add("Frostwolf_Warlord");
        paladinDeck_minion.Add("Boulderfist_Ogre");
        paladinDeck_minion.Add("Stormwind_Champion");
        paladinDeck.Add(paladinDeck_minion);

        ImportCard("River_Crocolisk", 0);
        ImportCard("Murloc_Tidehunter", 0);
        ImportCard("Hammer_of_Wrath", 0);

        ImportCard("Elven_Archer", 1);
        ImportCard("Razorfen_Hunter", 1);
        ImportCard("Shattered_Sun_Cleric", 1);
        ImportCard("Stormpike_Commando", 1);
        ImportCard("Gnomish_Inventor", 1);

        #region Välja kort efter cardDeck
        //List<string> cardDeck = Resources.Load<PublicData>("PublicData").cardDeck;
        //List<int> chosenIndexes = new List<int>();
        //for (int i = 0; i < 3; i++)
        //{
        //    int index = Random.Range(0, 29);
        //    if (!chosenIndexes.Contains(index))
        //    {
        //        ImportCard(cardDeck[index], 1);
        //    }
        //    else
        //    {
        //        while (chosenIndexes.Contains(index))
        //        {
        //            index = Random.Range(0, 29);
        //        }
        //    }
        //}

        //chosenIndexes = new List<int>();
        //for (int i = 0; i < 3; i++)
        //{
        //    int index = Random.Range(0, 29);
        //    if (!chosenIndexes.Contains(index))
        //    {
        //        ImportCard(cardDeck[index], 0);
        //    }
        //    else
        //    {
        //        int margin = 200, k = 0;
        //        while (chosenIndexes.Contains(index) && k < margin)
        //        {
        //            index = Random.Range(0, 29);
        //            k++; 
        //        }
        //        ImportCard(cardDeck[index], 0);
        //    }
        //}
        #endregion

        playerTurn = true;

        FindObjectOfType<AudioManager>().Play("test3");

        p.health = 30;
        p.armor = 0;
        p.heroPowerMana = 2; 

        e.health = 30;
        e.armor = 0;
        e.heroPowerMana = 2;

        foreach(Transform mana in GameObject.Find("Mana Bar").transform)
        {
            mana.GetComponent<Image>().color = manaDark;
            if (mana.GetSiblingIndex() < maxMana)
            {
                mana.GetComponent<Image>().enabled = true;
            }
            else
            {
                mana.GetComponent<Image>().enabled = false;
            }
        }

        GameObject.Find("Mana Text").GetComponent<Text>().text = "0/1";

        GameObject hero = GameObject.Find("Player Hero");
        hero.transform.GetChild(2).GetComponent<Image>().enabled = false; 
        hero.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
        hero.transform.GetChild(3).GetComponent<Image>().enabled = false;
        hero.transform.GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;

        hero = GameObject.Find("Enemy Hero");
        hero.transform.GetChild(2).GetComponent<Image>().enabled = false;
        hero.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
        hero.transform.GetChild(3).GetComponent<Image>().enabled = false;
        hero.transform.GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;

        GameObject weapon = GameObject.Find("Player Weapon");
        weapon.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false; 
        weapon.transform.GetChild(0).GetChild(1).GetComponent<Image>().enabled = false; 
        weapon.transform.GetChild(0).GetChild(2).GetComponent<Image>().enabled = false; 
        weapon.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().enabled = false; 
        weapon.transform.GetChild(0).GetChild(3).GetComponent<Image>().enabled = false;
        weapon.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;

        weapon = GameObject.Find("Enemy Weapon");
        weapon.transform.GetChild(0).GetComponent<Image>().enabled = false;
        weapon.transform.GetChild(1).GetComponent<Image>().enabled = false;
        weapon.transform.GetChild(2).GetComponent<Image>().enabled = false;
        weapon.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = false;
        weapon.transform.GetChild(3).GetComponent<Image>().enabled = false;
        weapon.transform.GetChild(3).GetChild(0).GetComponent<Text>().enabled = false;

        ReloadBorders();

    }

    void Update()
    {

        if (prevPlayerTurn == false && playerTurn == true)
        {
            GameObject endTurnButton = GameObject.Find("Button");
            ColorBlock cb = endTurnButton.GetComponent<Button>().colors;
            cb.normalColor = buttonClickable;
            endTurnButton.GetComponent<Button>().colors = cb;
            endTurnButton.GetComponent<Button>().interactable = true;
        }
        prevPlayerTurn = playerTurn;

    }

    /// <summary>
    /// Importerar ett kort till antingen spelaren eller fiendens kortlek
    /// </summary>
    /// <param name="cardName">Namnet på kortet att importera</param>
    /// <param name="side">Sidan där kortet ska läggas på (0 = fiende, 1 = spelare)</param>
    public void ImportCard(string cardName, int side)
    {

        Card card = Resources.Load<Card>(cardsURL + cardName);

        if(side == 0)
        {

            Sprite frame = Resources.Load<Sprite>(cardsFramesURL + "Card_Back");
            GameObject deck = GameObject.Find("Enemy Deck");
            GameObject cardObject = new GameObject(cardName, typeof(RectTransform));
            cardObject.transform.parent = deck.transform;
            cardObject.layer = LayerMask.NameToLayer("UI");
            cardObject.tag = "Card";

            float cardWidth = 70; 
            float cardHeight = 100; 

            cardObject.AddComponent<Image>();
            cardObject.GetComponent<Image>().sprite = frame;
            cardObject.GetComponent<RectTransform>().sizeDelta = new Vector2(cardWidth, cardHeight);
            cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
            e.cardObjects.Add(cardObject);

            float margin = 35;
            float lengthOfLine = 0;

            foreach (GameObject obj in e.cardObjects)
            {
                lengthOfLine += cardWidth - margin;
            }
            lengthOfLine += margin;

            float startPosX = -lengthOfLine / 2;
            for (int i = 0; i < e.cardObjects.Count; i++)
            {
                float x = startPosX + i * (cardWidth - margin);
                e.cardObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + cardWidth / 2, 0);
            }

        }
        else if(side == 1)
        {

            GameObject cardCopy = GameObject.Find("Card Normal");

            int health = card.health;
            int attack = card.attack;
            int mana = card.mana;

            string name = card.name;
            string description = card.description;

            Sprite image = card.image;
            CardType cardType = card.cardType;

            int durability = card.durability; 

            Sprite manaSprite = Resources.Load<Sprite>(cardsImagesURL + "Mana");
            Sprite healthSprite = Resources.Load<Sprite>(cardsImagesURL + "Health");
            Sprite attackSprite = Resources.Load<Sprite>(cardsImagesURL + "Attack");
            if(cardType == CardType.Weapon)
            {
                healthSprite = Resources.Load<Sprite>(cardsImagesURL + "Durability");
                attackSprite = Resources.Load<Sprite>(cardsImagesURL + "Weapon Attack");
            }

            Sprite frame;
            string imageName = "";

            if (cardType == CardType.Minion)
                imageName = "Frame-minion-neutral";
            else if(cardType == CardType.Weapon)
                imageName = "Frame-weapon-warrior";
            else if (cardType == CardType.Spell)
                imageName = "Frame-spell-warrior";
            else //ändra sen för att anpassa till typ av kort
                imageName = "Frame-minion-neutral";

            frame = Resources.Load<Sprite>(cardsFramesURL + imageName);

            GameObject deck = GameObject.Find("Player Deck");
            GameObject cardObject = new GameObject(cardName, typeof(RectTransform));
            cardObject.layer = LayerMask.NameToLayer("UI");
            cardObject.transform.parent = deck.transform;
            cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            cardObject.tag = "Card";
            cardObject.AddComponent<OnClickEvents>();
            cardObject.AddComponent<OnDragEvents>();
            cardObject.AddComponent<CanvasGroup>();

            #region Skapande av card UI
            GameObject border = new GameObject("Green Border", typeof(RectTransform));
            border.transform.parent = cardObject.transform;
            border.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 125);
            border.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            border.AddComponent<Image>();
            border.GetComponent<Image>().sprite = Resources.Load<Sprite>(cardsFramesURL + imageName + "-mask");
            border.layer = LayerMask.NameToLayer("UI");
            border.AddComponent<Mask>();
            border.GetComponent<Mask>().showMaskGraphic = false;
            border.tag = "Green Border";

            GameObject borderObject = new GameObject("Object", typeof(RectTransform));
            borderObject.transform.parent = border.transform;
            borderObject.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 125);
            borderObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            borderObject.layer = LayerMask.NameToLayer("UI");
            borderObject.AddComponent<Image>();
            borderObject.GetComponent<Image>().color = GameObject.Find("Scripts").GetComponent<Game>().buttonClickable;

            GameObject mask = new GameObject("Image", typeof(RectTransform));
            mask.transform.parent = cardObject.transform;
            mask.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);
            mask.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            mask.AddComponent<Image>();
            mask.GetComponent<Image>().sprite = Resources.Load<Sprite>(cardsFramesURL + imageName + "-mask");
            mask.layer = LayerMask.NameToLayer("UI");
            mask.AddComponent<Mask>();
            mask.GetComponent<Mask>().showMaskGraphic = false;

            GameObject imageObj = new GameObject("Image", typeof(RectTransform));
            imageObj.transform.parent = mask.transform;
            imageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 70);
            imageObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 20);
            imageObj.AddComponent<Image>();
            imageObj.GetComponent<Image>().sprite = image;
            imageObj.layer = LayerMask.NameToLayer("UI");

            GameObject frameObject = new GameObject("Frame", typeof(RectTransform));
            frameObject.transform.parent = mask.transform;
            frameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);
            frameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            frameObject.AddComponent<Image>();
            frameObject.GetComponent<Image>().sprite = frame;
            frameObject.layer = LayerMask.NameToLayer("UI");

            GameObject nameText = new GameObject("Name", typeof(RectTransform));
            nameText.transform.parent = frameObject.transform;
            nameText.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 18);
            nameText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -8);
            nameText.AddComponent<Text>();
            nameText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
            nameText.GetComponent<Text>().fontSize = 8;
            nameText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            nameText.GetComponent<Text>().color = Color.white;
            nameText.GetComponent<Text>().text = name;
            nameText.layer = LayerMask.NameToLayer("UI");

            GameObject descriptionText = new GameObject("Description", typeof(RectTransform));
            GameObject descriptionCopy = cardCopy.transform.GetChild(1).GetChild(1).GetChild(1).gameObject; 
            descriptionText.transform.parent = frameObject.transform;
            descriptionText.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionCopy.GetComponent<RectTransform>().rect.width, descriptionCopy.GetComponent<RectTransform>().rect.height);
            descriptionText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, descriptionCopy.GetComponent<RectTransform>().anchoredPosition.y);
            descriptionText.AddComponent<Text>();
            descriptionText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
            descriptionText.GetComponent<Text>().fontSize = descriptionCopy.GetComponent<Text>().fontSize;
            descriptionText.GetComponent<Text>().verticalOverflow = descriptionCopy.GetComponent<Text>().verticalOverflow; 
            descriptionText.GetComponent<Text>().alignment = descriptionCopy.GetComponent<Text>().alignment;
            descriptionText.GetComponent<Text>().color = Color.white;
            descriptionText.GetComponent<Text>().text = description;
            descriptionText.layer = LayerMask.NameToLayer("UI");

            GameObject manaObject = new GameObject("Mana", typeof(RectTransform));
            manaObject.transform.parent = frameObject.transform;
            manaObject.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
            manaObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-36, 47);
            manaObject.AddComponent<Image>();
            manaObject.GetComponent<Image>().sprite = manaSprite;
            manaObject.GetComponent<Image>().maskable = false;
            manaObject.layer = LayerMask.NameToLayer("UI");

            GameObject manaText = new GameObject("Text", typeof(RectTransform));
            manaText.transform.parent = manaObject.transform;
            manaText.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            manaText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 2);
            manaText.AddComponent<Text>();
            manaText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
            manaText.GetComponent<Text>().fontSize = 18;
            manaText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            manaText.GetComponent<Text>().color = Color.white;
            manaText.GetComponent<Text>().text = mana + "";
            manaText.GetComponent<Text>().maskable = false;
            manaText.layer = LayerMask.NameToLayer("UI");

            if(cardType != CardType.Spell)
            {
                GameObject attackObject = new GameObject("Attack", typeof(RectTransform));
                attackObject.transform.parent = frameObject.transform;
                attackObject.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                attackObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-36, -52);
                attackObject.AddComponent<Image>();
                attackObject.GetComponent<Image>().sprite = attackSprite;
                attackObject.GetComponent<Image>().maskable = false;
                attackObject.layer = LayerMask.NameToLayer("UI");

                GameObject attackText = new GameObject("Text", typeof(RectTransform));
                attackText.transform.parent = attackObject.transform;
                attackText.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                attackText.GetComponent<RectTransform>().anchoredPosition = new Vector2(2, -1);
                attackText.AddComponent<Text>();
                attackText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
                attackText.GetComponent<Text>().fontSize = 18;
                attackText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                attackText.GetComponent<Text>().color = Color.white;
                attackText.GetComponent<Text>().text = attack + "";
                attackText.GetComponent<Text>().maskable = false;
                attackText.layer = LayerMask.NameToLayer("UI");

                GameObject healthObject = new GameObject("health", typeof(RectTransform));
                healthObject.transform.parent = frameObject.transform;
                healthObject.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                healthObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(36, -52);
                healthObject.AddComponent<Image>();
                healthObject.GetComponent<Image>().sprite = healthSprite;
                healthObject.GetComponent<Image>().maskable = false;
                healthObject.layer = LayerMask.NameToLayer("UI");

                GameObject healthText = new GameObject("Text", typeof(RectTransform));
                healthText.transform.parent = healthObject.transform;
                healthText.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                healthText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1);
                healthText.AddComponent<Text>();
                healthText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
                healthText.GetComponent<Text>().fontSize = 18;
                healthText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                healthText.GetComponent<Text>().color = Color.white;
                healthText.GetComponent<Text>().text = health + "";
                if (cardType == CardType.Weapon)
                    healthText.GetComponent<Text>().text = durability + "";
                healthText.GetComponent<Text>().maskable = false;
                healthText.layer = LayerMask.NameToLayer("UI");
            }

            #endregion

            cardObject.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 120);

            Player p = GameObject.Find("Scripts").GetComponent<Player>();
            p.cardObjects.Add(cardObject);

            ReloadCards(1);
        }
        else
        {
            Debug.Log("Fel i kod i metoden ImportCard()");
        }

    }

    /// <summary>
    /// Importerar en mercenary ("legosoldat", spelbar karaktär) till antingen spelaren eller fiendens del av spelplanen
    /// </summary>
    /// <param name="cardName">Namnet på kortet att importera</param>
    /// <param name="side">Sidan där karaktären ska läggas på (0 = fiende, 1 = spelare)</param>
    public void ImportMercenary(string cardName, int side)
    {

        string enemyOrPlayer = "";
        if (side == 0)
            enemyOrPlayer = "Enemy";
        else if (side == 1)
            enemyOrPlayer = "Player";
        else
        {
            Debug.Log("Fel värde på int side i ImportMercenary(string, int)"); //-
            return; 
        }

        Card card = Resources.Load<Card>(cardsURL + cardName);

        int health = card.health;
        int attack = card.attack;
        Sprite image = card.image;

        Sprite healthSprite = Resources.Load<Sprite>(cardsImagesURL + "Health");
        Sprite attackSprite = Resources.Load<Sprite>(cardsImagesURL + "Attack");

        Sprite frame;
        string imageName = "";
            
        if (card.cardType == CardType.Minion)
        {
            imageName = "Mercenary-Minion";
        }
        else //ändra senare
        {
            imageName = "Mercenary-Minion";
        }

        string frameName = cardsFramesURL + imageName; 
        if(card.description.Length >= 5)
        {
            if(card.description.Contains("Taunt"))
            {
                frameName = frameName + "-Taunt";
            }
        }

        GameObject mercObject = new GameObject(cardName, typeof(RectTransform));
        GameObject example = GameObject.Find("Mercenary Example");

        mercObject.transform.parent = GameObject.Find(enemyOrPlayer + " Board").transform;
        mercObject.layer = LayerMask.NameToLayer("UI");

        RectTransform rt = example.GetComponent<RectTransform>();
        mercObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        mercObject.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        mercObject.tag = "Mercenary";

        if(side == 1)
        {
            mercObject.AddComponent<OnDragEvents>();

            if(card.cardType == CardType.Minion & card.description.Length >= 6) // charge 
            {
                if(card.description.Contains("Charge"))
                {
                    mercObject.GetComponent<OnDragEvents>().draggable = true; 
                }
            }
        }

        mercObject.AddComponent<OnClickEvents>();
        mercObject.AddComponent<CanvasGroup>();

        mercObject.AddComponent<Mercenary>();
        mercObject.GetComponent<Mercenary>().health = health;
        mercObject.GetComponent<Mercenary>().attack = attack;

        #region Skapande av mercenary UI
        if(side == 1)
        {
            GameObject border = new GameObject("Green Border", typeof(RectTransform));
            rt = example.transform.GetChild(0).GetComponent<RectTransform>();
            border.transform.parent = mercObject.transform;
            border.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            border.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            border.AddComponent<Image>();
            border.GetComponent<Image>().sprite = Resources.Load<Sprite>(cardsFramesURL + imageName + "-mask");
            border.layer = LayerMask.NameToLayer("UI");
            border.AddComponent<Mask>();
            border.GetComponent<Mask>().showMaskGraphic = false;
            border.tag = "Green Border";

            GameObject borderObject = new GameObject("Object", typeof(RectTransform));
            rt = example.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
            borderObject.transform.parent = border.transform;
            borderObject.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
            borderObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            borderObject.layer = LayerMask.NameToLayer("UI");
            borderObject.AddComponent<Image>();
            borderObject.GetComponent<Image>().color = GameObject.Find("Scripts").GetComponent<Game>().buttonClickable;
        }

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
        attackText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
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
        healthText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
        healthText.GetComponent<Text>().fontSize = 10;
        healthText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        healthText.GetComponent<Text>().color = Color.white;
        healthText.GetComponent<Text>().maskable = false;
        #endregion

        if(side == 0)
        {

            Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
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

        }
        else
        {

            Player p = GameObject.Find("Scripts").GetComponent<Player>();
            p.mercenaries.Add(mercObject);

            float margin = 10;
            float width = mercObject.GetComponent<RectTransform>().rect.width;
            float lengthOfLine = 0;

            foreach (GameObject obj in p.mercenaries)
            {
                lengthOfLine += width + margin;
            }
            lengthOfLine -= margin;

            float startPosX = -lengthOfLine / 2;
            for (int i = 0; i < p.mercenaries.Count; i++)
            {
                float x = startPosX + i * (width + margin);
                p.mercenaries[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + width / 2, 0);
            }

        }

    }

    /// <summary>
    /// Lägger till ett vapen till karaktären 
    /// </summary>
    /// <param name="weaponName">Namnet på vapnet</param>
    /// <param name="side">Sidan där vapnet ska läggas till (1 == spelare, 2 == fiende)</param>
    public void AddWeapon(string weaponName, int side)
    {

        Card card = Resources.Load<Card>("Cards/" + weaponName);
        GameObject weaponObj, hero;
        if (side == 0)
        {
            weaponObj = GameObject.Find("Enemy Weapon");
            hero = GameObject.Find("Enemy Hero");
        }
        else if (side == 1)
        {
            weaponObj = GameObject.Find("Player Weapon");
            hero = GameObject.Find("Player Hero");
        }
        else
        {
            Debug.Log("Värde på int side i AddWeapon(string, int) är felaktigt initierad");
            return; 
        }

        if(side == 0)
        {
            weaponObj.transform.GetChild(0).GetComponent<Image>().enabled = true;
            weaponObj.transform.GetChild(0).GetComponent<Image>().sprite = card.image;
            weaponObj.transform.GetChild(1).GetComponent<Image>().enabled = true;

            weaponObj.transform.GetChild(2).GetComponent<Image>().enabled = true;
            weaponObj.transform.GetChild(3).GetComponent<Image>().enabled = true;

            weaponObj.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = true;
            weaponObj.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = card.attack + "";

            weaponObj.transform.GetChild(3).GetChild(0).GetComponent<Text>().enabled = true;
            weaponObj.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = card.durability + "";
        }
        else
        {
            weaponObj.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            weaponObj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = card.image;
            weaponObj.transform.GetChild(0).GetChild(1).GetComponent<Image>().enabled = true;

            weaponObj.transform.GetChild(0).GetChild(2).GetComponent<Image>().enabled = true;
            weaponObj.transform.GetChild(0).GetChild(3).GetComponent<Image>().enabled = true;

            weaponObj.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().enabled = true;
            weaponObj.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = card.attack + "";

            weaponObj.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().enabled = true;
            weaponObj.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = card.durability + "";
        }

        hero.transform.GetChild(2).GetComponent<Image>().enabled = true; 
        hero.transform.GetChild(2).GetChild(0).GetComponent<Text>().enabled = true; 
        hero.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = card.attack + "";

        if (side == 0)
            GameObject.Find("Scripts").GetComponent<Enemy>().attack = card.attack;
        else if(side == 1)
            GameObject.Find("Scripts").GetComponent<Player>().attack = card.attack;

    }

    /// <summary>
    /// Uppdaterar korthögen för spelare eller fiende
    /// </summary>
    /// <param name="side">Sidan där kort ska uppdateras (0 = fiende, 1 = spelare)</param>
    public void ReloadCards(int side)
    {

        if(side == 0)
        {

            Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
            if (e.cardObjects.Count > 0)
            {

                float margin = 35;
                float cardWidth = GameObject.Find("Enemy Deck").transform.GetChild(0).GetComponent<RectTransform>().rect.width;
                float lengthOfLine = 0;

                foreach (GameObject obj in e.cardObjects)
                {
                    lengthOfLine += cardWidth - margin;
                }
                lengthOfLine += margin;

                //float totalAngle = 30;
                //float angle = totalAngle / p.cardObjects.Count;
                //float startAngle = -totalAngle / 2;

                float startPosX = -lengthOfLine / 2;
                for (int i = 0; i < e.cardObjects.Count; i++)
                {
                    float x = startPosX + i * (cardWidth - margin);
                    e.cardObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + cardWidth / 2, 0);
                    //e.cardObjects[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -1 * (startAngle + angle*i));
                }
            }

        }
        else if(side == 1)
        {
            Player p = GameObject.Find("Scripts").GetComponent<Player>();
            if (p.cardObjects.Count > 0)
            {

                float margin = 35;
                float cardWidth = GameObject.Find("Player Deck").transform.GetChild(0).GetComponent<RectTransform>().rect.width;
                float lengthOfLine = 0;

                foreach (GameObject obj in p.cardObjects)
                {
                    lengthOfLine += cardWidth - margin;
                }
                lengthOfLine += margin;

                //float totalAngle = 30;
                //float angle = totalAngle / p.cardObjects.Count;
                //float startAngle = -totalAngle / 2;

                float startPosX = -lengthOfLine / 2;
                for (int i = 0; i < p.cardObjects.Count; i++)
                {
                    float x = startPosX + i * (cardWidth - margin);
                    p.cardObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + cardWidth / 2, 0);
                    //p.cardObjects[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -1 * (startAngle + angle*i));
                }
            }
        }
        else
        {
            Debug.Log("Fel i kod i metod ReloadCards(int)");
        }

    }

    /// <summary>
    /// Uppdaterar karaktärer på spelplan för spelare eller fiende
    /// </summary>
    /// <param name="side">Sidan där det ska uppdateras (0 = fiende, 1 = spelare)</param>
    public void ReloadMercenaries(int side)
    {

        if (side == 0)
        {

            Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();

            float margin = 10;
            if(GameObject.Find("Enemy Board").transform.childCount > 0)
            {
                float width = GameObject.Find("Enemy Board").transform.GetChild(0).GetComponent<RectTransform>().rect.width;
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
            }

        }
        else if (side == 1)
        {
            Player p = GameObject.Find("Scripts").GetComponent<Player>();

            float margin = 10;
            if(GameObject.Find("Player Board").transform.childCount > 0)
            {
                float width = GameObject.Find("Player Board").transform.GetChild(0).GetComponent<RectTransform>().rect.width;
                float lengthOfLine = 0;

                foreach (GameObject obj in p.mercenaries)
                {
                    lengthOfLine += width + margin;
                }
                lengthOfLine -= margin;

                float startPosX = -lengthOfLine / 2;
                for (int i = 0; i < p.mercenaries.Count; i++)
                {
                    float x = startPosX + i * (width + margin);
                    p.mercenaries[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + width / 2, 0);
                }
            }
        }
        else
        {
            Debug.Log("Fel i kod i metod ReloadCards(int)");
        }

    }

    /// <summary>
    /// Metod kallad när knappen "End Turn Button" är klickad
    /// </summary>
    public void EndTurn()
    {

        GameObject playerBoard = GameObject.Find("Player Board");
        GameObject enemyBoard = GameObject.Find("Enemy Board");

        foreach (Transform mercenary in playerBoard.transform)
        {
            mercenary.GetComponent<OnDragEvents>().draggable = false; 
        }
        GameObject.Find("Player Hero").GetComponent<OnDragEvents>().draggable = false;

        GameObject.Find("Player HeroPower").GetComponent<OnClickEvents>().clickable = false;

        try
        {
            GameObject[] borders = GameObject.FindGameObjectsWithTag("Green Border");
            foreach (GameObject border in borders)
            {
                border.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        } catch(MissingComponentException mce) { }

        playerTurn = false;

        GameObject heroPowerObj = GameObject.Find("Player HeroPower");
        heroPowerObj.GetComponent<OnClickEvents>().clickable = false;
        heroPowerObj.transform.GetChild(0).GetComponent<Image>().enabled = false; 
        heroPowerObj.transform.GetChild(1).GetComponent<Image>().enabled = false;

        GameObject heroPowerObj2 = GameObject.Find("Enemy HeroPower");
        heroPowerObj2.transform.GetChild(0).GetComponent<Image>().enabled = true;
        heroPowerObj2.transform.GetChild(1).GetComponent<Image>().enabled = true;

        GameObject endTurnButton = GameObject.Find("Button");
        endTurnButton.transform.GetChild(0).GetComponent<Text>().text = "ENEMY TURN";
        endTurnButton.transform.GetChild(0).GetComponent<Text>().fontSize = 12;
        ColorBlock cb = endTurnButton.GetComponent<Button>().colors;
        cb.normalColor = buttonNotClickable;
        endTurnButton.GetComponent<Button>().colors = cb;
        endTurnButton.GetComponent<Button>().interactable = false;

        Enemy e = GameObject.Find("Scripts").GetComponent<Enemy>();
        List<GameObject> mercenaries = e.mercenaries;
        if (mercenaries.Count > 0)
        {

            foreach(Transform mercenary in enemyBoard.transform)
            {
                int enemyIndex = 0;
                if (mercenaries.Count > 1)
                    enemyIndex = mercenary.GetSiblingIndex();

                if(playerBoard.transform.childCount > 0)
                {
                    int playerIndex = 0;

                    if (playerBoard.transform.childCount > 1)
                    {
                        int tauntIndex = 0;
                        bool taunt = false; 
                        foreach(Transform playerMercenary in playerBoard.transform)
                        {
                            Card card = Resources.Load<Card>("Cards/" + playerMercenary.gameObject.name);
                            if(card.description.Length >= 5)
                            {
                                if(card.description.Contains("Taunt"))
                                {
                                    taunt = true;
                                    if(playerBoard.transform.childCount > 1)
                                    {
                                        tauntIndex = playerMercenary.GetSiblingIndex();
                                    }
                                    break; 
                                }
                            }
                        }

                        if(taunt)
                        {
                            playerIndex = tauntIndex;
                        }
                        else
                        {
                            playerIndex = Random.Range(0, playerBoard.transform.childCount - 1);
                        }
                    }

                    e.Attack(enemyIndex, playerIndex);
                }
                else
                {
                    e.Attack(enemyIndex, -1);
                }

            }

        }

        if (e.cardObjects.Count > 0 && e.mercenaries.Count < 6)
        {

            int index = 0;
            if(e.cardObjects.Count > 1)
            {
                index = Random.Range(0, e.cardObjects.Count-1);
            }

            e.UseCard(index);
            //ImportMercenary(e.cardObjects[index].name, 0);

            //e.cardObjects.RemoveAt(index);
            ReloadCards(0);

        }

        //reloadar. nu är det spelarens tur igen

        if(maxMana < 10)
            maxMana++;

        foreach (Transform mana in GameObject.Find("Mana Bar").transform)
        {
            if(mana.GetSiblingIndex() < maxMana)
            {
                mana.GetComponent<Image>().color = manaDark;
                mana.GetComponent<Image>().enabled = true;
            }
            else
            {
                mana.GetComponent<Image>().enabled = false;
            }
        }
        GameObject.Find("Mana Text").GetComponent<Text>().text = "0/" + maxMana;

        foreach (Transform mercenary in GameObject.Find("Player Board").transform)
        {
            mercenary.GetComponent<OnDragEvents>().draggable = true;
        }
        GameObject.Find("Player Hero").GetComponent<OnDragEvents>().draggable = true; 
        cb.normalColor = buttonClickable;
        endTurnButton.GetComponent<Button>().colors = cb;
        endTurnButton.GetComponent<Button>().interactable = true;
        endTurnButton.transform.GetChild(0).GetComponent<Text>().text = "END TURN";
        endTurnButton.transform.GetChild(0).GetComponent<Text>().fontSize = 14;

        heroPowerObj.GetComponent<OnClickEvents>().clickable = true;
        heroPowerObj.transform.GetChild(0).GetComponent<Image>().enabled = true;
        heroPowerObj.transform.GetChild(1).GetComponent<Image>().enabled = true;

        heroPowerObj2.transform.GetChild(0).GetComponent<Image>().enabled = false;
        heroPowerObj2.transform.GetChild(1).GetComponent<Image>().enabled = false;

        ReloadBorders();

        playerTurn = true;

    }

    public void ReloadBorders()
    {

        int manaLeft = maxMana - (GameObject.Find("Mana Text").GetComponent<Text>().text[0] - 48); //ascii
        foreach(GameObject border in GameObject.FindGameObjectsWithTag("Green Border"))
        {
            if(border.layer == LayerMask.NameToLayer("UI"))
            {
                int requiredMana = 0;
                bool choseable = true; 
                GameObject parentObject = border.transform.parent.gameObject; 
                if(parentObject.name == "Player HeroPower")
                {
                    requiredMana = 2;
                    if (parentObject.GetComponent<OnClickEvents>().clickable == false)
                    {
                        choseable = false;
                    }
                }
                else if (parentObject.tag == "Mercenary")
                {
                    requiredMana = 0; 
                    if(parentObject.GetComponent<OnDragEvents>().draggable == false)
                    {
                        choseable = false; 
                    }
                }
                else if(parentObject.tag == "Card")
                {
                    try
                    {
                        requiredMana = int.Parse(parentObject.transform.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text);
                    } catch(System.FormatException fe) { }
                }
                else
                {
                    print(parentObject.name);
                    print("Något gick fel i metoden ReloadBounds() i Game.cs");
                    return; 
                }

                if(requiredMana <= manaLeft && choseable)
                {
                    parentObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true; 
                }
                else
                {
                    parentObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
                }
            }
        }

    }


}
