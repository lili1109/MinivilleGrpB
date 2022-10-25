using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public int de;
    public string nom;
    public string color;
    public int gain;
    public int prix;



    public Cards(int De,string Color,string Nom, int Gain, int Prix)
    {
        de = De;
        color = Color;
        nom = Nom;
        prix = Prix;
        gain = Gain;
    }

    public override string ToString()
    {
        string toString = string.Format("["+de+"] "+color+" "+nom + ", prix : " + prix + ", gain : " + gain + "\n");
        return toString;
    }

    public int effet()
    {
        return gain;
    }
}

