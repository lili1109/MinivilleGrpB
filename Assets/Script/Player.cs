using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public string nom;
    public int nbCartes, argent;
    private int changementSolde;
    public bool joue, piles, bEnnemi;
    [SerializeField] GameObject pieces, content, updateScore, updateScoreEnnemy;
    public GameObject de;
    public List<GameObject> mainJoueur;
    int k = 0;
    int scoreDe = 0;
    int gainCarte = 0;
    int gainTotalMancheJoueur = 0;
    int gainTotalMancheEnnemi = 0;
    public GameObject ennemi;
    public List<GameObject> cartesPiles; 
    void Awake()
    {
        joue = true;
        bEnnemi = false;
        nbCartes = 2;
        argent = 3;

        foreach (var carte in mainJoueur)
        {
            AfficherMain(carte);
        }
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

    public IEnumerator TempsAffichageJoueur()
    {
        updateScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        updateScore.gameObject.SetActive(false);
    }

    public IEnumerator TempsAffichageEnnemi()
    {
        updateScoreEnnemy.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        updateScoreEnnemy.gameObject.SetActive(false);
    }

    public void tourJoueur()
    {
        scoreDe = de.GetComponent<Dice>().score;
        gainTotalMancheJoueur = 0;
        gainTotalMancheEnnemi = 0;
        foreach (var carte in mainJoueur)
        {
            string colorCarte = carte.GetComponent<Card>().color;
            if ((colorCarte == "B" || colorCarte == "V") && carte.GetComponent<Card>().de == scoreDe)
            {
                gainCarte = carte.GetComponent<Card>().gain;
                argent += gainCarte;
                gainTotalMancheJoueur += gainCarte;
                Debug.Log(gainCarte);
            }
        }
        foreach (var carte in ennemi.GetComponent<Player>().mainJoueur)
        {
            string colorCarte = carte.GetComponent<Card>().color;
            int carteGain = carte.GetComponent<Card>().gain;
            int deCarte = carte.GetComponent<Card>().de;
            if (colorCarte == "R" && deCarte == scoreDe)
            {
                gainCarte = carteGain;
                argent -= gainCarte;
                gainTotalMancheJoueur -= gainCarte;
                gainTotalMancheEnnemi += gainCarte;
                ennemi.GetComponent<Player>().argent += gainCarte;
                updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                Debug.Log(gainCarte);
            }
            else if (colorCarte == "B" && deCarte == scoreDe)
            {
                gainCarte = carteGain;
                ennemi.GetComponent<Player>().argent += gainCarte;
                gainTotalMancheEnnemi += gainCarte;
                updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                Debug.Log(gainCarte);
            }
        }
        if (gainTotalMancheEnnemi > 0)
        {
            updateScoreEnnemy.GetComponent<TMP_Text>().text = "+" + gainTotalMancheEnnemi.ToString();
            StartCoroutine("TempsAffichageEnnemi");
        }
        else if (gainTotalMancheEnnemi < 0)
        {
            updateScoreEnnemy.GetComponent<TMP_Text>().text = gainTotalMancheEnnemi.ToString();
            StartCoroutine("TempsAffichageEnnemi");
        }
        if (gainTotalMancheJoueur > 0)
        {
            updateScore.GetComponent<TMP_Text>().text = "+" + gainTotalMancheJoueur.ToString();
            StartCoroutine("TempsAffichageJoueur");
        }
        else if (gainTotalMancheJoueur < 0)
        {
            updateScore.GetComponent<TMP_Text>().text = gainTotalMancheJoueur.ToString();
            StartCoroutine("TempsAffichageJoueur");
        }
        piles = true;
    }

    public void tourDeEnnemi()
    {
        bEnnemi = true;
        bool de2 = false;
        foreach (var carte in ennemi.GetComponent<Player>().mainJoueur)
        {
            if (carte.GetComponent<Card>().de > 6)
            {
                int n = Random.Range(0, 2);
                if (n == 0)
                {
                    de2 = true;
                }
                else
                {
                    de2 = false;
                }
            }
            else
            {
                de2 = false;
            }
        }

        if (!de2)
        {
            de.GetComponent<Dice>().Throw();
            Debug.Log("Throw");
        }
        else
        {
            de.GetComponent<Dice>().Throw2();
            Debug.Log("Throw2");
        }

    }


    public void tourEnnemi()
    {
        Debug.Log("tourEnnemi");
        scoreDe = de.GetComponent<Dice>().score;
        gainTotalMancheJoueur = 0;
        gainTotalMancheEnnemi = 0;

        foreach (var carte in ennemi.GetComponent<Player>().mainJoueur)
        {
            string colorCarte = carte.GetComponent<Card>().color;
            if ((colorCarte == "B" || colorCarte == "V") && carte.GetComponent<Card>().de == scoreDe)
            {
                Debug.Log("carte bleu ou verte");
                gainCarte = carte.GetComponent<Card>().gain;
                gainTotalMancheEnnemi += gainCarte;
                ennemi.GetComponent<Player>().argent += gainCarte;
                Debug.Log(gainCarte);
            }
        }
        foreach (var carte in mainJoueur)
        {
            string colorCarte = carte.GetComponent<Card>().color;
            if (colorCarte == "R" && carte.GetComponent<Card>().de == scoreDe)
            {
                gainCarte = carte.GetComponent<Card>().gain;
                Debug.Log(gainCarte);
                argent += gainCarte;
                gainTotalMancheJoueur += gainCarte;
                gainTotalMancheEnnemi -= gainCarte;
                ennemi.GetComponent<Player>().argent -= gainCarte;
                updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
               
            }
            else if (colorCarte == "B" && carte.GetComponent<Card>().de == scoreDe)
            {
                gainCarte = carte.GetComponent<Card>().gain;
                argent += gainCarte;
                gainTotalMancheJoueur += gainCarte;
                updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                Debug.Log(gainCarte);
            }
        }
        List<GameObject> pileValideEnnemi = new List<GameObject>();
        foreach (var piles in cartesPiles)
        {
            if (ennemi.GetComponent<Player>().argent >= piles.GetComponent<Card>().prix && piles.GetComponent<Pile>().nbCartes > 0)
            {
                pileValideEnnemi.Add(piles);
                Debug.Log(piles.GetComponent<Card>().nom +"-- "+piles.GetComponent<Card>().prix);
            }
        }
        if (gainTotalMancheEnnemi > 0)
        {
            updateScoreEnnemy.GetComponent<TMP_Text>().text = "+" + gainTotalMancheEnnemi.ToString();
            StartCoroutine("TempsAffichageEnnemi");
        }
        else if (gainTotalMancheEnnemi < 0)
        {
            updateScoreEnnemy.GetComponent<TMP_Text>().text = gainTotalMancheEnnemi.ToString();
            StartCoroutine("TempsAffichageEnnemi");
        }
        if (gainTotalMancheJoueur > 0)
        {
            updateScore.GetComponent<TMP_Text>().text = "+" + gainTotalMancheJoueur.ToString();
            StartCoroutine("TempsAffichageJoueur");
        }
        else if (gainTotalMancheJoueur < 0)
        {
            updateScore.GetComponent<TMP_Text>().text = gainTotalMancheJoueur.ToString();
            StartCoroutine("TempsAffichageJoueur");
        }

        Debug.Log("pilevalidennemi"+pileValideEnnemi.Count);
        int nb = Random.Range(0, pileValideEnnemi.Count);
        pileValideEnnemi[nb].GetComponent<Pile>().OnclickEnnemi();
    }
    
}
   
    
