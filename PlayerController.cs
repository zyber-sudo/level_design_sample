using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Control")]

    // Fields and information pertaining to the stats of the player for movement in Unity.
    [Space(10)]
    [Header("Player Movement")]
    [Space(2)]

    // Gameplay object 
    public GameObject playerBody;
    // Players x-axis value.
    [SerializeField] float x;
    // Players z-axis value.
    [SerializeField] float z;
    // Field to adjust the player move speed for debugging.
    [SerializeField] float moveSpeed = 4.0f;
    // Field to adjust the player's gravity speed for debugging.
    [SerializeField] float gravity = -9.81f;


    // Fields and information pertaining to the testing of the player ground check-in Unity.
    [Space(10)]
    [Header("Grounded Status")]
    [Space(2)]

    // Shows the state of the isGrounded bool in Engine.
    [SerializeField] bool isGrounded;
    // Field for the transform components for the ground check sphere.
    [SerializeField] Transform groundCheck;
    // Sets the distance the ground check is for the player.
    [SerializeField] float groundDistance = 0.4f;
    // Interactive mask for the ground in the Unity Engine.
    [SerializeField] LayerMask groundMask;


    // Fields and information pertaining to the stats of the player look control in Unity.
    [Space(10)]
    [Header("Look Control")]
    [Space(2)]

    // Field to adjust and view the mouse look sensitivity.
    [SerializeField] float lookSensitivity = 1000f;
    // Shows the rotation speed (FOR DEBUGGING).
    [SerializeField] float xRotation = 0f;
    // Field to set the angle clamp value for looking up in Engine.
    public float TopClamp = 90.0f;
    // Field to set the angle clamp value for looking down in Engine.
    public float BottomClamp = -90.0f;


    
    [Space(10)]
    [Header("Pause UI")]
    [Space(2)]
    
    [SerializeField] GameObject pauseUI;
    public bool isPaused = false;


    // UNCATEGORIZED VARIABLES

    //Variable for the CharacterController component.
    CharacterController playerCharController;
    // Variable for velocity Vector3.
    Vector3 velocity;
    



    // Start is called before the first frame update
    void Start()
    {
        // Hides the cursor during gameplay
        Cursor.lockState = CursorLockMode.Locked;

        // Accesses the Character Controller 
        playerCharController = playerBody.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Runs pause menu function.
        PauseMenu();

        // Checks if the state of the game is not paused.
        if (!isPaused)
        {
            MouseLook();
            PlayerMovement();
        }
    }


    void MouseLook()
    {
        // Assigns both the x and y axies for the mouse
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

        // Rotates the player along the X-axis based on the axis input information.
        playerBody.transform.Rotate(Vector3.up * mouseX);
        
        
        xRotation -= mouseY;
        // Clamps the up and down view control.
        xRotation = Mathf.Clamp(xRotation, BottomClamp, TopClamp);

        // Controls the up and down mouse view.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void PlayerMovement()
    {
        // Uses trigger sphere to check if the player object is grounded.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // Stops the velocity from counting up when grounded.
            velocity.y = -2f;
        }

        // Gets the axes for the movement of the player (Not the look action).
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //Initializes a Vector3 to access change movement using the axes accessed.
        Vector3 inputDirection = new Vector3(x, 0.0f, z).normalized;

        // Sets the direction of the new Vector3 created.
        inputDirection = transform.right * x + transform.forward * z;

        // Moves the player based on the direction set above.
        playerCharController.Move(inputDirection * moveSpeed * Time.deltaTime);
        // Sets the player's gravity. 
        velocity.y += gravity * Time.deltaTime;

        // Enacts the gravity on the player.
        playerCharController.Move(velocity * Time.deltaTime);
    }

    
    void PauseMenu()
    {
        // Checks if the pause state has not been activated.
        if (Input.GetKeyDown(KeyCode.F1) && !isPaused)
        {
            // Sets the pause object to active.
            pauseUI.SetActive(true);
            // Changes the check bool for pause state to true.
            isPaused = true;
            // Unlocks the cursor for selecting in the pause menu.
            Cursor.lockState = CursorLockMode.Confined;

        } 
        else if (Input.GetKeyDown(KeyCode.F1) && isPaused)
        {
            // Sets the pause object to inactive.
            pauseUI.SetActive(false);
            // Changes the check bool for pause state to false.
            isPaused = false;
            // Locks and hides the cursor for gameplay.
            Cursor.lockState = CursorLockMode.Locked;
        }


    }

    
}
