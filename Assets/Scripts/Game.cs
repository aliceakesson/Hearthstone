using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        //ImportCard("Bloodfen_Raptor");
        ImportCard("River_Crocolisk");

        playerTurn = true; 

    }

    void Update()
    {

        if(prevPlayerTurn == false && playerTurn == true)
        {
            GameObject endTurnButton = GameObject.Find("End Turn Button");
            ColorBlock cb = endTurnButton.GetComponent<Button>().colors;
            cb.normalColor = buttonClickable;
            endTurnButton.GetComponent<Button>().colors = cb;
            endTurnButton.GetComponent<Button>().interactable = true;
        }
        prevPlayerTurn = playerTurn;

    }

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

        GameObject deck = GameObject.Find("Deck");
        GameObject cardObject = new GameObject("Card", typeof(RectTransform));
        cardObject.layer = LayerMask.NameToLayer("UI");
        cardObject.transform.parent = deck.transform;
        cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        cardObject.tag = "Card";

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

        GameObject attackObject = new GameObject("attack", typeof(RectTransform));
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

    }

    public void EndTurn()
    {

        GameObject endTurnButton = GameObject.Find("End Turn Button");
        ColorBlock cb = endTurnButton.GetComponent<Button>().colors;
        cb.normalColor = buttonNotClickable;
        endTurnButton.GetComponent<Button>().colors = cb;
        endTurnButton.GetComponent<Button>().interactable = false; 
        
    }
}
