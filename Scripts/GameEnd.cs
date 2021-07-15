using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public LevelLoader levelLoader;
    
    private void Update()
    {
        if (this.gameObject.transform.transform.position.y >= 945f)
        {
            levelLoader.Load(Scene.MainMenu);
        }
               
    }
}
