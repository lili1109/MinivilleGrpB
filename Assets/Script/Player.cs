using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [Header("Variable Player")]
    public int nbCartes, argent;
    int k = 0;
    [Header("GameObject")]
    [SerializeField] GameObject pieces, content;
   
    [Header("Main")]
    public List<GameObject> mainJoueur;
    
    void Awake()
    {
        
        nbCartes = 2; //nb de cartes par mains
        argent = 3; 

        //Affiche la main de chaque joueur
        foreach (var carte in mainJoueur)
        {
            AfficherMain(carte);
        }
    }

    void Update()
    {
        //MAJ nom de GomyCoins
        pieces.GetComponent<TMP_Text>().text = argent.ToString();
    }

    public void AfficherMain(GameObject carte) //Affiche une nouvelle carte dans la main
    {
        Vector3 vect = new Vector3(170.0f * k + 50.0f, -130.0f, 0.0f); // position de la carte
        Instantiate(carte, vect, Quaternion.identity, content.transform); //creation nouvelle carte
        k++;
    }  
}