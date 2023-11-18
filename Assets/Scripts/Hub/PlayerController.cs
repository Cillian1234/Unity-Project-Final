using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public Camera playerCamera;

    private float forwardInput;
    private float horizontalInput;
    private Vector3 movementDirection;
    private Vector3 forward;
    private Vector3 right;

    public float walkSpeed;
    public float runSpeed;
    private float yVelocity;
    private float jumpHeight = 15f;
    private float gravity = 0.25f;

    private float mouseRotation;
    private float lookRange = 80f;
    public float sensitivity = 2f;


    private bool isRunning;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        forward = transform.TransformDirection(Vector3.forward);
        right = transform.TransformDirection(Vector3.right);
        grounded = characterController.isGrounded;

        isRunning = Input.GetKey(KeyCode.LeftShift);

        forwardInput = Input.GetAxis("Vertical") * (isRunning ? runSpeed : walkSpeed);
        horizontalInput = Input.GetAxis("Horizontal") * (isRunning ? runSpeed : walkSpeed);
        movementDirection = (forward * forwardInput) + (right * horizontalInput);

        movementDirection.y = yVelocity;

        characterController.Move(movementDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            yVelocity = jumpHeight;
        }
        else if (!grounded)
        {
            yVelocity -= gravity;
        }

        mouseRotation += -Input.GetAxis("Mouse Y") * sensitivity;
        mouseRotation = Mathf.Clamp(mouseRotation, -lookRange, lookRange);
        playerCamera.transform.localRotation = Quaternion.Euler(mouseRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);

    }
}
