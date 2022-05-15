using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public MenuButtonEvents()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();

        float multiplier = 1 / 255f;
        float dark1 = 70 * multiplier, dark2 = 170 * multiplier;

        Color tl = new Color(dark1, dark1, dark1);
        Color br = new Color(dark1, dark1, dark1);
        Color tr = new Color(dark2, dark2, dark2);
        Color bl = new Color(dark2, dark2, dark2);

        tmp.colorGradient = new TMPro.VertexGradient(tl, tr, bl, br);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();

        float multiplier = 1 / 255f;
        float light1 = 136 * multiplier, light2 = 255 * multiplier;

        Color tl = new Color(light1, light1, light1);
        Color br = new Color(light1, light1, light1);
        Color tr = new Color(light2, light2, light2);
        Color bl = new Color(light2, light2, light2);

        tmp.colorGradient = new TMPro.VertexGradient(tl, tr, bl, br);
    }

    public virtual void PlayGame()
    {
        SwitchScene(1);
    }

    protected void SwitchScene(int sceneIndex)
    {
        switch(sceneIndex)
        {
            case 1:
                AsyncOperation operation = SceneManager.LoadSceneAsync(1);
                break;
            case 2:
                List<string> cardDeck = Resources.Load<PublicData>("PublicData").cardDeck;
                if (cardDeck.Count > 0)
                {
                    List<string> cardDeckCopy = new List<string>();
                    foreach (string card in cardDeck)
                    {
                        cardDeckCopy.Add(card);
                    }
                    foreach (string card in cardDeckCopy)
                    {
                        cardDeck.Remove(card);
                    }
                }

                if (GameObject.Find("Scripts").GetComponent<SelectDeck>().cardsChosen == 30)
                {

                    bool canPlayGame = true;

                    GameObject cards = GameObject.Find("Cards").gameObject;
                    if (cards.transform.childCount != 30)
                    {
                        canPlayGame = false;
                    }
                    else
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            string name = cards.transform.GetChild(i).name;
                            cardDeck.Add(name);
                        }
                    }

                    if (canPlayGame)
                    {
                        operation = SceneManager.LoadSceneAsync(2);
                    }
                }
            break;
                default:
                    break; 
        }
    }
}
