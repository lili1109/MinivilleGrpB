using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Pile> piles = new List<Pile>();
    public Game()
    {
        piles.Add(new Pile(new Cards(1, "B", "Immeuble 83", 1, 1)));
    }
}
