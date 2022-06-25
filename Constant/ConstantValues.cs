using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.Constant
{
    public class ConstantValues : MonoBehaviour
    {
       public static Vector2 cellSize = new Vector2(254, 387);
       public static Vector2 defaultSpacing = new Vector2(50, 50);
       public static Vector2 fourByFourSpacing = new Vector2(50, 15);
       public static float cellScale = 0.5f;

       public static int pointsForWin = 10;
       public static int pointsForLoose = -6;

       public static string victoryText = "Victory!";
       public static string failureText = "Failure!";

    }
}
