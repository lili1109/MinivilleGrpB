using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Immeuble83 : MonoBehaviour
{
    public int de = 1;
    public int gain = 1;
    public int prix = 1;
    public string nom = "L'immeuble 83";
    [SerializeField]
    Sprite sprCarte;
   
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
