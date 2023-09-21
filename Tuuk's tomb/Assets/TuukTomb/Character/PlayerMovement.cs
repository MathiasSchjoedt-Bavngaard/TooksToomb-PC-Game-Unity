using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    
    private float _horizontalMove;
    private float _verticalMove;
    
    public float runSpeed = 40f;
    
    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        _verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
        
        
        var movement = new Vector2(_horizontalMove, _verticalMove);
        var combinedSpeed = movement.magnitude;
        
        animator.SetFloat("Speed", combinedSpeed);
        
    }
    
    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _verticalMove * Time.fixedDeltaTime, false, false);
    }
}
