using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Game()
    {
        Pile pile = new Pile(new Cards(1, "B", "Immeuble 83", 1, 1));
        Dice de = new Dice(6);
        Dice de2 = new Dice(6);


    }
}
