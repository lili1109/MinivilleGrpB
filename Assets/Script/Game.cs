using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game 
{
    [SerializeField]
    Dice de;
    
    public List<Pile> Piles = new List<Pile>();
    public Game()
    {
       
        Piles.Add(new Pile(new Cards(1, "B", "Immeuble 83", 1, 1)));
        Piles.Add(new Pile(new Cards(1, "B", "Epicerie solidaire", 1, 2)));
        Piles.Add(new Pile(new Cards(2, "V", "Musée du Papier", 2, 1)));
        Piles.Add(new Pile(new Cards(3, "R", "Bock n'Roll", 1, 2)));
        Piles.Add(new Pile(new Cards(4, "V", "Musée de la BD", 3, 2)));
        Piles.Add(new Pile(new Cards(5, "B", "Jardin Vert", 1, 2)));
        Piles.Add(new Pile(new Cards(5, "R", "RestoU", 2, 4)));
        Piles.Add(new Pile(new Cards(6, "B", "Cnam Enjmin", 5, 7)));
        Piles.Add(new Pile(new Cards(7, "B", "Cosmopolite", 2, 3)));
        Piles.Add(new Pile(new Cards(7, "B", "CGR", 1, 3)));
        Piles.Add(new Pile(new Cards(8, "V", "Patinoire", 2, 2)));
        Piles.Add(new Pile(new Cards(9, "R", "Hôtel de ville", 1, 3)));
        Piles.Add(new Pile(new Cards(10, "V", "Gare SNCF", 3, 4)));
        Piles.Add(new Pile(new Cards(11, "B", "Bare Le Nil", 1, 2)));
        Piles.Add(new Pile(new Cards(11, "R", "E.Leclerc", 2, 4)));
        Piles.Add(new Pile(new Cards(12, "B", "Bord de la Charente", 4, 6)));
    }
}
