using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConScrip : MonoBehaviour
{
    public static bool gameOn = false;

    private void Awake()
    {
        gameOn = true;
    }
}
