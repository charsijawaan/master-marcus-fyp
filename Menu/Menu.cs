using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Rendering.Universal;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;


public class Menu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public GameObject MainMenu;
    public GameObject SettingsMenuRoot;

    public List<UniversalRenderPipelineAsset> Settings = new List<UniversalRenderPipelineAsset>();

    public GameObject mainMenuFirstSelected;
    public GameObject settingsMenuFirstSelected;

    public void NewGame()
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/levelProgress.data";

        FileStream stream = new FileStream(path, FileMode.Create);

        ////GameData data = new GameData(sceneName, control, damageDetector);
        LevelProgress levelProgress = new LevelProgress(1);

        formatter.Serialize(stream, levelProgress);

        stream.Close();
        

        //File.WriteAllText(Application.dataPath + "/LevelProgress.json", JsonUtility.ToJson(levelProgress));

        levelLoader.Load(Scene.Story);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //[System.Serializable]
    //public class LevelProgress
    //{
    //    public int LevelNumber;
    //}

    public void ShowSettingsMenuRoot()
    {
        MainMenu.SetActive(false);
        SettingsMenuRoot.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsMenuFirstSelected);
    }

    public void BackToMainMenu()
    {
        SettingsMenuRoot.SetActive(false);
        MainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
    }

    public void SetMSAA(int quality)
    {
        foreach (UniversalRenderPipelineAsset s in Settings)
        {
            // 8x
            if (quality == 0)
            {
                s.msaaSampleCount = 8;
            }
            // 4x
            else if (quality == 1)
            {
                s.msaaSampleCount = 4;
            }
            // 2x
            else if (quality == 2)
            {
                s.msaaSampleCount = 2;
            }
            // 0x
            else if (quality == 3)
            {
                s.msaaSampleCount = 0;
            }
        }
    }

    public void SetRenderScale(float renderScale)
    {
        foreach (UniversalRenderPipelineAsset s in Settings)
        {
            s.renderScale = renderScale / 100f;
        }

    }

    //public void ToggleShadows(bool shadows)
    //{
    //    if (shadows)
    //    {
    //        Light.shadows = LightShadows.Soft;
    //    }
    //    else
    //    {
    //        Light.shadows = LightShadows.None;
    //    }
    //}

    public void ChangeShadowQuality(int qualityIndex)
    {
        QualitySettings.renderPipeline = Settings[qualityIndex];
    }

    public void ChangeTextureQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void LoadGame()
    {
        Loader.Load = true;

        GameData data = SaveSystem.LoadGame();

        //Resume();

        levelLoader.Load(data.LevelName);

    }

}


