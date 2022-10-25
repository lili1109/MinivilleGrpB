using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public string nom;
    public int prix;
    public int gain;

    public Cards(string Nom, int Prix, int Effet)
    {
        nom = Nom;
        prix = Prix;
        gain = Effet;
    }

    public override string ToString()
    {
        string toString = string.Format("");
        return toString;
    }

    public int effet()
    {
        return gain;
    }
}
