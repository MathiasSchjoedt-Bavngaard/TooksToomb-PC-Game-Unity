using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenControls : MonoBehaviour
{
    public SceneReference startLevel;
    public Vector3 playerPos;
    public GameObject continueButton;
    private string _continueLevel;

    private void Update()
    {
        _continueLevel = PlayerPrefs.GetString("continueLevel");

        var continueActive = !string.IsNullOrEmpty(_continueLevel);
        if (continueButton != null)
            continueButton.SetActive(continueActive);
    }

    public void ContinueLevel()
    {
        PlayerPrefs.SetFloat("x", playerPos.x);
        PlayerPrefs.SetFloat("z", playerPos.z);
        PlayerPrefs.SetFloat("y", playerPos.y);

        if (string.IsNullOrEmpty(_continueLevel))
        {
            _continueLevel = startLevel.Name;
        }
        SceneManager.LoadScene(_continueLevel);
    }

    public void LoadLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(startLevel.Name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
