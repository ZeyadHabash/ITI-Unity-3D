using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private TMPro.TMP_Text hitText;
    [SerializeField] LayerMask interactablesLayerMask;
    private Rigidbody rb;
    private Vector2 moveAmount;
    private string lastHitName = "";
    private int interactablesLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactablesLayer = interactablesLayerMask.value;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(moveAmount.x * (speed * Time.deltaTime), 0f, moveAmount.y * (speed * Time.deltaTime));
        rb.linearVelocity += move;
        if (move != Vector3.zero)
            rb.MoveRotation(Quaternion.LookRotation(move));

        float minDistance = Mathf.Infinity;
        string hitName = "";
        for (float yOffset = -1f; yOffset <= 1f; yOffset += 0.5f)
        {
            Vector3 origin = transform.position + Vector3.up * yOffset;
            RaycastHit hit;
            if (Physics.Raycast(origin, transform.forward, out hit, Mathf.Infinity, interactablesLayer))
            {
                if (hit.distance < minDistance)
                {
                    minDistance = hit.distance;
                    hitName = hit.collider.gameObject.name;
                }
            }
        }
        if (!string.IsNullOrEmpty(hitName))
        {
            lastHitName = hitName;
        }
        hitText.text = lastHitName;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        moveAmount = inputVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (float yOffset = -1f; yOffset <= 1f; yOffset += 0.5f)
        {
            Vector3 origin = transform.position + Vector3.up * yOffset;
            Gizmos.DrawRay(origin, transform.forward * 10f);
        }
    }
}