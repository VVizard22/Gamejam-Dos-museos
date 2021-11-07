using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int Points { get; set; }


    public static void set(int p)
    {
        Points = p;
    }

    public static void sumarPuntos(int p)
    {
        Points = Points+p;
    }
}