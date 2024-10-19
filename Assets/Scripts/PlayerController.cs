using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManager inputManager;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [Header("Gravity Settings")]
    [SerializeField] private float groundedGravity;

    [SerializeField] private float gravity;

    [Header("Jump Settings")]
    [SerializeField] private float initialJumpVel;
    private bool isJumping = false;

    private Vector3 currentVelocity = Vector3.zero;

    private CharacterController characterController;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        HandleMovementInput();
        HandleRun();
        HandleGravity();
        HandleJump();
        characterController.Move(currentVelocity * Time.deltaTime);
    }

    private void HandleMovementInput()
    {
        float verticalVel = inputManager.MoveInput.y;
        float horizontalVel = inputManager.MoveInput.x;

        currentVelocity.x = horizontalVel * walkSpeed; 
        currentVelocity.z = verticalVel * walkSpeed;

        currentVelocity = (transform.TransformDirection(currentVelocity));
    }

    private void HandleRun()
    {
        if( inputManager.getRunInput)
        {
            float verticalVel = inputManager.MoveInput.y;
            float horizontalVel = inputManager.MoveInput.x;
            currentVelocity.x = horizontalVel * runSpeed;
            currentVelocity.z = verticalVel * runSpeed;
            currentVelocity = (transform.TransformDirection(currentVelocity));
        }
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            currentVelocity.y = groundedGravity;
        }
        else
        {
            currentVelocity.y += gravity * Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && inputManager.getJumpInput)
        {
            Debug.Log("trigger");
            isJumping = true;
            currentVelocity.y += Mathf.Sqrt(initialJumpVel * -3.0f * gravity);
        }
        else if (!inputManager.getJumpInput && characterController.isGrounded)
        {
            isJumping = false;
        }
    }
    
}