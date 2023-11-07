using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TutorialController : MonoBehaviour
{
    public GameObject sidePlayer;
    public GameObject toggleSceneManager;
    public GameObject pauseMenu;
    [CanBeNull] public DialogueManager dialogueManager;
    private List<RequiredControl> _requiredControls;

    private void Start()
    {
        var characterControl = sidePlayer.GetComponent<CharacterController2D>();
        var playerMovement = sidePlayer.GetComponent<PlayerMovement>();

        var pauseMenuScript = pauseMenu.GetComponent<PauseMenu>();
        var toggleSceneManagerScript = toggleSceneManager.GetComponent<ToggleScene>();

        var movement = new RequiredControl(new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D}," to move!", characterControl, playerMovement);
        var shift = new RequiredControl(new KeyCode[] { KeyCode.LeftShift, KeyCode.RightShift }, " to sprint!");
        var pauseMenuControl = new RequiredControl(new KeyCode[] {KeyCode.Escape }, " to toggle the pause menu!", pauseMenuScript);
        var toggleView = new RequiredControl(new KeyCode[] { KeyCode.Z}, " to toggle the character perspective between side-view and topdown!", toggleSceneManagerScript);

        _requiredControls = new List<RequiredControl>() { movement, shift, pauseMenuControl, toggleView};

        foreach(var control in _requiredControls)
        {
            control.DisableComponents();
        }

        if (dialogueManager != null)
        {
            dialogueManager.gameObject.SetActive(false);
        }

        EnableCurrentRequiredControl();
    }

    void Update()
    {
        var currentRequiredControl = _requiredControls[0];
        if (currentRequiredControl == null) return;

        foreach (var control in currentRequiredControl.Controls)
        {
            if (Input.GetKey(control))
            {
                Debug.Log(control+" has been pressed");
                _requiredControls.RemoveAt(0);
                EnableCurrentRequiredControl();
            }
        }
    }

    private void EnableCurrentRequiredControl()
    {
        if (_requiredControls.Count == 0) return;

        var currentRequiredControl = _requiredControls[0];
        currentRequiredControl.EnableComponents();
        Debug.Log(currentRequiredControl.Message);
        if (dialogueManager == null) return;

        dialogueManager.StartDialogue(sidePlayer);
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.lines = new string[] { currentRequiredControl.Message };
        dialogueManager.StartText();
    }

    private class RequiredControl
    {
        public RequiredControl(KeyCode[] controls, string message, params MonoBehaviour[] disabledComponents) {
            Controls = controls;
            var controlsString = string.Join(", ", controls);
            Message = "Press " + controlsString + message;
            DisabledComponents = disabledComponents.ToList();
        }
        public KeyCode[] Controls;
        public string Message;
        private List<MonoBehaviour> DisabledComponents = new List<MonoBehaviour>();

        public void DisableComponents()
        {
            foreach (var component in DisabledComponents)
            {
                component.enabled = false;
            }
        }

        public void EnableComponents()
        {
            foreach (var component in DisabledComponents)
            {
                component.enabled = true;
            }
        }
    }
}