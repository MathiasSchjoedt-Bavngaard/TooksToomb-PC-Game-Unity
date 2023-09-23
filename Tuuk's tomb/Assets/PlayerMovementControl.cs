using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    
    public CharacterController2D controller;
    
    public VectorValue playerStartPosition;
    float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
        //set player position
        transform.position = PlayerPrefs.HasKey("x") ? new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y")) : playerStartPosition.initialValue;}
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if( Input.GetButtonDown("Jump") )
        {
            jump = true;
        }
        
    }
    
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    
}
