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
        OnInteract();
        InteractWithDialog();
    }
    protected virtual void OnInteract()
    {
        if (_interacted) return;
        _interacted = true;
    }

    // Function to reset the game view after the dialog ends
    public void ResetGameView()
    {
        StartCoroutine(FadeOut());
    }
    
    private void InteractWithDialog()
    {
        if (dialogueManager != null && !dialogueManager.isDialogInProgress)
        {
            StartCoroutine(FadeIn());
            
            dialogueManager.isDialogInProgress = true; // Make dialog skip possible
            
            //Problem probably here:
            dialogueManager.gameObject.SetActive(true); // Activate the dialog canvas
            
            dialogueManager.StartDialogue(); // Start the dialog
        }
    }
    
    private IEnumerator FadeIn()
    {
        var startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - startTime;
            var alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            fullScreenOverlay.color = new Color(fullScreenOverlay.color.r, fullScreenOverlay.color.g, fullScreenOverlay.color.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        var startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - startTime;
            var alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            fullScreenOverlay.color = new Color(fullScreenOverlay.color.r, fullScreenOverlay.color.g, fullScreenOverlay.color.b, alpha);
            yield return null;
        }
    }
    
}
