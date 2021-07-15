using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ObstacleRemover : MonoBehaviour
{

    private void Start()
    {
        int LevelNumber = getNumber();
        
        foreach (Transform child in this.gameObject.transform)
        {
            if (child.gameObject.GetComponent<ObstacleNumberChecker>() != null)
            {
                int obstacleNumber = child.GetComponent<ObstacleNumberChecker>().ObstacleLevelNumber;

                if (obstacleNumber <= LevelNumber)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
    private int getNumber()
    {        
        string path = Application.dataPath + "/levelProgress.data";
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        LevelProgress levelProgress = formatter.Deserialize(stream) as LevelProgress;

        stream.Close();
        return levelProgress.LevelNumber;
    }

    private void Awake()
    {
        //LevelProgress levelProgress = JsonUtility.FromJson<LevelProgress>(jsonFile.text);
        //Debug.Log(levelProgress.LevelNumber);
        //foreach (Transform child in this.gameObject.transform)
        //{
        //    if (child.gameObject.GetComponent<ObstacleNumberChecker>() != null)
        //    {
        //        int obstacleNumber = child.GetComponent<ObstacleNumberChecker>().ObstacleLevelNumber;

        //        if (obstacleNumber <= levelProgress.LevelNumber)
        //        {
        //            child.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }

    //[System.Serializable]
    //public class LevelProgress
    //{
    //    public int LevelNumber;
    //}

}
