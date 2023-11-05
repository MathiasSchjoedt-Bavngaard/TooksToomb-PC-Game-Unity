using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


public class TutorialController : MonoBehaviour
{
    public GameObject sidePlayer;
    public GameObject topDownPlayer;
    public GameObject toggleSceneManager;
    public GameObject pauseMenu;
    private List<RequiredControl> _requiredControls;

    private void Start()
    {
        var characterControl = sidePlayer.GetComponent<CharacterController2D>();
        var playerMovement = sidePlayer.GetComponent<PlayerMovement>();

        var pauseMenuScript = pauseMenu.GetComponent<PauseMenu>();
        var toggleSceneManagerScript = toggleSceneManager.GetComponent<ToggleScene>();

        var movement = new RequiredControl(new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D}," to move!", characterControl, playerMovement);
        var pauseMenuControl = new RequiredControl(new KeyCode[] {KeyCode.Escape }, " to toggle the pause menu!", pauseMenuScript);
        var shift = new RequiredControl(new KeyCode[] { KeyCode.LeftShift, KeyCode.RightShift }, " to sprint!");
        var toggleView = new RequiredControl(new KeyCode[] { KeyCode.Z}, " to toggle the character perspective between side-view and topdown!", toggleSceneManagerScript);

        _requiredControls = new List<RequiredControl>() { movement, pauseMenuControl, shift, toggleView};

        for (int i = _requiredControls.Count - 1; i >= 0; --i)
        {
            if (i == _requiredControls.Count - 1) continue;

            var curControl = _requiredControls[i];
            var earlierControl = _requiredControls[i + 1];
            curControl.ComponentsToStayDisabled = earlierControl.GetDisabledComponents();
        }

        _requiredControls[0].DisableComponents();
        Debug.Log(_requiredControls[0].Message);
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
                currentRequiredControl.EnableComponents();
                _requiredControls.RemoveAt(0);
                Debug.Log(_requiredControls[0].Message);
            }
        }
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
        public List<MonoBehaviour> ComponentsToStayDisabled = new List<MonoBehaviour>();

        public List<MonoBehaviour> GetDisabledComponents()
        {
            return DisabledComponents.Concat(ComponentsToStayDisabled).ToList();
        }

        public void DisableComponents()
        {
            foreach (var component in DisabledComponents.Concat(ComponentsToStayDisabled))
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