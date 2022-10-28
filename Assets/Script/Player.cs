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
    public string nom;
    public int nbCartes, argent;
    public bool joue, piles, bEnnemi;
    int k = 0;
    int scoreDe = 0;
    int gainCarte = 0;
    int gainTotalMancheJoueur = 0;
    int gainTotalMancheEnnemi = 0;
    bool enJeu = true;
    public int nbCoins = 20;
    [Header("GameObject")]
    public GameObject de;
    public GameObject ennemi;
    [SerializeField] GameObject pieces, content, updateScore, updateScoreEnnemy, endGameVictory, endGameDefeat, endGameDraw,endGameCalque, niveau,textEndGame;
    public List<GameObject> mainJoueur;
    public List<GameObject> cartesPiles;

   

    void Awake()
    {
        joue = true;
        bEnnemi = false;
        nbCartes = 2; //nb de cartes par mains
        argent = 3; 

        //Affiche la main de chaque joueur
        foreach (var carte in mainJoueur)
        {
            AfficherMain(carte);
        }

        //Affiche la demande de niveau
        niveau.gameObject.SetActive(true);

        //N'affiche pas les calque de fin
        endGameCalque.gameObject.SetActive(false);
        endGameVictory.gameObject.SetActive(false);
        endGameDefeat.gameObject.SetActive(false);
        endGameDraw.gameObject.SetActive(false);
       
    }

    void Update()
    {
        //MAJ nom de GomyCoins
        pieces.GetComponent<TMP_Text>().text = argent.ToString();
        //Verifi si condition de fin
        EndGame();
    }

    public void Rapide() //Partie rapide
    {
        nbCoins = 10;
        niveau.gameObject.SetActive(false);
    }
    public void Standard()//Partie Standard
    {
        nbCoins = 20;
        niveau.gameObject.SetActive(false);
    }
    public void Long()//Partie Longue
    {
        nbCoins = 30;
        niveau.gameObject.SetActive(false);
    }

    public void AfficherMain(GameObject carte) //Affiche une nouvelle carte dans la main
    {
        Vector3 vect = new Vector3(170.0f * k + 50.0f, -130.0f, 0.0f); // position de la carte
        Instantiate(carte, vect, Quaternion.identity, content.transform); //creation nouvelle carte
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

    public void tourJoueur() //tour du joueur
    {
        if (enJeu) // si pas fini
        {
            scoreDe = de.GetComponent<Dice>().score; //recuperation score des des
            gainTotalMancheJoueur = 0;
            gainTotalMancheEnnemi = 0;
            foreach (var carte in mainJoueur) //recheche dans les cartes du joueur si carte Bleue ou Verte
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

            foreach (var carte in ennemi.GetComponent<Player>().mainJoueur) //recheche dans les cartes de l'ordi si carte Bleue ou Rouge
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                int carteGain = card.gain;
                int deCarte = card.de;
                if (colorCarte == "R" && deCarte == scoreDe) //Si carte rouge
                {
                    int ennemiArgent = ennemi.GetComponent<Player>().argent; //recupere l'argent qu'a l'ordi

                    if (argent >= carteGain)// le joueur a assez de piece pour payer l'ordi
                    {
                        gainCarte = carteGain;
                        argent -= gainCarte;
                        gainTotalMancheJoueur -= gainCarte;
                        gainTotalMancheEnnemi += gainCarte;
                        ennemi.GetComponent<Player>().argent += gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }
                    else // sinon il lui donne tous ce qu'il a
                    {
                        gainCarte = ennemiArgent;
                        argent -= gainCarte;
                        gainTotalMancheJoueur -= gainCarte;
                        gainTotalMancheEnnemi += gainCarte;
                        ennemi.GetComponent<Player>().argent += gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }
                }
                else if (colorCarte == "B" && deCarte == scoreDe) // si carte bleu gagne le gain
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
            piles = true;//Active l'acces au piles
        }
    }

    public void tourDeEnnemi() //L'ordi lance les des
    {
        if (enJeu) // si toujours en jeu
        {
            bEnnemi = true;
            bool de2 = false;
            foreach (var carte in ennemi.GetComponent<Player>().mainJoueur) //Si parmis les cartes il y en a qui on besoin de plus de 6 pour s'activer alors choix random entre 2 des ou 1 de
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

            if (!de2)//lance 1 de
            {
                de.GetComponent<Dice>().ThrowEnnemi();
            }
            else //lance 2 des
            {
                de.GetComponent<Dice>().Throw2Ennemi();
            }
        }
    }


    public void tourEnnemi() // tour de l'ordi
    {
        if (enJeu) //si toujours en jeu
        {
            scoreDe = de.GetComponent<Dice>().score; //recuperation score du de
            gainTotalMancheJoueur = 0;
            gainTotalMancheEnnemi = 0;

            foreach (var carte in ennemi.GetComponent<Player>().mainJoueur) // recherche parmis la main de l'ordi si carte Bleu et verte 
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
            foreach (var carte in mainJoueur) // recherche par mis la main du joueur si carte Rouge ou Bleu
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                int carteGain = card.gain;
                if (colorCarte == "R" && card.de == scoreDe) //si carte rouge 
                {
                    if (ennemi.GetComponent<Player>().argent >= carteGain) //verifie si l'ordi a de quoi payer le joueur
                    {
                        gainCarte = carteGain;
                        argent += gainCarte;
                        gainTotalMancheJoueur += gainCarte;
                        gainTotalMancheEnnemi -= gainCarte;
                        ennemi.GetComponent<Player>().argent -= gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }
                    else //sinon lui donne tous ce qu'il a
                    {
                        gainCarte = argent;
                        argent += gainCarte;
                        gainTotalMancheJoueur += gainCarte;
                        gainTotalMancheEnnemi -= gainCarte;
                        ennemi.GetComponent<Player>().argent -= gainCarte;
                        updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    }

                   

                }
                else if (colorCarte == "B" && card.de == scoreDe) // si carte bleu recupere gain de la carte
                {
                    gainCarte = card.gain;
                    argent += gainCarte;
                    gainTotalMancheJoueur += gainCarte;
                    updateScore.GetComponent<TMP_Text>().text = gainCarte.ToString();
                }
            }
            //L'ordi pioche
            List<GameObject> pileValideEnnemi = new List<GameObject>(); //Liste qui va contenir toute les carte que l'ordi peut acheter
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
            //si le joueur a plus de 0 gomy coins il pioche une carte au hasard et la paye
            if (ennemi.GetComponent<Player>().argent > 0)
            {
                int nb = Random.Range(0, pileValideEnnemi.Count);
                pileValideEnnemi[nb].GetComponent<Pile>().OnclickEnnemi();
            }
            de.GetComponent<Dice>().activeDes();
        }
    }
    public void EndGame() //condition de fin du jeu
    {
        if (ennemi.GetComponent<Player>().argent < nbCoins && argent >= nbCoins) // le joueur gagne
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Vous avez gagné car vous avez atteint les "+ nbCoins +" GomyCoins requis !";
            endGameVictory.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;

        }
        else if (ennemi.GetComponent<Player>().argent >= nbCoins && argent < nbCoins) // si le joueur perd
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Votre adversaire a atteint les " + nbCoins + " Gomycoins nécessaires avant vous, vous avez perdu.";
            endGameDefeat.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;
        }
        else if (ennemi.GetComponent<Player>().argent >= nbCoins && argent >= nbCoins)// si egalites
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Vous avez tout les deux atteint les " + nbCoins + " Gomycoins nécessaires, c'est une égalité.";
            endGameDraw.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;
        }

       
    }
    public void Rejouer()
    {
        SceneManager.LoadScene("Main");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}