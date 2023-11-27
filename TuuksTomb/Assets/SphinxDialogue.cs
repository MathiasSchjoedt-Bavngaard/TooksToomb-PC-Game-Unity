using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxDialogue : CollidableObject
{
    private bool _interacted;
    
    private float _lastInteractionTime;
    private const float InteractionCooldown = 4.0f;
    private GameObject _player;
    
    
    public Canvas dialogueCanvas;
    
    protected override void WhenCollided(GameObject collidedObj)
    {
        _player = collidedObj;
        if (!Input.GetKey(KeyCode.E)) return;

        if (!(Time.time - _lastInteractionTime >= InteractionCooldown)) return;
        InteractWithSphinx();
    }

    private void InteractWithSphinx()
    {
        _player.transform.GetChild(2).gameObject.GetComponent<Animator>().enabled = false;
        _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _player.GetComponent<PlayerMovement>().enabled = false;
        
        if (dialogueCanvas != null)
        {
            dialogueCanvas.gameObject.SetActive(true);
        }
    }
    
    public void CloseDialogue()
    {
        _player.transform.GetChild(2).gameObject.GetComponent<Animator>().enabled = true;
        _player.GetComponent<PlayerMovement>().enabled = true;
        dialogueCanvas.gameObject.SetActive(false);
    }
}
