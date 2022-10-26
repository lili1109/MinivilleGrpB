using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
	public Sprite[] Faces;
	private int nbFaces = 6;
	private int face = 1;
	Image dé;
	System.Random random = new System.Random();

    public void Start()
    {
		dé = GetComponent<Image>();
    }

	public void Throw(GameObject dé)
	{
		dé.SetActive(false);
		StartCoroutine("Shuffle");
		Lancer();
    }
	public void Throw2(GameObject dé)
	{
		dé.SetActive(true);
		StartCoroutine("Shuffle");
		Lancer();
	}
	public void Lancer()
	{
		face = Random.Range(0, 6);
		dé.sprite = Faces[face];
		Debug.Log(face);
	}

	private IEnumerator Shuffle()
	{
		int randomDiceSide = 0;

		for (int i = 0; i <= 14; i++)
		{
			randomDiceSide = Random.Range(0, 6);
			dé.sprite = Faces[randomDiceSide];
			yield return new WaitForSeconds(0.05f);
		}
	}
	public override string ToString()
	{

		string toString = face + "";
		return toString;
	}
}