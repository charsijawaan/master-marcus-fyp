using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Settings : MonoBehaviour
{
    public FrameSettings frameSettings;
    public PhysicsSettings physicsSettings;

    private void Awake()
    {
        //Frames
        Time.timeScale = frameSettings.TimeScale;

        Application.targetFrameRate = frameSettings.TargetFPS;

        //Physics
        Physics.defaultSolverVelocityIterations = physicsSettings.DefaultSolverVelocityIterations;

        //Default Keys
        VirtualInputManager.Instance.LoadKeys();
        //VirtualInputManager.Instance.SetDefaultKeys();
    }
}

