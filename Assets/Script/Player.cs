using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public string nom;
    public int nbCartes, argent;
    public bool joue, piles, bEnnemi;
    [SerializeField] GameObject pieces, content, updateScore, updateScoreEnnemy, endGameVictory, endGameDefeat, endGameDraw,endGameCalque, niveau,textEndGame;
    public GameObject de;
    public List<GameObject> mainJoueur;
    int k = 0;
    int scoreDe = 0;
    int gainCarte = 0;
    int gainTotalMancheJoueur = 0;
    int gainTotalMancheEnnemi = 0;
    bool enJeu = true;
    public int nbCoins = 20;
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
        endGameCalque.gameObject.SetActive(false);
        endGameVictory.gameObject.SetActive(false);
        endGameDefeat.gameObject.SetActive(false);
        endGameDraw.gameObject.SetActive(false);
        niveau.gameObject.SetActive(true);
    }

    void Update()
    {
        pieces.GetComponent<TMP_Text>().text = argent.ToString();
        EndGame();
    }

    public void Rapide()
    {
        nbCoins = 10;
        niveau.gameObject.SetActive(false);
    }
    public void Standard()
    {
        nbCoins = 20;
        niveau.gameObject.SetActive(false);
    }
    public void Long()
    {
        nbCoins = 30;
        niveau.gameObject.SetActive(false);
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
        if (enJeu)
        {
            scoreDe = de.GetComponent<Dice>().score;
            gainTotalMancheJoueur = 0;
            gainTotalMancheEnnemi = 0;
            foreach (var carte in mainJoueur)
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                if ((colorCarte == "B" || colorCarte == "V") && card.de == scoreDe)
                {
                    gainCarte = card.gain;
                    argent += gainCarte;
                    gainTotalMancheJoueur += gainCarte;
                }
            }

            foreach (var carte in ennemi.GetComponent<Player>().mainJoueur)
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                int carteGain = card.gain;
                int deCarte = card.de;
                if (colorCarte == "R" && deCarte == scoreDe)
                {
                    int ennemiArgent = ennemi.GetComponent<Player>().argent;

                    if (ennemiArgent >= carteGain)
                    {
                        gainCarte = carteGain;
                        argent -= gainCarte;
                        gainTotalMancheJoueur -= gainCarte;
                        gainTotalMancheEnnemi += gainCarte;
                        ennemi.GetComponent<Player>().argent += gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }
                    else
                    {
                        gainCarte = ennemiArgent;
                        argent -= gainCarte;
                        gainTotalMancheJoueur -= gainCarte;
                        gainTotalMancheEnnemi += gainCarte;
                        ennemi.GetComponent<Player>().argent += gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }
                }
                else if (colorCarte == "B" && deCarte == scoreDe)
                {
                    gainCarte = carteGain;
                    ennemi.GetComponent<Player>().argent += gainCarte;
                    gainTotalMancheEnnemi += gainCarte;
                    updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
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
    }

    public void tourDeEnnemi()
    {
        if (enJeu)
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
                de.GetComponent<Dice>().ThrowEnnemi();
            }
            else
            {
                de.GetComponent<Dice>().Throw2Ennemi();
            }
        }
    }


    public void tourEnnemi()
    {
        if (enJeu)
        {
            Debug.Log("tourEnnemi");
            scoreDe = de.GetComponent<Dice>().score;
            gainTotalMancheJoueur = 0;
            gainTotalMancheEnnemi = 0;

            foreach (var carte in ennemi.GetComponent<Player>().mainJoueur)
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                if ((colorCarte == "B" || colorCarte == "V") && card.de == scoreDe)
                {
                    gainCarte = card.gain;
                    gainTotalMancheEnnemi += gainCarte;
                    ennemi.GetComponent<Player>().argent += gainCarte;
                }
            }
            foreach (var carte in mainJoueur)
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                int carteGain = card.gain;
                if (colorCarte == "R" && card.de == scoreDe)
                {
                    if (argent >= carteGain)
                    {
                        gainCarte = carteGain;
                        argent += gainCarte;
                        gainTotalMancheJoueur += gainCarte;
                        gainTotalMancheEnnemi -= gainCarte;
                        ennemi.GetComponent<Player>().argent -= gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }
                    else
                    {
                        gainCarte = argent;
                        argent += gainCarte;
                        gainTotalMancheJoueur += gainCarte;
                        gainTotalMancheEnnemi -= gainCarte;
                        ennemi.GetComponent<Player>().argent -= gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }

                   

                }
                else if (colorCarte == "B" && card.de == scoreDe)
                {
                    gainCarte = card.gain;
                    argent += gainCarte;
                    gainTotalMancheJoueur += gainCarte;
                    updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                }
            }
            List<GameObject> pileValideEnnemi = new List<GameObject>();
            foreach (var piles in cartesPiles)
            {
                if (ennemi.GetComponent<Player>().argent >= piles.GetComponent<Card>().prix && piles.GetComponent<Pile>().nbCartes > 0)
                {
                    pileValideEnnemi.Add(piles);
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
            if (ennemi.GetComponent<Player>().argent > 0)
            {
                Debug.Log("pilevalidennemi" + pileValideEnnemi.Count);
                int nb = Random.Range(0, pileValideEnnemi.Count);
                pileValideEnnemi[nb].GetComponent<Pile>().OnclickEnnemi();
            }
            de.GetComponent<Dice>().activeDes();
        }
    }
    public void EndGame()
    {
        if (ennemi.GetComponent<Player>().argent < nbCoins && argent >= nbCoins)
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Vous avez gagné car vous avez atteint les "+ nbCoins +" GomyCoins requis !";
            endGameVictory.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;

        }
        else if (ennemi.GetComponent<Player>().argent >= nbCoins && argent < nbCoins)
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Votre adversaire a atteint les " + nbCoins + " Gomycoins nécessaires avant vous, vous avez perdu.";
            endGameDefeat.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;
        }
        else if (ennemi.GetComponent<Player>().argent >= nbCoins && argent >= nbCoins)
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Vous avez tout les deux atteint les " + nbCoins + " Gomycoins nécessaires, c'est une égalité.";
            endGameDraw.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;
        }
    }
}