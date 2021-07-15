using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBossHealth : MonoBehaviour
{
    public GameObject BossEnemyHealth;
    
    private void OnTriggerEnter(Collider other)
    {
        BossEnemyHealth.SetActive(true);
    }
}
