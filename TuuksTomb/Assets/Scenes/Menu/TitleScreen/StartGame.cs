using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public SceneReference startLevel;

    public void LoadLevel()
    {
        SceneManager.LoadScene(startLevel.Name);
    }
}
