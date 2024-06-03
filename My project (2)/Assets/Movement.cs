using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;

    private Camera cameraChild;
    private CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cameraChild = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Slightly negative to ensure controller is grounded
        }

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Jump initiated");
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraChild.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Debugging
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");
        }

        Debug.Log("Grounded: " + isGrounded + " | Velocity Y: " + velocity.y);
    }
}
