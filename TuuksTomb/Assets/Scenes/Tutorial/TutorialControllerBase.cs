using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class TutorialControllerBase : MonoBehaviour
{
    public GameObject tutorialController;
    [CanBeNull] public DialogueManager dialogueManager;
    protected List<RequiredControl> _requiredControls;

    protected virtual void Start()
    {
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

    private void Update()
    {
        if (_requiredControls==null || _requiredControls.Count == 0) return;

        var currentRequiredControl = _requiredControls[0];

        // The dialogueManager listens to the space key to skip current write.
        // We cannot do that, so we just wait for it to type all the lines.
        if (dialogueManager.textComponent.text != currentRequiredControl.Message) return;

        foreach (var control in currentRequiredControl.Controls)
        {
            if (Input.GetKey(control))
            {
                Debug.Log(control+" has been pressed");
                _requiredControls.RemoveAt(0);
                EnableCurrentRequiredControl();
                break;
            }
        }
    }

    private void EnableCurrentRequiredControl()
    {
        if (_requiredControls.Count == 0)
        {
            EndTutorial();
            return;
        }

        var currentRequiredControl = _requiredControls[0];
        currentRequiredControl.EnableComponents();
        Debug.Log(currentRequiredControl.Message);
        if (dialogueManager == null) return;

        dialogueManager.lines = new string[] { currentRequiredControl.Message };
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(null);
        dialogueManager.StartText();
    }

    protected virtual void EndTutorial()
    {
        dialogueManager.EndDialogue();
        tutorialController.SetActive(false);
    }

    protected class RequiredControl
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