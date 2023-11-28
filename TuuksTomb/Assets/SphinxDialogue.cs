using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (_player != null)
        {
            var childTransform = _player.transform.GetChild(2);
            if (childTransform != null)
            {
                var childGameObject = childTransform.gameObject;
                var animatorComponent = childGameObject.GetComponent<Animator>();
            
                if (animatorComponent != null)
                {
                    animatorComponent.enabled = false;
                }
            }

            var playerRigidbody = _player.GetComponent<Rigidbody2D>();
        
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
            }

            var playerMovementComponent = _player.GetComponent<PlayerMovement>();
        
            if (playerMovementComponent != null)
            {
                playerMovementComponent.enabled = false;
            }
        }

        if (dialogueCanvas != null)
        {
            dialogueCanvas.gameObject.SetActive(true);
        }
    }

    
    public void CloseDialogue()
    {
        if (_player != null)
        {
            var childTransform = _player.transform.GetChild(2);
            if (childTransform != null)
            {
                var childGameObject = childTransform.gameObject;
                var animatorComponent = childGameObject.GetComponent<Animator>();
            
                if (animatorComponent != null)
                {
                    animatorComponent.enabled = true;
                }
            }

            var playerMovementComponent = _player.GetComponent<PlayerMovement>();
        
            if (playerMovementComponent != null)
            {
                playerMovementComponent.enabled = true;
            }
        }
        if (dialogueCanvas != null)
        {
            dialogueCanvas.gameObject.SetActive(false);
        }
    }
}
