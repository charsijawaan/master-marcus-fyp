using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public LevelLoader levelLoader;
    
    private void Update()
    {
        if (this.gameObject.transform.transform.position.y >= 1050f)
        {
            levelLoader.Load(Scene.LevelSelect);
        }
               
    }
}