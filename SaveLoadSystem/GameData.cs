using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public string LevelName;
    public float[] MarcusPosition;
    public float[] MarcusRotation;
    public float MarcusHP;
    public bool[] enemyDeadListTracker;
    public string[] enemyNames;
 
    public GameData(string sceneName, CharacterControl control, DamageDetector damageDetector, GameObject[] enemyList)
    {
        LevelName = sceneName;

        MarcusPosition = new float[3];
        MarcusRotation = new float[4];
        enemyDeadListTracker = new bool[enemyList.Length];
        enemyNames = new string[enemyList.Length];

        MarcusPosition[0] = control.transform.position.x;
        MarcusPosition[1] = control.transform.position.y;
        MarcusPosition[2] = control.transform.position.z;

        MarcusRotation[0] = control.transform.rotation.x;
        MarcusRotation[1] = control.transform.rotation.y;
        MarcusRotation[2] = control.transform.rotation.z;
        MarcusRotation[3] = control.transform.rotation.w;
        
        MarcusHP = damageDetector.hp;

        // False means enemy is dead
        // True means enemy is alive
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i].GetComponent<DamageDetector>().IsDead())
            {
                enemyDeadListTracker[i] = false;
                enemyNames[i] = enemyList[i].gameObject.name;
            }
            else
            {
                enemyDeadListTracker[i] = true;
                enemyNames[i] = enemyList[i].gameObject.name;
            }
        }

    }

}