using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Mouse setting")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField, Range(90f, 0f)] private float maxYRotation;
    [SerializeField, Range(-90f, 0f)] private float minYRotation;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform cameraHolderRef;


    private void Awake()
    {
        newCameraRotation = cameraHolderRef.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;
    }
    void Start()
    {

    }

    void Update()
    {
        HandleMouseInput();

    }


    private void HandleMouseInput()
    {
        //handle horizontal rotation
        newCharacterRotation.y += InputManager.Instance.LookInput.x * sensX * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);

        //handle vertical rotation
        newCameraRotation.x += sensX * Time.deltaTime * -InputManager.Instance.LookInput.y;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, minYRotation, maxYRotation);
        cameraHolderRef.localRotation = Quaternion.Euler(newCameraRotation);
    }
}
