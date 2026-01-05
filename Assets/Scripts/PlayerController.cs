using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody rb;
    private Vector2 moveAmount;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(moveAmount.x * (speed * Time.deltaTime), 0f, moveAmount.y * (speed * Time.deltaTime));
        rb.linearVelocity +=  move;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");  
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        moveAmount = inputVector;
    }
}