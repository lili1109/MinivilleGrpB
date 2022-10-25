using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards 
{
    public int de;
    public string nom;
    public string color;
    public int gain;
    public int prix;



    public Cards(int De,string Color,string Nom, int Gain, int Prix)
    {
        this.de = De;
        this.color = Color;
        this.nom = Nom;
        this.prix = Prix;
        this.gain = Gain;
    }

    public override string ToString()
    {
        string toString = string.Format("["+this.de+"] "+this.color+" "+this.nom + ", prix : " + this.prix + ", gain : " + this.gain + "\n");
        return toString;
    }

    public int effet()
    {
        return this.gain;
    }
}

