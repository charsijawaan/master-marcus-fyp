using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level1Trigger : MonoBehaviour
{

    public LevelLoader levelLoader;

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.name.Equals("StartLevel1Trigger"))
        {
            levelLoader.Load(Scene.Level_1);
        }
        else if(this.gameObject.name.Equals("StartLevel2Trigger"))
        {
            levelLoader.Load(Scene.Level_2);
        }
        else if (this.gameObject.name.Equals("StartLevel3Trigger"))
        {
            levelLoader.Load(Scene.Level_3);
        }
    }
}


