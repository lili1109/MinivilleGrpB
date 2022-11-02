using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject joueur1;
    [SerializeField] GameObject joueur2;
    public List<GameObject> mainJoueur1, mainJoueur2;
    public bool j1, j2;

    [Header("Affichage")]
    [SerializeField] GameObject choixNiveau;
    [SerializeField] GameObject updateScoreJ1, updateScoreJ2, textEndGame, endGameVictory, endGameDefeat, endGameDraw, endGameCalque;
    public bool piles;

    [Header("Object")]
    public GameObject de;
    public List<GameObject> cartesPiles;

    [Header("Son")]
    public AudioManager audioManager;

    int nbCoins = 20;
    int gainTotalMancheJoueur = 0;
    int gainTotalMancheEnnemi = 0;
    int scoreDe = 0;
    bool enJeu = true;
    private void Awake()
    {
        j1 = true;
        j2 = false;
        endGameCalque.SetActive(false);
        endGameVictory.SetActive(false);
        endGameDefeat.SetActive(false);
        endGameDraw.SetActive(false);
        choixNiveau.SetActive(true);
        mainJoueur1 = joueur1.GetComponent<Player>().mainJoueur;
        mainJoueur2 = joueur2.GetComponent<Player>().mainJoueur;
    }

    // Update is called once per frame
    void Update()
    {
        mainJoueur1 = joueur1.GetComponent<Player>().mainJoueur;
        mainJoueur2 = joueur2.GetComponent<Player>().mainJoueur;
        EndGame();
    }
    public void Rapide() //Partie rapide
    {
        nbCoins = 10;
        choixNiveau.gameObject.SetActive(false);
    }
    public void Standard()//Partie Standard
    {
        nbCoins = 20;
        choixNiveau.gameObject.SetActive(false);
    }
    public void Long()//Partie Longue
    {
        nbCoins = 30;
        choixNiveau.gameObject.SetActive(false);
    }
    public IEnumerator TempsAffichageJ1()
    {
        updateScoreJ1.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        updateScoreJ1.gameObject.SetActive(false);
    }

    public IEnumerator TempsAffichageJ2()
    {
        updateScoreJ2.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        updateScoreJ2.gameObject.SetActive(false);
    }

    public void tourJ1() //tour du joueur
    {
        if (enJeu) // si pas fini
        {
            scoreDe = de.GetComponent<Dice>().score; //recuperation score des des
            gainTotalMancheJoueur = 0;
            gainTotalMancheEnnemi = 0;
            foreach (var carte in mainJoueur1) //recheche dans les cartes du joueur si carte Bleue ou Verte
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                if ((colorCarte == "B" || colorCarte == "V") && card.de == scoreDe)
                {
                    int gainCarte = card.gain;
                    joueur1.GetComponent<Player>().argent += gainCarte;
                    gainTotalMancheJoueur += gainCarte;
                }
            }

            foreach (var carte in joueur2.GetComponent<Player>().mainJoueur) //recheche dans les cartes de l'ordi si carte Bleue ou Rouge
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                int carteGain = card.gain;
                int deCarte = card.de;
                if (colorCarte == "R" && deCarte == scoreDe) //Si carte rouge
                {
                    int joueur2Argent = joueur2.GetComponent<Player>().argent; //recupere l'argent qu'a l'ordi

                    if (joueur1.GetComponent<Player>().argent >= carteGain)// le joueur a assez de piece pour payer l'ordi
                    {
                        joueur1.GetComponent<Player>().argent -= carteGain;
                        gainTotalMancheJoueur -= carteGain;
                        gainTotalMancheEnnemi += carteGain;
                        joueur2.GetComponent<Player>().argent += carteGain;
                        updateScoreJ1.GetComponent<TMP_Text>().text = carteGain.ToString();
                        // coin sound
                        audioManager.GomyCoinSound();
                    }
                    else // sinon il lui donne tous ce qu'il a
                    {
                        int gainCarte = joueur1.GetComponent<Player>().argent;
                        joueur1.GetComponent<Player>().argent -= gainCarte;
                        gainTotalMancheJoueur -= gainCarte;
                        gainTotalMancheEnnemi += gainCarte;
                        joueur2.GetComponent<Player>().argent += gainCarte;
                        updateScoreJ1.GetComponent<TMP_Text>().text = gainCarte.ToString();
                        // coin sound
                        audioManager.GomyCoinSound();
                    }
                }
                else if (colorCarte == "B" && deCarte == scoreDe) // si carte bleu gagne le gain
                {
                    int gainCarte = carteGain;
                    joueur2.GetComponent<Player>().argent += gainCarte;
                    gainTotalMancheEnnemi += gainCarte;
                    updateScoreJ1.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    // coin sound
                    audioManager.GomyCoinSound();
                }
            }
            if (gainTotalMancheEnnemi > 0)
            {
                updateScoreJ2.GetComponent<TMP_Text>().text = "+" + gainTotalMancheEnnemi.ToString();
                StartCoroutine("TempsAffichageJ2");
                // coin sound
                audioManager.GomyCoinSound();
            }
            else if (gainTotalMancheEnnemi < 0)
            {
                updateScoreJ2.GetComponent<TMP_Text>().text = gainTotalMancheEnnemi.ToString();
                StartCoroutine("TempsAffichageJ2");
                // coin sound
                audioManager.GomyCoinSound();
            }
            if (gainTotalMancheJoueur > 0)
            {
                updateScoreJ1.GetComponent<TMP_Text>().text = "+" + gainTotalMancheJoueur.ToString();
                StartCoroutine("TempsAffichageJ1");
                // coin sound
                audioManager.GomyCoinSound();
            }
            else if (gainTotalMancheJoueur < 0)
            {
                updateScoreJ1.GetComponent<TMP_Text>().text = gainTotalMancheJoueur.ToString();
                StartCoroutine("TempsAffichageJ1");
                // coin sound
                audioManager.GomyCoinSound();
            }
            piles = true;//Active l'acces au piles
            j1 = false;
            j2 = true;
        }
    }
    public void tourDeOrdi() //L'ordi lance les des
    {
        if (enJeu) // si toujours en jeu
        {
            bool de2 = false;
            foreach (var carte in joueur2.GetComponent<Player>().mainJoueur) //Si parmis les cartes il y en a qui on besoin de plus de 6 pour s'activer alors choix random entre 2 des ou 1 de
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
    public void tourJ2() // tour de l'ordi
    {
        if (enJeu) //si toujours en jeu
        {
            scoreDe = de.GetComponent<Dice>().score; //recuperation score du de
            gainTotalMancheJoueur = 0;
            gainTotalMancheEnnemi = 0;

            foreach (var carte in joueur2.GetComponent<Player>().mainJoueur) // recherche parmis la main de l'ordi si carte Bleu et verte 
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                if ((colorCarte == "B" || colorCarte == "V") && card.de == scoreDe)
                {
                    int gainCarte = card.gain;
                    gainTotalMancheEnnemi += gainCarte;
                    joueur2.GetComponent<Player>().argent += gainCarte;
                }
            }
            foreach (var carte in joueur1.GetComponent<Player>().mainJoueur) // recherche par mis la main du joueur si carte Rouge ou Bleu
            {
                Card card = carte.GetComponent<Card>();
                string colorCarte = card.color;
                int carteGain = card.gain;
                if (colorCarte == "R" && card.de == scoreDe) //si carte rouge 
                {
                    if (joueur2.GetComponent<Player>().argent >= carteGain) //verifie si l'ordi a de quoi payer le joueur
                    {
                        joueur1.GetComponent<Player>().argent += carteGain;
                        gainTotalMancheJoueur += carteGain;
                        gainTotalMancheEnnemi -= carteGain;
                        joueur2.GetComponent<Player>().argent -= carteGain;
                        updateScoreJ1.GetComponent<TMP_Text>().text = carteGain.ToString();
                        // coin sound
                        audioManager.GomyCoinSound();
                    }
                    else //sinon lui donne tous ce qu'il a
                    {
                        int gainCarte = joueur2.GetComponent<Player>().argent;
                        joueur1.GetComponent<Player>().argent += gainCarte;
                        gainTotalMancheJoueur += gainCarte;
                        gainTotalMancheEnnemi -= gainCarte;
                        joueur2.GetComponent<Player>().argent -= gainCarte;
                        updateScoreJ1.GetComponent<TMP_Text>().text = gainCarte.ToString();
                        // coin sound
                        audioManager.GomyCoinSound();
                    }



                }
                else if (colorCarte == "B" && card.de == scoreDe) // si carte bleu recupere gain de la carte
                {
                    int gainCarte = card.gain;
                    joueur1.GetComponent<Player>().argent += gainCarte;
                    gainTotalMancheJoueur += gainCarte;
                    updateScoreJ1.GetComponent<TMP_Text>().text = gainCarte.ToString();
                    // coin sound
                    audioManager.GomyCoinSound();
                }
            }
            //L'ordi pioche
            List<GameObject> pileValideEnnemi = new List<GameObject>(); //Liste qui va contenir toute les carte que l'ordi peut acheter
            foreach (var piles in cartesPiles)
            {
                if (joueur2.GetComponent<Player>().argent >= piles.GetComponent<Card>().prix && piles.GetComponent<Pile>().nbCartes > 0)
                {
                    pileValideEnnemi.Add(piles);
                }
            }
            if (gainTotalMancheEnnemi > 0)
            {
                updateScoreJ2.GetComponent<TMP_Text>().text = "+" + gainTotalMancheEnnemi.ToString();
                StartCoroutine("TempsAffichageJ2");
                // coin sound
                audioManager.GomyCoinSound();
            }
            else if (gainTotalMancheEnnemi < 0)
            {
                updateScoreJ2.GetComponent<TMP_Text>().text = gainTotalMancheEnnemi.ToString();
                StartCoroutine("TempsAffichageJ2");
                // coin sound
                audioManager.GomyCoinSound();
            }
            if (gainTotalMancheJoueur > 0)
            {
                updateScoreJ1.GetComponent<TMP_Text>().text = "+" + gainTotalMancheJoueur.ToString();
                StartCoroutine("TempsAffichageJ1");
                // coin sound
                audioManager.GomyCoinSound();
            }
            else if (gainTotalMancheJoueur < 0)
            {
                updateScoreJ1.GetComponent<TMP_Text>().text = gainTotalMancheJoueur.ToString();
                StartCoroutine("TempsAffichageJ1");
                // coin sound
                audioManager.GomyCoinSound();
            }
            //si le joueur a plus de 0 gomy coins il pioche une carte au hasard et la paye
            if (joueur2.GetComponent<Player>().argent > 0)
            {
                int nb = Random.Range(0, pileValideEnnemi.Count);
                pileValideEnnemi[nb].GetComponent<Pile>().OnclickEnnemi();
            }
            de.GetComponent<Dice>().activeDes();
        }
        j1 = true;
        j2 = false;
    }
    public void EndGame() //condition de fin du jeu
    {
        if (joueur2.GetComponent<Player>().argent < nbCoins && joueur1.GetComponent<Player>().argent >= nbCoins) // le joueur gagne
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Vous avez gagné car vous avez atteint les " + nbCoins + " GomyCoins requis !";
            endGameVictory.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;

        }
        else if (joueur2.GetComponent<Player>().argent >= nbCoins && joueur1.GetComponent<Player>().argent < nbCoins) // si le joueur perd
        {
            endGameCalque.gameObject.SetActive(true);
            textEndGame.GetComponent<TMP_Text>().text = "Votre adversaire a atteint les " + nbCoins + " Gomycoins nécessaires avant vous, vous avez perdu.";
            endGameDefeat.gameObject.SetActive(true);
            piles = false;
            de.GetComponent<Dice>().DesactiveDes();
            enJeu = false;
        }
        else if (joueur2.GetComponent<Player>().argent >= nbCoins && joueur1.GetComponent<Player>().argent >= nbCoins)// si egalites
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
