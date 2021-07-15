using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings", menuName = "MasterMarcus/Settings/FrameSettings")]
public class FrameSettings : ScriptableObject
{
    [Range(0.01f, 1f)]
    public float TimeScale;
    public int TargetFPS;
}
