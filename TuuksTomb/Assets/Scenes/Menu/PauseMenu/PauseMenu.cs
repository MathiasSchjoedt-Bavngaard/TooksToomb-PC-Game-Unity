using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public static CursorLockMode lastLockMode;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resume");
        Cursor.lockState = lastLockMode;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Debug.Log("Pause");
        lastLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        SceneManager.LoadScene("TitleScreen");
    }
    
    public void ResetMeny()
    {
        Time.timeScale = 1.0f;
        GameIsPaused = false;
 
        PlayerPrefs.SetFloat("x", -7);
        PlayerPrefs.SetFloat("z", 0);
        PlayerPrefs.SetFloat("y", 3);

        var continueLevel = PlayerPrefs.GetString("continueLevel");
        if (string.IsNullOrEmpty(continueLevel)) return;

        SceneManager.LoadScene(continueLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}