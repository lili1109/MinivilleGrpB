using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
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
<<<<<<< Updated upstream
		string toString = this.face + "";
=======
		string toString =this.face + "";
>>>>>>> Stashed changes
		return toString;
	}
}