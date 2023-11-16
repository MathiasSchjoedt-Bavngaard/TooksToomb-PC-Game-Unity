using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxDialogue : CollidableObject
{
    private bool _interacted;
    
    private float _lastInteractionTime;
    private const float InteractionCooldown = 4.0f;
    private GameObject player;
    
    
    public Canvas dialogueCanvas;
    
    protected override void WhenCollided(GameObject collidedObj)
    {
        player = collidedObj;
        if (!Input.GetKey(KeyCode.E)) return;

        if (!(Time.time - _lastInteractionTime >= InteractionCooldown)) return;
        InteractWithSphinx();
    }

    private void InteractWithSphinx()
    {
        player.transform.GetChild(2).gameObject.GetComponent<Animator>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
        
        if (dialogueCanvas != null)
        {
            dialogueCanvas.gameObject.SetActive(true);
        }
    }
    
    public void CloseDialogue()
    {
        player.transform.GetChild(2).gameObject.GetComponent<Animator>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
        dialogueCanvas.gameObject.SetActive(false);
    }
}
