using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Control")]

    [Space(10)]
    [Header("Player Movement")]
    [Space(2)]
    public GameObject playerBody;
    [SerializeField] float x;
    [SerializeField] float z;
    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float gravity = -9.81f;

    [Space(10)]
    [Header("Grounded Status")]
    [Space(2)]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;


    [Space(10)]
    [Header("Look Control")]
    [Space(2)]
    [SerializeField] float lookSensitivity = 1000f;
    [SerializeField] float xRotation = 0f;
    public float TopClamp = 90.0f;
    public float BottomClamp = -90.0f;


    [Space(10)]
    [Header("Pause UI")]
    [Space(2)]
    [SerializeField] GameObject pauseUI;
    public bool isPaused = false;

    private GameObject playerCamera;
    CharacterController playerCharController;
    Vector3 velocity;
    



    // Start is called before the first frame update
    void Start()
    {
        // Hides the cursor during gameplay
        Cursor.lockState = CursorLockMode.Locked;

        playerCharController = playerBody.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        PauseMenu();

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

        playerBody.transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, BottomClamp, TopClamp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void PlayerMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0.0f, z).normalized;

        inputDirection = transform.right * x + transform.forward * z;

        playerCharController.Move(inputDirection * moveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        playerCharController.Move(velocity * Time.deltaTime);
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !isPaused)
        {
            pauseUI.SetActive(true);
            isPaused = true;
            Cursor.lockState = CursorLockMode.Confined;

        } 
        else if (Input.GetKeyDown(KeyCode.F1) && isPaused)
        {
            pauseUI.SetActive(false);
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }


    }

    
}
