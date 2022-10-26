using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pile : MonoBehaviour
{

    public int nbCartes;
    public GameObject mainJoueur;
    int prix;
    public bool passer;
    public Pile()
    {
        nbCartes = 6;       
    }

    void Awake()
    { if (passer == false)
        {
            GetComponent<Image>().sprite = GetComponent<Card>().sprCarte;
            prix = GetComponent<Card>().prix;
        }
    }

    public override string ToString()
    {
        string toString = string.Format("");
        return toString;
    }

    void Update()
    {
        if(nbCartes == 0 || prix > mainJoueur.GetComponent<Player>().argent || mainJoueur.GetComponent<Player>().piles ==false)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void Onclick()
    {
        mainJoueur.GetComponent<Player>().mainJoueur.Add(GetComponent<Card>().Carte);
        mainJoueur.GetComponent<Player>().AfficherMain(GetComponent<Card>().Carte);
        nbCartes--;
        mainJoueur.GetComponent<Player>().argent -= prix;
        mainJoueur.GetComponent<Player>().piles = false;

    }
    public void Passer()
    {
        mainJoueur.GetComponent<Player>().piles = false;
    }

}












/*for(int i = 0 ; i < nbCartes; i++)
        {
            cartes.Add(carte);
        }*/
/*for (int i = 0; i < 6; i++)
{
    cartes.Add(new Cards(1, "B","Immeuble 83", 1, 1));
    cartes.Add(new Cards(1, "B", "Epicerie solidaire", 1, 2));
    cartes.Add(new Cards(2, "V", "Musée du Papier", 2, 1));
    cartes.Add(new Cards(3, "R", "Bock n'Roll", 1, 2));
    cartes.Add(new Cards(4, "V", "Musée de la BD", 3, 2));
    cartes.Add(new Cards(5, "B", "Jardin Vert", 1, 2));
    cartes.Add(new Cards(5, "R", "RestoU", 2, 4));
    cartes.Add(new Cards(6, "B", "Cnam Enjmin", 5, 7));
    cartes.Add(new Cards(7, "B", "Cosmopolite", 2, 3));
    cartes.Add(new Cards(7, "B", "CGR", 1, 3));
    cartes.Add(new Cards(8, "V", "Patinoire", 2, 2));
    cartes.Add(new Cards(9, "R", "Hôtel de ville", 1, 3));
    cartes.Add(new Cards(10, "V", "Gare SNCF", 3, 4));
    cartes.Add(new Cards(11, "B", "Bare Le Nil", 1, 2));
    cartes.Add(new Cards(11, "R", "E.Leclerc", 2, 4));
    cartes.Add(new Cards(12, "B", "Bord de la Charente", 4, 6));

}*/

//Debug
// Debug.Log(cartes.Count);
/*  foreach (Cards c in cartes)
  {
      Debug.Log(c);
  }*/