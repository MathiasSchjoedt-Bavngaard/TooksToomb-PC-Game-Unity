using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public bool sideView = false;


    private float _horizontalMove;
    private float _verticalMove;

    private bool _jump = false;
    private bool _crouching = false;
    private bool _isRunning = false;
    public float runSpeed = 20f;

    void Start()
    {
        if (!PlayerPrefs.HasKey("x"))
            return;
        if (sideView)
        {
            if (!PlayerPrefs.HasKey("y"))
                return;
            transform.position = new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
        }
        //if topdown set player position to x and z
        else
        {
            if (!PlayerPrefs.HasKey("z"))
                return;
            transform.position =
                new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("z"));
        }
    }

    // Update is called once per frame
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

        switch (_verticalMove)
        {
            case > 0 when sideView:
                _jump = true;
                animator.SetBool("Jumping", true);
                OnCrouching(_crouching);
                break;
            case < 0 when sideView:
                _crouching = true;
                OnCrouching(_crouching);
                break;
            default:
                _jump = false;
                _crouching = false;
                OnCrouching(_crouching);
                break;
        }
        

        var movement = new Vector2(_horizontalMove, _verticalMove);
        var combinedSpeed = movement.magnitude;

        animator.SetFloat("Speed", combinedSpeed);
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("Crouching", isCrouching);
        if(_isRunning || isCrouching == false) 
            animator.SetBool("Sliding", isCrouching);
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _verticalMove * Time.fixedDeltaTime, _crouching, _jump,
            _isRunning, sideView);
    }
}