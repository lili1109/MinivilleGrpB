using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public string nom;
    public int prix;
    public int effet;

    public Cards(string Nom, int Prix, int Effet)
    {
        nom = Nom;
        prix = Prix;
        effet = Effet;
    }

    public override string ToString()
    {
        string toString = string.Format("");
        return toString;
    }
}
