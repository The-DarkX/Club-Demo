using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Settings")]
    public float movementTime = 5;
    public float zoomStep = 10f;
    public bool hideCursor = false;

    [Header("Limits")]
    public float minZoom = 10f;
    public float maxZoom = 30f;

    Transform targetTransform;

    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;

    Vector3 rotateStartPosition;
    Vector3 rotateCurrentPosition;

    Vector3 zoomAmount;

    void Start()
    {
        targetTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();

        zoomAmount = new Vector3(0, zoomStep, -zoomStep);

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;

        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom -= Input.mouseScrollDelta.y * zoomAmount;
        }

        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    void HandleMovementInput()
    {
        newPosition = targetTransform.position;
        newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
        newZoom.z = -Mathf.Clamp(newZoom.z, minZoom, maxZoom);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);

        cameraTransform.LookAt(transform.position);
    }
}