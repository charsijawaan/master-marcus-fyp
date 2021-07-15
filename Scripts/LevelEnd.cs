using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;


public class LevelEnd : MonoBehaviour
{

    public LevelLoader levelLoader;
    public List<GameObject> EnemeiesList;
    public GameObject pauseMenuUI;
    public GameObject scoreBoardRoot;
    public GameObject KillingScoreText;
    public GameObject FightScoreText;
    public GameObject TimeScoreText;
    public GameObject OverallScoretext;
    public int levelNumber;

    private void OnTriggerEnter(Collider other)
    {
        bool proceedToNextLevel = true;

        for (int i = 0; i < EnemeiesList.Count; i++)
        {
            if (!EnemeiesList[i].GetComponent<DamageDetector>().IsDead() && EnemeiesList[i].gameObject.active)
            {
                proceedToNextLevel = false;
            }
        }

        if(proceedToNextLevel)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.dataPath + "/levelProgress.data";

            FileStream stream = new FileStream(path, FileMode.Create);

            LevelProgress levelProgress = new LevelProgress(levelNumber);

            formatter.Serialize(stream, levelProgress);

            stream.Close();
            
            
            ScoreCalculator.timer.Stop();
            CalculateScore();
            DamageDetector.KillCount = 0;

            pauseMenuUI.SetActive(true);            
            scoreBoardRoot.SetActive(true);
            Time.timeScale = 0f;
            
            

            StartCoroutine(_Foo());
        }
    }

    IEnumerator _Foo()
    {
        while(true)
        {
            if(Input.GetButton("Close PopUp") || Input.GetKeyDown(KeyCode.M))
            {
                break;
            }
            yield return null;
        }
        pauseMenuUI.SetActive(false);
        scoreBoardRoot.SetActive(false);
        Time.timeScale = 1f;
        if (this.gameObject.name.Equals("Trigger_Level_3_End"))
        {
            levelLoader.Load(Scene.Credits);
        }
        else
        {
            levelLoader.Load(Scene.LevelSelect);   
        }
    }

    private void CalculateScore()
    {
        int ACount = 0, BCount = 0, CCount = 0;
        
        // 1
        var timeTaken = ScoreCalculator.timer.Elapsed;
        string timeTakenGrade;
        // 2
        var killCount = DamageDetector.KillCount;
        string killGrade;
        // 3
        var hpLeft = GameObject.Find("Ninja").GetComponent<DamageDetector>().hp;
        string hpGrade;

        // 1
        if (killCount > 6)
        {
            killGrade = "A";
            ACount++;
        }
        else if (killCount > 3)
        {
            killGrade = "B";
            BCount++;
        }
        else
        {
            killGrade = "C";
            CCount++;
        }

        
        // 2
        if (hpLeft >= 8)
        {
            hpGrade = "A";
            ACount++;
        }
        else if (hpLeft >= 5)
        {
            hpGrade = "B";
            BCount++;
        }
        else
        {
            hpGrade = "C";
            CCount++;
        }
        
        // 3
        if (timeTaken.TotalSeconds <= 60)
        {
            timeTakenGrade = "A";
            ACount++;
        }
        else if (timeTaken.TotalSeconds <= 90)
        {
            timeTakenGrade = "B";
            BCount++;
        }
        else
        {
            timeTakenGrade = "C";
            CCount++;
        }
        

        KillingScoreText.GetComponent<TMP_Text>().text = killGrade;
        FightScoreText.GetComponent<TMP_Text>().text = hpGrade;
        TimeScoreText.GetComponent<TMP_Text>().text = timeTakenGrade;

        if (ACount >=2)
        {
            OverallScoretext.GetComponent<TMP_Text>().text = "A";    
        }
        else if (BCount >= 2)
        {
            OverallScoretext.GetComponent<TMP_Text>().text = "B";
        }
        else
        {
            OverallScoretext.GetComponent<TMP_Text>().text = "C";
        }
        
        
    }

}

