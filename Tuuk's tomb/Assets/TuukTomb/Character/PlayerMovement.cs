using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    
    private float _horizontalMove;
    private float _verticalMove;
    
    private bool _isRunning = false;
    public float runSpeed = 20f;
    
    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
        
        // Check if the Shift key is pressed to toggle running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isRunning = true;
            runSpeed = 40f; // Set the run speed when Shift is pressed
        }
        else
        {
            _isRunning = false;
            runSpeed = 20f; // Set the default run speed when Shift is released
        }
        
        var movement = new Vector2(_horizontalMove, _verticalMove);
        var combinedSpeed = movement.magnitude;
        
        animator.SetFloat("Speed", combinedSpeed);
        
    }
    
    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _verticalMove * Time.fixedDeltaTime, false, false, _isRunning);
    }
}
