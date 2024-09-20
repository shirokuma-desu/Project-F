using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{

    [SerializeField] private ExampleCharacterCamera OrbitCamera;
    [SerializeField] private Transform CameraFollowPoint;
    [SerializeField] private CharacterController Character;
    [SerializeField] private InputManager inputManager;

    private Vector3 _lookInputVector = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Tell camera to follow transform
        OrbitCamera.SetFollowTransform(CameraFollowPoint);

        // Ignore the character's collider(s) for camera obstruction checks
        OrbitCamera.IgnoredColliders = Character.GetComponentsInChildren<Collider>().ToList();

        inputManager = InputManager.Instance;
       
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterInput();
    }

    private void LateUpdate()
    {
       HandleCameraInput();
    }

    private void HandleCameraInput()
    {
        // Create the look input vector for the camera
        var mouseLookAxisUp = inputManager.LookInput.y;
        var mouseLookAxisRight = inputManager.LookInput.x;
        _lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

        // Prevent moving the camera while the cursor isn't locked
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            _lookInputVector = Vector3.zero;
        }

        

        OrbitCamera.UpdateWithInput(Time.deltaTime, 0, _lookInputVector);
    }

    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        // Build the CharacterInputs struct
        characterInputs.MoveAxisForward = inputManager.MoveInput.y;
        characterInputs.MoveAxisRight = inputManager.MoveInput.x;
        characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
        characterInputs.JumpDown = inputManager.getJumpInput;
        characterInputs.OnCrouch = inputManager.getCrouchInput;
        // Apply inputs to character
        Character.SetInputs(ref characterInputs);
    }
}
