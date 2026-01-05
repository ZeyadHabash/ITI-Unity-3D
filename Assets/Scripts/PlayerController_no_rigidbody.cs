using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_no_rigidbody : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float mouseSensitivity = 1f;
    private CharacterController characterController;
    private Vector2 moveAmount;
    private Vector2 lookAmount;
    private float verticalRotation = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = transform.TransformDirection(new Vector3(moveAmount.x, 0f, moveAmount.y));
        characterController.Move(move * (speed * Time.deltaTime));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        moveAmount = inputVector;
    }

    public void OnViewSwitch(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        lookAmount = inputVector * mouseSensitivity;
        transform.Rotate(0, lookAmount.x, 0);

        verticalRotation -= lookAmount.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        if (Camera.main != null) Camera.main.transform.rotation = Quaternion.Euler(verticalRotation, transform.rotation.eulerAngles.y, 0);
    }
}