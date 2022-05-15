using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fungerar som huvudklass för spelaren och fienden. En klass för "personer"
/// </summary>
public class Humanoid : MonoBehaviour
{

    public List<GameObject> cardObjects = new List<GameObject>();
    public List<GameObject> mercenaries = new List<GameObject>();

    public int health;
    public int attack;
    public int armor;
    public int heroPowerMana;

    /// <summary>
    /// Konstruktor för Humanoid
    /// </summary>
    public Humanoid()
    {

    }

    /// <summary>
    /// Metod där spelaren eller fienden attackerar motsatta sidan
    /// </summary>
    /// <param name="index1">Index på den som skadar (positivt för mercenaries, negativt för hero)</param>
    /// <param name="index2">Index på den som blir skadad (positivt för mercenaries, negativt för hero)</param>
    public virtual void Attack(int index1, int index2) { }

    /// <summary>
    /// Metod som drar ett nytt kort till handen
    /// </summary>
    public virtual void DrawCard() { }

    /// <summary>
    /// Metod för att använda ens "Hero Power"
    /// </summary>
    /// <param name="heroPowerName">Namnet på förmågan</param>
    public virtual void UseHeroPower(string heroPowerName)
    {

    }

    /// <summary>
    /// Metod för att skada någon utan att själv bli skadad
    /// </summary>
    /// <param name="index">Index för den mercenary (eller hero) som ska bli skadad</param>
    /// <param name="dmg">Antal Health som förloras</param>
    public virtual void DealDamage(int index, int dmg) { }

}
