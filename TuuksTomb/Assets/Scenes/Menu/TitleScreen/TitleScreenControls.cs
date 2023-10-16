using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenControls : MonoBehaviour
{
    public SceneReference startLevel;

    public void LoadLevel()
    {
        SceneManager.LoadScene(startLevel.Name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
