using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : CollidableObject
{
    private bool _interacted;
    
    [CanBeNull] public DialogueManager dialogueManager; 
    
    private float lastInteractionTime;
    private float interactionCooldown = 4.0f;
    
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

        if (!(Time.time - lastInteractionTime >= interactionCooldown)) return;
        lastInteractionTime = Time.time;
        InteractWithDialog(collidedObj);
    }
    
    private void InteractWithDialog(GameObject collidedObj)
    {
        collidedObj.transform.GetChild(2).gameObject.GetComponent<Animator>().enabled = false;
        collidedObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collidedObj.GetComponent<PlayerMovement>().enabled = false;
        
        if (dialogueManager != null && !dialogueManager.isDialogInProgress)
        {
            
            dialogueManager.isDialogInProgress = true; // Make dialog skip possible
            
            //Problem probably here:
            dialogueManager.gameObject.SetActive(true); // Activate the dialog canvas
            
            dialogueManager.StartDialogue(collidedObj); // Start the dialog
        }
    }
}
