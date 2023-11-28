using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenControls : MonoBehaviour
{
    public SceneReference startLevel;
    public Vector3 playerPos;

    public void ContinueLevel()
    {
        
        PlayerPrefs.SetFloat("x", playerPos.x);
        PlayerPrefs.SetFloat("z", playerPos.z);
        PlayerPrefs.SetFloat("y", playerPos.y);
        var continueLevel = PlayerPrefs.GetString("continueLevel");
        if (string.IsNullOrEmpty(continueLevel))
        {
            continueLevel = startLevel.Name;
        }
        SceneManager.LoadScene(continueLevel);
    }
    public void LoadLevel()
    {
        PlayerPrefs.SetFloat("x", playerPos.x);
        PlayerPrefs.SetFloat("z", playerPos.z);
        PlayerPrefs.SetFloat("y", playerPos.y);

        SceneManager.LoadScene(startLevel.Name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
