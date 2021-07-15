using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public static Stopwatch timer = new Stopwatch();
    
    private void Start()
    {
        timer.Start();
    }
}
