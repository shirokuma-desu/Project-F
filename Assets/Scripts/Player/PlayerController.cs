using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float AniBlendSpeed = 8.9f;

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
        _inputManager = GetComponent<InputManager>();

        _xVelHash = Animator.StringToHash("X_Velocity");
        _yVelHash = Animator.StringToHash("Y_Velocity");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!_hasAnimator) return;

        float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;

        if (_inputManager.Move == Vector2.zero) targetSpeed = 0;

        _currentVel.x = Mathf.Lerp(_currentVel.x,targetSpeed * _inputManager.Move.x, AniBlendSpeed * Time.fixedDeltaTime);
        _currentVel.y = Mathf.Lerp(_currentVel.y, targetSpeed * _inputManager.Move.y, AniBlendSpeed * Time.fixedDeltaTime);

        var xVelDiff = _currentVel.x - _playerRb.velocity.x;
        var zVelDiff = _currentVel.y - _playerRb.velocity.z;

        _playerRb.AddForce(transform.TransformVector(new Vector3(xVelDiff,0, zVelDiff)),ForceMode.VelocityChange);
        _animator.SetFloat(_xVelHash, _currentVel.x);
        _animator.SetFloat(_yVelHash, _currentVel.y);
    }
}