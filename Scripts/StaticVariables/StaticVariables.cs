﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour {
    public static int ranking = -1;
    public static bool gameIsOver = false;
    public static bool gameStarts = false;
    public static bool musicStartFlag = false;
    public static string raceTimeStr = "";
    public static int coinNumber = 0;
    public static JSONObject saveData = null;

    public static void resetVariables()
    {
        ranking = -1;
        gameIsOver = false;
        gameStarts = false;
        musicStartFlag = false;
        raceTimeStr = "";
        coinNumber = 0;
        // dont reset savedata
    }
}
