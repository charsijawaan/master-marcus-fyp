using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Scene
{
    MainMenu,
    Story,
    LevelSelect,
    Level_1,
    Level_2,
    Level_3,
    Credits,
}

public class LevelLoader : MonoBehaviour
{        

    public Animator transition;

    public void Load(Scene scene)
    {
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(Scene scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene.ToString());
    }

    public void Load(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }

}

