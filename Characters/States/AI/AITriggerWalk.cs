using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriggerWalk : MonoBehaviour
{
    public List<GameObject> AIList = new List<GameObject>();

    public void OnTriggerEnter(Collider collision)
    {
        foreach (GameObject o in AIList)
        {
            o.SetActive(true);
        }
    }

}
