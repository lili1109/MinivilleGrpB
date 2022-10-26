using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public string nom;
    public int nbCartes;
    public int argent;
    public bool joue;
    public bool piles;
    [SerializeField]
    GameObject pieces;
    [SerializeField]
    GameObject de;
    [SerializeField]
    GameObject content;
    public List<GameObject> mainJoueur;
    int k = 0;
    int scoreDe =0;
    public GameObject ennemi;
    void Awake()
    {
        nbCartes = 2;
        argent = 3;
        foreach (var carte in mainJoueur)
        {
            AfficherMain(carte);
        }
    }
    void Start()
    {
       
    }

    void Update()
    {
        pieces.GetComponent<TMP_Text>().text = argent.ToString();
    }

    public void AfficherMain(GameObject carte)
    {
        Vector3 vect = new Vector3(170.0f * k + 50.0f, -130.0f, 0.0f);
        Instantiate(carte, vect, Quaternion.identity, content.transform);
        k++;
    }

    public void tourJoueur()
    {
        scoreDe = de.GetComponent<Dice>().score;
        foreach(var carte in mainJoueur)
        {
            if((carte.GetComponent<Card>().color == "B"|| carte.GetComponent<Card>().color == "V") && carte.GetComponent<Card>().de == scoreDe)
            {
                argent += carte.GetComponent<Card>().gain;
            }
        }
        foreach (var carte in ennemi.GetComponent<Player>().mainJoueur)
        {
            if(carte.GetComponent<Card>().color == "R" && carte.GetComponent<Card>().de == scoreDe)
            {
                argent -= carte.GetComponent<Card>().gain;
                ennemi.GetComponent<Player>().argent += carte.GetComponent<Card>().gain;
            }
        }
        piles = true;
    }
   
}
