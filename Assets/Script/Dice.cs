using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
	public Sprite[] Faces;
	public GameObject dé1;
	public GameObject dé2;
	private int face = 1;
	public int score;
	[SerializeField] GameObject scoreTotal;
	public GameObject btnDe1;
    public GameObject btnDe2;
	public GameObject mainJoueur;

    public void Throw()
    {
        dé2.SetActive(false);
        StartCoroutine("Shuffle");
        btnDe1.GetComponent<Button>().interactable = false;
        btnDe2.GetComponent<Button>().interactable = false;
        mainJoueur.GetComponent<Player>().joue = true;


    }
    public void Throw2()
    {
        dé2.SetActive(true);
        StartCoroutine("Shuffle2");
        btnDe1.GetComponent<Button>().interactable = false;
        btnDe2.GetComponent<Button>().interactable = false;
        mainJoueur.GetComponent<Player>().joue = true;


    }
    public void ThrowEnnemi()
    {
        dé2.SetActive(false);
        StartCoroutine("Shuffle");
        btnDe1.GetComponent<Button>().interactable = false;
        btnDe2.GetComponent<Button>().interactable = false;
    }
    public void Throw2Ennemi()
    {
        dé2.SetActive(true);
        StartCoroutine("Shuffle2");
        btnDe1.GetComponent<Button>().interactable = false;
        btnDe2.GetComponent<Button>().interactable = false;

    }

    private IEnumerator Shuffle2()
	{
		int randomDiceSide = 0;
		int randomDiceSide2 = 0;

		for (int i = 0; i <= 14; i++)
		{
			randomDiceSide = Random.Range(0, 6);
			dé1.GetComponent<Image>().sprite = Faces[randomDiceSide];
			randomDiceSide2 = Random.Range(0, 6);
			dé2.GetComponent<Image>().sprite = Faces[randomDiceSide2];
			yield return new WaitForSeconds(0.05f);
		}
		score = randomDiceSide + randomDiceSide2 + 2;
		scoreTotal.GetComponent<TMP_Text>().text = score.ToString();
        if (mainJoueur.GetComponent<Player>().joue == true)
        {
            mainJoueur.GetComponent<Player>().tourJoueur();
        }
        else if (mainJoueur.GetComponent<Player>().bEnnemi == true)
        {
            mainJoueur.GetComponent<Player>().piles = false;
            mainJoueur.GetComponent<Player>().tourEnnemi();
        }
    }
	private IEnumerator Shuffle()
	{
		int randomDiceSide = 0;

		for (int i = 0; i <= 14; i++)
		{
			randomDiceSide = Random.Range(0, 6);
			dé1.GetComponent<Image>().sprite = Faces[randomDiceSide];
			yield return new WaitForSeconds(0.05f);
		}
		score = randomDiceSide + 1;
		scoreTotal.GetComponent<TMP_Text>().text = score.ToString();
        if (mainJoueur.GetComponent<Player>().joue == true)
        {
            mainJoueur.GetComponent<Player>().tourJoueur();
        }else if(mainJoueur.GetComponent<Player>().bEnnemi == true)
		{
            mainJoueur.GetComponent<Player>().piles = false;
            mainJoueur.GetComponent<Player>().tourEnnemi();
        }
    }

    public override string ToString()
	{
		string toString = face + "";
		return toString;
	}

	public void activeDes()
	{
        btnDe1.GetComponent<Button>().interactable = true;
        btnDe2.GetComponent<Button>().interactable = true;
    }
    public void DesactiveDes()
    {
        btnDe1.GetComponent<Button>().interactable = false;
        btnDe2.GetComponent<Button>().interactable = false;
    }
}