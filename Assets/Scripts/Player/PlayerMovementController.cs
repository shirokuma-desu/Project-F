using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerMovementData movementData;

    public int currentStamina = 3;
    public int maxStamina = 3;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 dashDirection = Vector3.zero;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovementInput();
        ApplyGravity();
        ApplyAirAccel();
        HandleJump();
        characterController.Move(currentVelocity * Time.deltaTime);
        HandleDash();
    }

    private void HandleMovementInput()
    {
        float verticalVel = InputManager.Instance.MoveInput.y;
        float horizontalVel = InputManager.Instance.MoveInput.x;

        currentVelocity.x = horizontalVel * movementData.speed;
        currentVelocity.z = verticalVel * movementData.speed;

        currentVelocity = (transform.TransformDirection(currentVelocity));
    }

    private void HandleDash()
    {
        if (InputManager.Instance.getDashInput && !movementData.isDashing && InputManager.Instance.MoveInput != Vector2.zero)
        {
            { 
                StartCoroutine(Dash());
            }
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            currentVelocity.y = movementData.groundedGravity;
        }
        else
        {
            currentVelocity.y += movementData.gravity * Time.deltaTime;
        }
    }

    private void ApplyAirAccel()
    {
        if (!characterController.isGrounded)
        {
            //apply air drag
            currentVelocity.x = currentVelocity.x * movementData.airSpeed;
            currentVelocity.z = currentVelocity.z * movementData.airSpeed;
        }
    }

    private void HandleJump()
    {
        if (!movementData.isJumping && characterController.isGrounded && InputManager.Instance.getJumpInput)
        {
            movementData.isJumping = true;
            currentVelocity.y += Mathf.Sqrt(movementData.initialJumpVel * -3.0f * movementData.gravity);
        }
        else if (!InputManager.Instance.getJumpInput && characterController.isGrounded)
        {
            movementData.isJumping = false;
        }
    }

    private IEnumerator Dash()
    {
        Vector2 input = InputManager.Instance.MoveInput.normalized;
        dashDirection = new Vector3(input.x, 0, input.y);
        dashDirection = transform.TransformDirection(dashDirection);

        float startTime = Time.time;
        Vector3 initialVelocity = currentVelocity;

        while (Time.time < startTime + movementData.dashDuration)
        {
            currentVelocity = dashDirection * movementData.dashSpeed;
            characterController.Move(currentVelocity * Time.deltaTime);
            yield return null;
        }
        currentVelocity = initialVelocity;
        movementData.isDashing = true;
        Debug.Log(initialVelocity);
        yield return new WaitForSeconds(movementData.dashCoolDown);
        movementData.isDashing = false;
    }
}