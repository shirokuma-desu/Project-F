using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput PlayerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }

    private InputActionMap _currentMap;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _runAction;

    private void Awake()
    {
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
            Look = context.ReadValue<Vector2>();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (_currentMap is not null)
        {
            Move = context.ReadValue<Vector2>();
        }
    }
}
