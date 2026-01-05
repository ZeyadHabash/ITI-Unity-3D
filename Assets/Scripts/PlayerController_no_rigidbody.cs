using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_no_rigidbody : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float thirdPersonDistance = 5f;  // Distance behind player in third-person
    [SerializeField] private float thirdPersonHeight = 2f;    // Height offset for third-person camera
    [SerializeField] private Animator animator;
    private CharacterController characterController;
    private Vector2 moveAmount;
    private Vector2 lookAmount;
    private float verticalRotation = 0f;
    private bool isThirdPerson = false;  // Toggle for view mode
    private Vector3 FPSCameraPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        FPSCameraPosition = Camera.main.transform.localPosition;
        animator.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = transform.TransformDirection(new Vector3(moveAmount.x, 0f, moveAmount.y));
        characterController.Move(move * (speed * Time.deltaTime));
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }

    // This should all probably be in a camera controller or something
    void LateUpdate()
    {
        if (Camera.main == null) return;

        if (isThirdPerson)
        {
            // Third-person: Position camera behind player and look at them
            Vector3 desiredPosition = transform.position - transform.forward * thirdPersonDistance + Vector3.up * thirdPersonHeight;
            Camera.main.transform.position = desiredPosition;
            Camera.main.transform.LookAt(transform.position + Vector3.up * 2f);  // Look at player's approximate head height
        }
        else
        {
            // First-person: Camera at player position, rotation handled in OnLook
            Camera.main.transform.localPosition = FPSCameraPosition;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        moveAmount = inputVector;
    }

    public void OnViewSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)  // Only toggle on button press (not release)
        {
            isThirdPerson = !isThirdPerson;
            Debug.Log("Switched to " + (isThirdPerson ? "Third-Person" : "First-Person") + " view");
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        lookAmount = inputVector * mouseSensitivity;

        transform.Rotate(0, lookAmount.x, 0);
        if (!isThirdPerson)
        {
            verticalRotation -= lookAmount.y;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            Camera.main.transform.rotation = Quaternion.Euler(verticalRotation, transform.rotation.eulerAngles.y, 0);
        }
    }
}