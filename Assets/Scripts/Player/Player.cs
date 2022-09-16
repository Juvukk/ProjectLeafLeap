using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference movementAction;

    [SerializeField] private InputActionReference jumpAction;

    [Header("Animations")]
    [SerializeField] private Animator playerAni;
    [SerializeField] private AnimatorController playerController;

    [Header("Player Settings")]
    [Tooltip("How high the player jumps")]
    [SerializeField] private float jumpForce = 3f;

    [Tooltip("Offset the raycast to give leeway for the ground check")]
    [SerializeField] private float raycastOffset = 0.1f;

    [SerializeField] private float speed;

    [Header("Lanes")]
    private int lanes = 1;

    [SerializeField] private Transform[] laneTForms;

    [HideInInspector] public bool isPlayerHit { get; private set; } = false;

    [Header("Audio")]
    [SerializeField] private AudioClip effectsJump;
    [SerializeField] private AudioClip effectsMove;

    private ObjectPooling objectPool;
    private Rigidbody rb;
    private Collider collider;
    private Collider otherCollider;

    private float distToGround;

    private AnimatorControllerParameter[] parameters;

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
        objectPool = FindObjectOfType<ObjectPooling>();
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        distToGround = collider.bounds.extents.y;

        parameters = playerController.parameters;
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    /// <summary>
    /// Move the character left or right as long as the player remains in the lanes
    /// </summary>
    private void Movement()
    {
        var inputVector = movementAction.action.ReadValue<Vector3>();

        if (movementAction.action.triggered)
        {
            AudioManager.Instance.PlaySFX(effectsJump);

            // Left
            if (inputVector.x < 0 && lanes > 0)
            {
                lanes--;
                SetAnimBool("IsLeft", true);
            }
            // Right
            else if (inputVector.x > 0 && lanes < 2)
            {
                lanes++;
                SetAnimBool("IsRight", true);
            }
        }
        else if (!movementAction.action.triggered)
        {
            SetAnimBool("IsForward", true);
        }
        Vector3 MovePos = new Vector3(laneTForms[lanes].position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, MovePos, Time.deltaTime * speed);
    }

    /// <summary>
    /// Makes the player jump by adding a force upwards
    /// </summary>
    private void Jump()
    {
        if (jumpAction.action.triggered && IsPlayerGrounded())
        {
            AudioManager.Instance.PlaySFX(effectsJump);
            playerAni.SetTrigger("IsJump");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obs"))
        {
            EventManager.hitEvent?.Invoke();
            otherCollider = other;
            isPlayerHit = true;

            playerAni.SetTrigger("IsHit");
            // play Sfx
            objectPool.ReturnGameObject(other.gameObject);
        }
    }

    public void SetPlayerHit(bool playerHit)
    {
        // To prevent the object pooling timer
        // from overspamming obstacles
        isPlayerHit = playerHit;
    }

    /// <summary>
    /// Set animations true or false
    /// Only works with bool parameters 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isTrue"></param>
    private void SetAnimBool(string name, bool isTrue)
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            if (parameters[i].name == name && parameters[i].type == AnimatorControllerParameterType.Bool)
            {
                playerAni.SetBool(name, isTrue);
            }
            else if(parameters[i].name != name && parameters[i].type == AnimatorControllerParameterType.Bool)
            {
                playerAni.SetBool(parameters[i].name, false);
            }
        }
    }
}