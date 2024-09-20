using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput PlayerInput;
    private static InputManager instance;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool getRunInput { get; private set; }

    public bool getJumpInput { get; private set; }

    public bool getCrouchInput { get; private set; }

    public static InputManager Instance
    { get { return instance; } }

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _runAction;
    private InputAction _jumpAction;
    private InputAction _crouchAction;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        _currentMap = PlayerInput.currentActionMap;
        _moveAction = _currentMap.FindAction("Move");
        _lookAction = _currentMap.FindAction("Look");
        _runAction = _currentMap.FindAction("Run");
        _jumpAction = _currentMap.FindAction("Jump");
        _crouchAction = _currentMap.FindAction("Crouch");

        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _lookAction.performed += OnLook;
        _lookAction.canceled += OnLook;

        _runAction.performed += OnRun;
        _runAction.canceled += OnRun;

        _jumpAction.performed += OnJump;
        _jumpAction.canceled += OnJump;

        _crouchAction.performed += OnCrouch;
        _crouchAction.canceled += OnCrouch;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _currentMap.Enable();
    }

    private void OnDisable()
    {
        _currentMap.Disable();
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            getRunInput = context.ReadValueAsButton();
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            LookInput = context.ReadValue<Vector2>();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            getJumpInput = context.ReadValueAsButton();
        }
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if( _currentMap is not null)
        {
            getCrouchInput = context.ReadValueAsButton();
        }
    }
}