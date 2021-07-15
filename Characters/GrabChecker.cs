using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabChecker : MonoBehaviour
{

    public bool IsGrabbing;
    public Box GrabbedObject;
    Box CheckBox = null;

    private void OnTriggerEnter(Collider other)
    {
        CheckBox = other.gameObject.GetComponent<Box>();
        if (CheckBox != null)
        {
            IsGrabbing = true;
            GrabbedObject = CheckBox;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CheckBox = other.gameObject.GetComponent<Box>();
        if (CheckBox != null)
        {
            IsGrabbing = false;
        }
    }
}
