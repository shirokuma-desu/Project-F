using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float AniBlendSpeed = 8.9f;

    [SerializeField] private Transform cameraTransform;

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

    // Start is called before the first frame update
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
        /*RotatePlayer();*/
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
    

    //TO DO: Fix currently bug
    private void RotatePlayer()
    {
        Vector2 lookInput = _inputManager.LookInput;

        // Adjust rotation speed with mouse sensitivity
        float mouseX = lookInput.x * 10f * Time.deltaTime;

        // Rotate the player body only along the Y-axis (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Ensure camera is synchronized for the correct horizontal forward direction
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ignore vertical rotation for player body rotation
        if (cameraForward.magnitude > 0.1f)
        {
            // Smoothly rotate the player to face the same direction as the camera
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
   
}