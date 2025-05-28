using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float rotationSmoothTime = 0.1f;
    public float jumpForce = 8f;
    public float gravity = 20f;

    public Transform cameraTransform;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    public Animator animator;

    private CharacterController controller;
    private Vector3 velocity;
    private float turnSmoothVelocity;
    private bool isGrounded;
    private bool isJumping;
    private float originalStepOffset;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalStepOffset = controller.stepOffset;
    }

    void Update()
    {
        GroundCheck();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        animator.SetBool("run", direction.magnitude > 0.1f);
        animator.SetBool("sprint", isSprinting);
        animator.SetBool("air", !isGrounded);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            isJumping = true;
            velocity.y = jumpForce;
            animator.Play("Armature|jump");
        }

        // Apply gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ensures we stay grounded
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        // Reset jump when grounded
        if (isGrounded && isJumping)
        {
            isJumping = false;
        }

        animator.SetFloat("Speed", direction.magnitude);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        controller.stepOffset = isGrounded ? originalStepOffset : 0f;
    }
}
