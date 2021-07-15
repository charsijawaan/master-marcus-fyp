using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggered : MonoBehaviour
{

    public bool ButtonIsTriggered;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Box"))
        {
            ButtonIsTriggered = true;            
            meshRenderer.material.color = Color.green;
        }
        else if (this.gameObject.name.Equals("Button_Trigger_3") && other.gameObject.name.Equals("Ninja"))
        {
            if (GameObject.Find("Button_Trigger_1").GetComponent<ButtonTriggered>().ButtonIsTriggered && GameObject.Find("Button_Trigger_2").GetComponent<ButtonTriggered>().ButtonIsTriggered)
            {
                ButtonIsTriggered = true;            
                meshRenderer.material.color = Color.green;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Box"))
        {
            ButtonIsTriggered = false;
            meshRenderer.material.color = Color.red;
        }
    }
}
