using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pile : MonoBehaviour
{
    [Header("Variable Piles")]
    public int nbCartes;
    [SerializeField] GameObject game, joueur1,joueur2;
    int prix;
    public bool passer;

    void Awake()
    { 
        if (passer == false) // Affichages des cartes de la pioche
        {
            GetComponent<Image>().sprite = GetComponent<Card>().sprCarte;
            prix = GetComponent<Card>().prix;
        }
    }

    void Update()
    {
        //Verifie si le joueur peux prendre une carte de la pile ou si il reste des cartes dans la pile
        if(nbCartes == 0 || prix > joueur1.GetComponent<Player>().argent || game.GetComponent<Game>().piles ==false)
        {
            GetComponent<Button>().interactable = false; //desactivation du paquet 
        }
        else
        {
            GetComponent<Button>().interactable = true; //activation du paquet
        }
    }

    public void Onclick() // si le joueur veux acheter un carte de la pile
    {
        joueur1.GetComponent<Player>().mainJoueur.Add(GetComponent<Card>().Carte);
        joueur1.GetComponent<Player>().AfficherMain(GetComponent<Card>().Carte);
        nbCartes--;
        joueur1.GetComponent<Player>().argent -= prix;
        game.GetComponent<Game>().piles = false;
        game.GetComponent<Game>().tourDeOrdi();

    }
    public void OnclickEnnemi() // si l'ordi veux acheter un carte de la pile
    {
        joueur2.GetComponent<Player>().mainJoueur.Add(GetComponent<Card>().Carte);
        joueur2.GetComponent<Player>().AfficherMain(GetComponent<Card>().Carte);
        nbCartes--;
        joueur2.GetComponent<Player>().argent -= prix;
        
        game.GetComponent<Game>().de.GetComponent<Dice>().activeDes();

    }
    public void Passer() //Si le joueur veut rien acheter
    {
        game.GetComponent<Game>().piles = false;
        game.GetComponent<Game>().tourDeOrdi();
    }
}
