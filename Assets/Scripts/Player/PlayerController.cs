using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float AniBlendSpeed = 8.9f;

    [SerializeField] private Transform CameraRoot;
    [SerializeField] private Transform Camera;

    [SerializeField] private float UpperLimit = -40f;
    [SerializeField] private float BottomLimit = 70f;
    [SerializeField] private float MouseSensitivity = 21.9f;

    private float _xRotation;

    private Rigidbody _playerRb;

    private InputManager _inputManager;

    private Animator _animator;

    private bool _hasAnimator;

    private int _xVelHash;
    private int _yVelHash;

    private const float _walkSpeed = 2f;

    private const float _runSpeed = 6f;

    private Vector2 _currentVel;

    private void Start()
    {
        _hasAnimator = TryGetComponent<Animator>(out _animator);
        _playerRb = GetComponent<Rigidbody>();
        _inputManager = InputManager.Instance;


        _xVelHash = Animator.StringToHash("X_Velocity");
        _yVelHash = Animator.StringToHash("Y_Velocity");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CamMovements();
    }

    private void Move()
    {
        if (!_hasAnimator) return;

        float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;

        if (_inputManager.MoveInput == Vector2.zero) targetSpeed = 0;

        _currentVel.x = Mathf.Lerp(_currentVel.x,targetSpeed * _inputManager.MoveInput.x, AniBlendSpeed * Time.fixedDeltaTime);
        _currentVel.y = Mathf.Lerp(_currentVel.y, targetSpeed * _inputManager.MoveInput.y, AniBlendSpeed * Time.fixedDeltaTime);

        var xVelDiff = _currentVel.x - _playerRb.velocity.x;
        var zVelDiff = _currentVel.y - _playerRb.velocity.z;

        _playerRb.AddForce(transform.TransformVector(new Vector3(xVelDiff,0, zVelDiff)),ForceMode.VelocityChange);
        _animator.SetFloat(_xVelHash, _currentVel.x);
        _animator.SetFloat(_yVelHash, _currentVel.y);
    }

    private void CamMovements()
    {
        if (!_hasAnimator) return;

        var Mouse_X = _inputManager.LookInput.x;
        var Mouse_Y = _inputManager.LookInput.y;
        Camera.transform.position = CameraRoot.position;


        _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
        _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

        Camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _playerRb.MoveRotation(_playerRb.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
    }

}