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
    [SerializeField]
    GameObject pieces;
    public List<GameObject> mainJoueur;

    void Awake()
    {
        nbCartes = 2;
        argent = 3;
        foreach(var carte in mainJoueur)
        {
            
        }
       
    }
    void Start()
    {
       
    }

    void Update()
    {

        pieces.GetComponent<TMP_Text>().text = argent.ToString();
    }
   
}
