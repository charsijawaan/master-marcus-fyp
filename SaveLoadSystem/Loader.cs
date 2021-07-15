using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Loader : MonoBehaviour
{
    public static bool Load = false;

    public void Start()
    {
        if (Load)
        {

            GameData data = SaveSystem.LoadGame();
            Vector3 MarcusPosition = new Vector3(data.MarcusPosition[0], data.MarcusPosition[1], data.MarcusPosition[2]);
            Quaternion MarcusRotation = new Quaternion(data.MarcusRotation[0], data.MarcusRotation[1], data.MarcusRotation[2], data.MarcusRotation[3]);

            var freshEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
            List<string> names = new List<string>();

            for (int i = 0; i < freshEnemyList.Length; i++)
            {
                names.Add(freshEnemyList[i].gameObject.name);
            }

            for (int i = 0; i < names.Count; i++)
            {
                if (!data.enemyNames.Contains(names[i]))
                {
                    GameObject.Find(names[i]).SetActive(false);
                }
            }

            for (int i = 0; i < data.enemyDeadListTracker.Length; i++)
            {
                if (data.enemyDeadListTracker[i] == false)
                {
                    GameObject.Find(data.enemyNames[i]).SetActive(false);
                }
            }

            // Debug.Log("Fresh List");
            // for (int i = 0; i < names.Count; i++)
            // {
            //     Debug.Log(names[i]);
            // }

            // Debug.Log("Stored");
            // for (int i = 0; i < data.enemyDeadListTracker.Length; i++)
            // {
                
                //Debug.Log(data.enemyNames[i]);
                //Debug.Log(data.enemyNames[i] + " = " + data.enemyDeadListTracker[i]);
                //if (!data.enemyNames.Contains(names[i]))
                //{
                    //Debug.Log(names[i]);
                    //GameObject.Find(names[i]).SetActive(false);
                //    continue;
                //}
                //
                // else if (data.enemyDeadListTracker[i] == false)
                // {
                //     GameObject.Find(data.enemyNames[i]).SetActive(false);
                // }
            //}

            float MarcusHP = data.MarcusHP;

            CharacterManager.Instance.GetPlayableCharacter().transform.position = MarcusPosition;
            CharacterManager.Instance.GetPlayableCharacter().transform.rotation = MarcusRotation;

            CharacterManager.Instance.GetPlayableCharacter().GetComponent<DamageDetector>().hp = MarcusHP;
            CharacterManager.Instance.GetPlayableCharacter().GetComponent<DamageHealthBarLink>().healthBar.SetHealth(MarcusHP);

            Load = false;
        }
        else
        {
            // do nothing
            // default settings
        }
    }
}

