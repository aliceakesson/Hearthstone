using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{

    public List<GameObject> cardObjects = new List<GameObject>();
    public List<GameObject> mercenaries = new List<GameObject>();

    public int health;
    public int attack;
    public int armor;
    public int heroPowerMana;


    /// <summary>
    /// Metod där spelaren eller fienden attackerar motsatta sidan
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    public virtual void Attack(int index1, int index2) { }

    /// <summary>
    /// Metod som drar ett nytt kort till handen
    /// </summary>
    public virtual void DrawCard() { }

    public virtual void UseHeroPower(string heroPowerName)
    {

    }

    public virtual void DealDamage(int index, int dmg) { }

}
