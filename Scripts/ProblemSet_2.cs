using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemSet_2 : MonoBehaviour
{

    public List<ButtonTriggered> ButtonTriggers = new List<ButtonTriggered>();
    public List<GameObject> Platforms = new List<GameObject>();
    
    [SerializeField] public string popUpText;
    private bool istriggered = false;
    public GameObject canvas;
    public PopUpSystem pop;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        pop = canvas.GetComponentInChildren<PopUpSystem>();
    }

    private void Update()
    {
        bool show = true;
        foreach (ButtonTriggered b in ButtonTriggers)
        {
            if (!b.ButtonIsTriggered)
            {
                show = false;
            }
        }
        if (show)
        {
            if (istriggered == false)
            {
                PlayerInput.InputIsDisabled = true;
                pop.PopUp(popUpText);
                istriggered = true;
            }

            if (Input.GetButton("Close PopUp"))
            {
                pop.ClosePopUp();
                PlayerInput.InputIsDisabled = false;
            }
            
            foreach(GameObject p in Platforms)
            {
                p.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject p in Platforms)
            {
                p.SetActive(false);
            }
        }
    }
}
