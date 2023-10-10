using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpControl : MonoBehaviour
{
    [SerializeField]
    public GameObject _resumeButton, _menuButton, _quitButton, _showHelpButton, _hideHelpButton, _helpText;

    public void ShowHelp()
    {
        _resumeButton.SetActive(false);
        _menuButton.SetActive(false);
        _quitButton.SetActive(false);
        _showHelpButton.SetActive(false);
        _hideHelpButton.SetActive(true);
        _helpText.SetActive(true);
    }

    public void HideHelp()
    {
        _resumeButton.SetActive(true);
        _menuButton.SetActive(true);
        _quitButton.SetActive(true);
        _showHelpButton.SetActive(true);
        _hideHelpButton.SetActive(false);
        _helpText.SetActive(false);
    }
}
