using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Programs : MonoBehaviour
{
    [SerializeField]
    GameObject Pile;

    // Start is called before the first frame update
    void Start()
    {
        Game g = new Game();
        for(int i = 0; i <g.Piles.Count; i++)
        { 
            if (i < 4)
            { 
                for (int j = 0; j < 4; j++)
                {
                    Vector3 vect = new Vector3(1.2f * j, 3.0f, 0.0f);
                    Instantiate(Pile, vect, Quaternion.identity);
                }
            }
            else if (i < 8)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector3 vect = new Vector3(1.2f * j, 1.0f, 0.0f);
                    Instantiate(Pile, vect, Quaternion.identity);
                }
            }
            else if(i<12)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector3 vect = new Vector3(1.2f * j, -1.0f, 0.0f);
                    Instantiate(Pile, vect, Quaternion.identity);
                }
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector3 vect = new Vector3(1.2f * j, -3.0f, 0.0f);
                    Instantiate(Pile, vect, Quaternion.identity);
                }
            }
        }
    }

}
