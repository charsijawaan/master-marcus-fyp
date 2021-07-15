using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    public GameObject KillCountText;
    
    public void SetText(int killCount)
    {
        KillCountText.GetComponent<TextMeshProUGUI>().text = "Kill Count = " + killCount.ToString();
    }
   
}
