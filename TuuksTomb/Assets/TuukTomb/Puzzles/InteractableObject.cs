using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : CollidableObject
{
    private bool _interacted;
    
    [CanBeNull] public DialogueManager dialogueManager; 
    
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
        InteractWithDialog();
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
