using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference jumpAction;

    [Tooltip("How high the player jumps")]
    [SerializeField] private float jumpForce = 3f;

    [Tooltip("Offset the raycast to give leeway for the ground check")]
    [SerializeField] private float raycastOffset = 0.1f;

    private Rigidbody rb;
    private Collider collider;

    // 0: left, 1: middle, 2: right
    private int lanes = 1;

    private float distToGround;
    private void OnEnable()
    {
        movementAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        movementAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Awake()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        distToGround = collider.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    /// <summary>
    /// Move the character left or right as long as the player remains in the lanes
    /// </summary>
    private void Movement()
    {
        // TODO:
        // Move the player using the lanes instead of vectors

        var inputVector = movementAction.action.ReadValue<Vector3>();

       if (movementAction.action.triggered)
        {
            // Left
            if (inputVector.x < 0 && lanes > 0)
            {
                lanes--;
                transform.Translate(-transform.right);
            }
            // Right
            else if (inputVector.x > 0 && lanes < 2)
            {
                lanes++;
                transform.Translate(transform.right);
            }
        }
    }

    /// <summary>
    /// Makes the player jump by adding a force upwards
    /// </summary>
    private void Jump()
    {
        if (jumpAction.action.triggered && IsPlayerGrounded())
        {
            // Multiply by jumpForce to get the desired jump height
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Check if the player is allowed to jump
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerGrounded()
    {
        // Shoots a raycast from the center of the player's body to
        // the bounds of its collider + an offset to give some leeway
        return Physics.Raycast(transform.position, Vector3.down, distToGround + raycastOffset);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * (distToGround + raycastOffset));
    }
}