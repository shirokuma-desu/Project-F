using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float horizontalSpeed = 20f;
    [SerializeField] private float verticalSpeed = 20f;
    [SerializeField] private float clampAngle = 80f;
    private InputManager _inputManager;
    private Vector3 startingRotation;
    protected override void Awake()
    {
        _inputManager = InputManager.Instance;
        startingRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if (_inputManager != null)
                {
                    Vector2 deltaInput = _inputManager.LookInput;
                    Debug.Log(deltaInput);
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0);
                }
            }
        }
    }

}
