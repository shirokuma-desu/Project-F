using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed;

    [SerializeField] public Transform orientation;
    [SerializeField] public float groundDrag;
    [SerializeField] public float jumpForce;
    [SerializeField] public float jumpCooldown;
    [SerializeField] public float airMultiplier;

    [Header("LayerMask")]
    [SerializeField] public float playerHeight;

    [SerializeField] public LayerMask groundMask;
    [SerializeField] private bool isGround;
    [SerializeField] private bool isJump;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        GroundCheck();
        SpeedControl();
        GetInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
       
        if (Input.GetKey(KeyCode.Space) && !isJump && isGround)
        {
            isJump = true;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
            
    }

    private void HandleMovement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGround)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (isJump)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Impulse);
        }
        
    }

    private void GroundCheck()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
        if (isGround)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void SpeedControl()
    {
        Vector3 currentVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (currentVel.magnitude > moveSpeed)
        {
            {
                Vector3 limitedVel = currentVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }
    private void ResetJump()
    {
        isJump = false;
    }
}