using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{

    public List<Card> cards = new List<Card>();

    public List<GameObject> cardObjects = new List<GameObject>();
    public List<GameObject> mercenaries = new List<GameObject>();

    public int health;
    public int armor;
    public int heroPowerMana; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void DrawCard()
    {

    }
}
