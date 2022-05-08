using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectDeck : MonoBehaviour
{

    int cardsInMenu = 0; 
    public int cardsChosen = 0;

    Vector2 startPos = new Vector2(-235.3f, 109.9f);

    public List<string> cardDeck = new List<string>();

    private void Start()
    {
        ImportCard("River_Crocolisk");
        ImportCard("River_Crocolisk");
        ImportCard("River_Crocolisk");
        ImportCard("River_Crocolisk");
        ImportCard("River_Crocolisk");
        ImportCard("River_Crocolisk");
        ImportCard("River_Crocolisk");
    }

    void ImportCard(string cardName)
    {

        GameObject example = GameObject.Find("Card Example");

        Card card = Resources.Load<Card>("Cards/" + cardName);

        GameObject cardObject = GameObject.Instantiate(example);
        cardObject.transform.parent = GameObject.Find("Card Options").transform;
        cardObject.name = cardName; 

        float xMargin = 152.9f, yMargin = 193.82f;

        int index = cardsInMenu;
        if(cardsInMenu >= 8)
        {
            index = cardsInMenu % 8; 
        }

        int xIndex = index;
        int yIndex = 0; 

        if(index >= 4)
        {
            xIndex -= 4;
            yIndex = 1;
        }

        cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x + xMargin * xIndex, startPos.y - yMargin * yIndex);

        cardObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = card.image;

        cardObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = card.name;
        cardObject.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = card.description; 
        cardObject.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = card.mana + ""; 
        cardObject.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>().text = card.attack + ""; 
        cardObject.transform.GetChild(0).GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>().text = card.health + "";

        cardObject.AddComponent<CardToDeck>();

        cardsInMenu++;

    }

    public void PlayGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        //if(cardsChosen == 30)
        //{
        //    AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        //}
    }

}
