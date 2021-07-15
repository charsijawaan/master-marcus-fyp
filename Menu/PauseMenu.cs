using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuRootUI;
    public GameObject settingsMenuRootUI;
    public GameObject controlsMenuRootUI;
    public Light Light;
    public List<UniversalRenderPipelineAsset> Settings = new List<UniversalRenderPipelineAsset>();

    public GameObject pauseFirstButton;
    public GameObject settingsFirstButton;
    public GameObject controlMenuFirstSelected;

    void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlayerInput.InputIsDisabled = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuRootUI.SetActive(true);
        settingsMenuRootUI.SetActive(false);
        controlsMenuRootUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        PlayerInput.InputIsDisabled = true;
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

    public void OpenSettingsMenu()
    {
        pauseMenuRootUI.SetActive(false);
        settingsMenuRootUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void OpenControlsMenu()
    {
        pauseMenuRootUI.SetActive(false);
        controlsMenuRootUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlMenuFirstSelected);
    }

    public void BackToPauseRootButton()
    {
        pauseMenuRootUI.SetActive(true);
        settingsMenuRootUI.SetActive(false);
        controlsMenuRootUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void SetRenderScale(float renderScale)
    {
        foreach (UniversalRenderPipelineAsset s in Settings)
        {
            s.renderScale = renderScale / 100f;
        }

    }

    public void ToggleShadows(bool shadows)
    {
        if (shadows)
        {
            Light.shadows = LightShadows.Soft;
        }
        else
        {
            Light.shadows = LightShadows.None;
        }
    }

    public void ChangeShadowQuality(int qualityIndex)
    {
        QualitySettings.renderPipeline = Settings[qualityIndex];
    }

    public void ChangeTextureQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SaveGame()
    {
        var enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        SaveSystem.SaveGame(SceneManager.GetActiveScene().name ,CharacterManager.Instance.GetPlayableCharacter(), CharacterManager.Instance.GetPlayableCharacter().GetComponent<DamageDetector>(), enemyList);
        Debug.Log("Game Saved");
    }


    public void LoadGame()
    {
        Loader.Load = true;

        GameData data = SaveSystem.LoadGame();

        Resume();

        levelLoader.Load(data.LevelName);

    }

    public void GoToMainMenu()
    {
        Resume();
        levelLoader.Load(Scene.MainMenu.ToString());
    }
    

}

