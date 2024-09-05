using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float senX;
    [SerializeField] private float senY;

    [SerializeField] public Transform orientation;
    [SerializeField] public Transform cameraPosition;

    private float xRotation;
    private float yRotation;
    [SerializeField] private float maxRotationAngles = 90f;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = cameraPosition.position;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * senY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -maxRotationAngles, maxRotationAngles);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}