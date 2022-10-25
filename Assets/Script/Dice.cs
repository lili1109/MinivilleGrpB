using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	private int nbFaces;
	private int face = 1;
	System.Random random = new System.Random();

	public Dice(int anbFaces)
	{
		this.nbFaces = anbFaces;
	}

	public int NbFaces
	{
		get { return nbFaces; }
		set { nbFaces = value; }
	}

	public int Lancer()
	{
		this.face = random.Next(1, this.nbFaces + 1);
		return this.face;
	}
	public override string ToString()
	{
		string toString = this.face + "";
		return toString;
	}
}