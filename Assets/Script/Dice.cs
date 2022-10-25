using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
	public Sprite Face1;
	public Sprite Face2;
	public Sprite Face3;
	public Sprite Face4;
	public Sprite Face5;
	public Sprite Face6;
	private int nbFaces = 6;
	private int face = 1;
	System.Random random = new System.Random();

    public void Start()
    {
		Lancer();
    }

    public void Lancer()
	{
		face = Random.Range(1, 6);	
        if (face == 1)
        {
			this.GetComponent<Image>().sprite = Face1;
        } 
		if (face == 2)
        {
			this.GetComponent<Image>().sprite = Face2;
		}
		if (face == 3)
        {
			this.GetComponent<Image>().sprite = Face3;
		}
		if (face == 4)
        {
			this.GetComponent<Image>().sprite = Face4;
		}
		if (face == 5)
        {
			this.GetComponent<Image>().sprite = Face5;
		}
		if (face == 6)
		{
			this.GetComponent<Image>().sprite = Face6;
		}
		Debug.Log(face);
	}
	public override string ToString()
	{

		string toString = face + "";
		return toString;
	}
}