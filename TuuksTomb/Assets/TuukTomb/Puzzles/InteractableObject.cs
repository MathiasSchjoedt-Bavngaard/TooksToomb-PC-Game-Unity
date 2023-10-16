using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : CollidableObject
{
    private bool _interacted;
    
    [CanBeNull] public DialogueManager dialogueManager; 
    
    public RawImage fullScreenOverlay; // Reference to the RawImage covering the screen
    public float fadeDuration = 3.0f; // Duration of the fading effect

    protected override void Start()
    {
        base.Start();
        if (dialogueManager != null)
        {
            dialogueManager.gameObject.SetActive(false);
        }
    }


    protected override void WhenCollided(GameObject collidedObj)
    {
        if (!Input.GetKey(KeyCode.E)) return;
        //OnInteract();
        InteractWithDialog();
    }
    protected virtual void OnInteract()
    {
        if (_interacted) return;
        _interacted = true;
    }
    
    
    private void InteractWithDialog()
    {
        if (dialogueManager != null && !dialogueManager.isDialogInProgress)
        {
            
            dialogueManager.isDialogInProgress = true; // Make dialog skip possible
            
            //Problem probably here:
            dialogueManager.gameObject.SetActive(true); // Activate the dialog canvas
            
            dialogueManager.StartDialogue(); // Start the dialog
        }
    }
}
