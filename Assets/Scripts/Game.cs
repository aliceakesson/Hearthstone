using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// "Main"-klassen, här implementeras all gameplay
/// </summary>
public class Game : MonoBehaviour
{

    readonly string cardsURL = "Cards/";
    readonly string cardsImagesURL = "Images/Cards/";
    readonly string cardsFramesURL = "Images/Cards/Frames/";
    readonly string belweFontsURL = "Fonts/belwe/";

    readonly string usedFont = "belwe bold bt";

    protected bool playerTurn = false;
    bool prevPlayerTurn = false;
    protected bool gameIsFinished = false;
    public Color buttonNotClickable;
    public Color buttonClickable;



    void Start()
    {
        print("Start");

        ImportCard("Bloodfen_Raptor");
        ImportCard("River_Crocolisk");
        ImportCard("River_Crocolisk");

        playerTurn = true;

    }

    void Update()
    {

        if (prevPlayerTurn == false && playerTurn == true)
        {
            GameObject endTurnButton = GameObject.Find("End Turn Button");
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
    void ImportCard(string cardName)
    {

        Card card = Resources.Load<Card>(cardsURL + cardName);

        int health = card.health;
        int attack = card.attack;
        int mana = card.mana;

        string name = card.name;
        string description = card.description;

        Sprite image = card.image;
        CardType cardType = card.cardType;

        Sprite manaSprite = Resources.Load<Sprite>(cardsImagesURL + "Mana");
        Sprite healthSprite = Resources.Load<Sprite>(cardsImagesURL + "Health");
        Sprite attackSprite = Resources.Load<Sprite>(cardsImagesURL + "Attack");

        Sprite frame;
        string imageName = "";

        if (cardType == CardType.Minion)
            imageName = "Frame-minion-neutral";
        else //ändra sen för att anpassa till typ av kort
            imageName = "Frame-minion-neutral";

        frame = Resources.Load<Sprite>(cardsFramesURL + imageName);

        GameObject deck = GameObject.Find("Player Deck");
        GameObject cardObject = new GameObject(cardName, typeof(RectTransform));
        cardObject.layer = LayerMask.NameToLayer("UI");
        cardObject.transform.parent = deck.transform;
        cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        cardObject.tag = "Card";
        cardObject.AddComponent<OnClick>();

        #region Skapande av card UI
        GameObject mask = new GameObject("Image", typeof(RectTransform));
        mask.transform.parent = cardObject.transform;
        mask.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);
        mask.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        mask.AddComponent<Image>();
        mask.GetComponent<Image>().sprite = Resources.Load<Sprite>(cardsImagesURL + imageName + "-mask");
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
        descriptionText.transform.parent = frameObject.transform;
        descriptionText.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 18);
        descriptionText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -25);
        descriptionText.AddComponent<Text>();
        descriptionText.GetComponent<Text>().font = Resources.Load<Font>(belweFontsURL + usedFont);
        descriptionText.GetComponent<Text>().fontSize = 8;
        descriptionText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
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
        healthText.GetComponent<Text>().maskable = false;
        healthText.layer = LayerMask.NameToLayer("UI");
        #endregion

        cardObject.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 120);

        Player p = GameObject.Find("Scripts").GetComponent<Player>();
        p.cardObjects.Add(cardObject);

        float margin = 35;
        float cardWidth = cardObject.GetComponent<RectTransform>().rect.width;
        float lengthOfLine = 0; 

        foreach(GameObject obj in p.cardObjects)
        {
            lengthOfLine += cardWidth - margin; 
        }
        lengthOfLine += margin; 

        float startPosX = -lengthOfLine / 2;
        for(int i = 0; i < p.cardObjects.Count; i++)
        {
            float x = startPosX + i * (cardWidth-margin);
            p.cardObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + cardWidth/2, 0);
        }

    }

    /// <summary>
    /// Importerar en mercenary (spelbar karaktär) till antingen spelaren eller fiendens del av spelplanen
    /// </summary>
    /// <param name="cardName">Namnet på kortet att importera</param>
    public void ImportMercenary(string cardName)
    {

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
            imageName = "Mercenary_Minion";
        }
        else //ändra senare
        {
            imageName = "Mercenary_Minion";
        }

        frame = Resources.Load<Sprite>(cardsFramesURL + imageName);

        GameObject mercObject = new GameObject(cardName, typeof(RectTransform));
        GameObject example = GameObject.Find("Mercenary Example");

        mercObject.transform.parent = GameObject.Find("Player Board").transform;
        mercObject.layer = LayerMask.NameToLayer("UI");

        RectTransform rt = example.GetComponent<RectTransform>();
        mercObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        mercObject.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        mercObject.tag = "Mercenary";

        #region Skapande av mercenary UI
        GameObject frame1 = new GameObject("Frame", typeof(RectTransform));
        rt = example.transform.GetChild(0).GetComponent<RectTransform>();
        frame1.transform.parent = mercObject.transform;
        frame1.layer = LayerMask.NameToLayer("UI");
        frame1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        frame1.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        frame1.AddComponent<Image>();
        frame1.GetComponent<Image>().sprite = Resources.Load<Sprite>(cardsFramesURL + imageName + "_mask");
        frame1.AddComponent<Mask>();
        frame1.GetComponent<Mask>().showMaskGraphic = false;

        GameObject imageObj = new GameObject("Image", typeof(RectTransform));
        rt = example.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        imageObj.transform.parent = frame1.transform;
        imageObj.layer = LayerMask.NameToLayer("UI");
        imageObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        imageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        imageObj.AddComponent<Image>();
        imageObj.GetComponent<Image>().sprite = image;

        GameObject frame2 = new GameObject("Frame Visible", typeof(RectTransform));
        rt = example.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        frame2.transform.parent = frame1.transform;
        frame2.layer = LayerMask.NameToLayer("UI");
        frame2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        frame2.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        frame2.AddComponent<Image>();
        frame2.GetComponent<Image>().sprite = frame;

        GameObject attackObj = new GameObject("Attack", typeof(RectTransform));
        rt = example.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RectTransform>();
        attackObj.transform.parent = frame2.transform;
        attackObj.layer = LayerMask.NameToLayer("UI");
        attackObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        attackObj.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        attackObj.AddComponent<Image>();
        attackObj.GetComponent<Image>().sprite = attackSprite;
        attackObj.GetComponent<Image>().maskable = false;

        GameObject attackText = new GameObject("Text", typeof(RectTransform));
        rt = example.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();
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
        rt = example.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RectTransform>();
        healthObj.transform.parent = frame2.transform;
        healthObj.layer = LayerMask.NameToLayer("UI");
        healthObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y);
        healthObj.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.rect.width, rt.rect.height);
        healthObj.AddComponent<Image>();
        healthObj.GetComponent<Image>().sprite = healthSprite;
        healthObj.GetComponent<Image>().maskable = false;

        GameObject healthText = new GameObject("Text", typeof(RectTransform));
        rt = example.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<RectTransform>();
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

    /// <summary>
    /// Uppdaterar korthögen för spelare eller fiende
    /// </summary>
    public void ReloadCards()
    {

        if(GameObject.Find("Player Deck").transform.childCount > 0)
        {
            Player p = GameObject.Find("Scripts").GetComponent<Player>();

            float margin = 35;
            float cardWidth = GameObject.Find("Player Deck").transform.GetChild(0).GetComponent<RectTransform>().rect.width;
            float lengthOfLine = 0;

            foreach (GameObject obj in p.cardObjects)
            {
                lengthOfLine += cardWidth - margin;
            }
            lengthOfLine += margin;

            float startPosX = -lengthOfLine / 2;
            for (int i = 0; i < p.cardObjects.Count; i++)
            {
                float x = startPosX + i * (cardWidth - margin);
                p.cardObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x + cardWidth / 2, 0);
            }
        }

    }

    /// <summary>
    /// Metod kallad när knappen "End Turn Button" är klickad
    /// </summary>
    public void EndTurn()
    {

        GameObject endTurnButton = GameObject.Find("End Turn Button");
        ColorBlock cb = endTurnButton.GetComponent<Button>().colors;
        cb.normalColor = buttonNotClickable;
        endTurnButton.GetComponent<Button>().colors = cb;
        endTurnButton.GetComponent<Button>().interactable = false;

    }
}
