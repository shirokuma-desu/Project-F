using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }


    [SerializeField] private PlayerInput PlayerInput;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool getDashInput { get; private set; }

    public bool getJumpInput { get; private set; }

    public bool getCrouchInput { get; private set; }

    public bool getReloadInput { get; private set; }

    public bool getFireInput { get; private set; }

    public bool getSpecialInput { get; private set; }



    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _dashAction;
    private InputAction _jumpAction;
    private InputAction _crouchAction;
    private InputAction _fireAction;
    private InputAction _reloadAction;
    private InputAction _specialFireAction;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            PlayerInput = GetComponent<PlayerInput>();
            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _dashAction = _currentMap.FindAction("Dash");
            _jumpAction = _currentMap.FindAction("Jump");
            _crouchAction = _currentMap.FindAction("Crouch");
            _fireAction = _currentMap.FindAction("Fire");
            _reloadAction = _currentMap.FindAction("Reload");
            _specialFireAction = _currentMap.FindAction("SecondFire");


            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;

            _lookAction.performed += OnLook;
            _lookAction.canceled += OnLook;

            _dashAction.performed += OnDash;
            _dashAction.canceled += OnDash;


            _jumpAction.performed += OnJump;
            _jumpAction.canceled += OnJump;

            _crouchAction.performed += OnCrouch;
            _crouchAction.canceled += OnCrouch;

            _fireAction.performed += OnFire;
            _fireAction.canceled += OnFire;

            _reloadAction.performed += OnReload;
            _reloadAction.canceled += OnReload;

            _specialFireAction.performed += OnSpecialFire;
            _specialFireAction.canceled += OnSpecialFire;
        }
        
    }



    private void OnEnable()
    {
        _currentMap.Enable();
    }

    private void OnDisable()
    {
        _currentMap.Disable();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            getDashInput = context.ReadValueAsButton();
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

    private void OnReload(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            getReloadInput = context.ReadValueAsButton();
        }
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            getFireInput = context.ReadValueAsButton();
        }
    }

    private void OnSpecialFire(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            getSpecialInput = context.ReadValueAsButton();
        }
    }
}