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
    [SerializeField]
    GameObject content;
    public List<GameObject> mainJoueur;

    void Awake()
    {
        nbCartes = 2;
        argent = 3;
        AfficherMain();
       
    }
    void Start()
    {
       
    }

    void Update()
    {
        pieces.GetComponent<TMP_Text>().text = argent.ToString();
    }

    void AfficherMain()
    {
        int k = 0;
        foreach (var carte in mainJoueur)
        {
            Vector3 vect = new Vector3(170.0f * k + 50.0f, -130.0f, 0.0f);
            Instantiate(carte, vect, Quaternion.identity, content.transform);
            k++;
        }
    }
   
}
