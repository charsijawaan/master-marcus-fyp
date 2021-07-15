using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SceneLoader
{

    public static Animator transition;

    public enum Scene
    {
        MainMenu,
        LevelSelect,
        Level_1,
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    //static IEnumerator LoadLevel(Scene scene)
    //{
    //    transition.SetTrigger("Start");

    //    yield return new WaitForSeconds(1);

    //    SceneManager.LoadScene(scene.ToString());
    //}

}

