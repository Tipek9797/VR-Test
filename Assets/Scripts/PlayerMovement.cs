using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 7f;
    public float acceleration = 6f;
    public float deceleration = 6f;
    public float airControl = 0.1f;
    public float friction = 8f;
    public float landingSlowdown = 15f; 

    public float sensitivity = 2f;
    public float fovIncrease = 15f;
    public float fovChangeSpeed = 5f;

    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 velocity;
    private Vector3 moveVelocity;
    private bool isGrounded;
    private bool wasInAir;
    private float defaultFOV;
    private float verticalRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        defaultFOV = playerCamera.fieldOfView;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        if (isGrounded && wasInAir)
        {
            moveVelocity = Vector3.MoveTowards(moveVelocity, Vector3.zero, landingSlowdown * Time.deltaTime);
        }

        wasInAir = !isGrounded;

        float targetSpeed = isGrounded && Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveInput = (transform.right * moveX + transform.forward * moveZ).normalized;

        float currentAcceleration = isGrounded ? acceleration : acceleration * airControl;
        float currentFriction = isGrounded ? friction : 0f;

        if (moveInput.magnitude > 0)
        {
            moveVelocity = Vector3.MoveTowards(moveVelocity, moveInput * targetSpeed, currentAcceleration * Time.deltaTime);
        }
        else
        {
            moveVelocity = Vector3.MoveTowards(moveVelocity, Vector3.zero, currentFriction * Time.deltaTime);
        }

        controller.Move(moveVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) velocity.y = jumpForce;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        float targetFOV = (isGrounded && Input.GetKey(KeyCode.LeftShift)) ? defaultFOV + fovIncrease : defaultFOV;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, fovChangeSpeed * Time.deltaTime);
    }
}