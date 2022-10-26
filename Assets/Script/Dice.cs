using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
	public Sprite[] Faces;
	private int nbFaces = 6;
	private int face = 1;
	Image d�;
	System.Random random = new System.Random();

    public void Start()
    {
		d� = GetComponent<Image>();
    }

	public void Throw(GameObject d�)
	{
		d�.SetActive(false);
		StartCoroutine("Shuffle");
		Lancer();
    }
	public void Throw2(GameObject d�)
	{
		d�.SetActive(true);
		StartCoroutine("Shuffle");
		Lancer();
	}
	public void Lancer()
	{
		face = Random.Range(0, 6);
		d�.sprite = Faces[face];
		Debug.Log(face);
	}

	private IEnumerator Shuffle()
	{
		int randomDiceSide = 0;

		for (int i = 0; i <= 14; i++)
		{
			randomDiceSide = Random.Range(0, 6);
			d�.sprite = Faces[randomDiceSide];
			yield return new WaitForSeconds(0.05f);
		}
	}
	public override string ToString()
	{

		string toString = face + "";
		return toString;
	}
}