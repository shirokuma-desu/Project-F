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
    public bool Run { get; private set; }
    public static InputManager Instance
    { get { return instance; } }

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _runAction;

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

        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _lookAction.performed += OnLook;
        _lookAction.canceled += OnLook;

        _runAction.performed += OnRun;
        _runAction.canceled += OnRun;
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
            Run = context.ReadValueAsButton();
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
}