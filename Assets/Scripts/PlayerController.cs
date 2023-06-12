using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    private Camera playerCam; //this is the camera in our game
    private Transform camContainer; //this is the container which we are going to use for rotating the camera

    private float speed = 5;

    [SerializeField]
    float mouseXSensitivity = 1f;
    [SerializeField]
    float mouseYSensitivity = 1f;
    [SerializeField]
    float jumpHeight = 15.0f;
    [SerializeField]
    float invert = 1.0f;
    [SerializeField]
     float walkingSpeed = 5;
    [SerializeField]
     float runningSpeed = 10;

    bool isRunning;
    //animation changes
    private const float ANIMATOR_SMOOTHING = 0.5f;

    private Vector3 animatorInput;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GetComponentInChildren<Camera>(); //This gets us the camera
        camContainer = playerCam.transform.parent; //this gets us the camera's parent's transform
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float yAxis = Input.GetAxis("Mouse Y");
        //we are going to rotate our camera based on our mouse movement
        if (yAxis <  90 && yAxis > -90)
        {
            camContainer.Rotate(invert * yAxis * mouseYSensitivity, 0, 0); //Input.GetAxis("Mouse X/Y") gives us the mouse movement up or down of our character

        }


        float rotationX = Input.GetAxis("Mouse X") * mouseXSensitivity;

        this.transform.Rotate(0, rotationX, 0);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontal, 0, vertical).normalized * speed;
        animatorInput = Vector3.Lerp(animatorInput, input, ANIMATOR_SMOOTHING);

        animator.SetFloat("HorizontalSpeed",animatorInput.x);
        animator.SetFloat("VerticalSpeed", animatorInput.z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = runningSpeed;
            animator.SetBool("Running", isRunning);

        }
        else
        {
            isRunning = false;
            speed = walkingSpeed;
            animator.SetBool("Running", isRunning);
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            input.y = jumpHeight;
            animator.SetTrigger("Jumping");
        }
        else
        {
            input.y = GetComponent<Rigidbody>().velocity.y; //our jump velocity if we are not triggering a new jump is the current rigid body velocity based on its interaction with gravity
        }
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Taunting");
        }
        GetComponent<Rigidbody>().velocity = transform.TransformVector(input);
    }
}
