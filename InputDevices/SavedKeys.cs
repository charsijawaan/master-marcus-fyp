using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings", menuName = "MasterMarcus/Settings/SavedKeys")]
public class SavedKeys : ScriptableObject
{
    public List<KeyCode> KeyCodesList = new List<KeyCode>();
}
