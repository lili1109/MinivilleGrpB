using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int de;
    public int gain;
    public int prix;
    public string color;
    public string nom;
    public Sprite sprCarte;
    public GameObject Carte;

    public override string ToString()
    {
        string toString = string.Format("[" + this.de + "] " + this.nom + ", prix : " + this.prix + ", gain : " + this.gain + "\n");
        return toString;
    }

    public int effet()
    {
        return this.gain;
    }
}
