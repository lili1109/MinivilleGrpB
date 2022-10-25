using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public int Face
		{
			get { return face; }
		}

		public int Lancer()
		{
			face = random.Next(1, nbFaces + 1);
			return face;
		}
        public override string ToString()
        {
			string toString = face + "";
            return toString;
        }
}
