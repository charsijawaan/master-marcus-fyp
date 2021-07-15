using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static bool IsBox(GameObject obj)
    {
        if (obj.GetComponent<Box>() == null)
        {
            return false;
        }

        return true;
    }

    public static bool IsGrabChecker(GameObject obj)
    {
        if (obj.GetComponent<GrabChecker>() == null)
        {
            return false;
        }

        return true;
    }

    public static bool IsCharacter(GameObject obj)
    {
        if (obj.transform.root.GetComponent<CharacterControl>() == null)
        {
            return false;
        }

        return true;
    }

}
