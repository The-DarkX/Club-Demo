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

    Vector2 scrollDelta;

	void Start()
    {
        targetTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>(); //Find Target

        zoomAmount = new Vector3(0, zoomStep, -zoomStep); //Initial zoom

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;

        if (hideCursor) // Lock and hide cursor
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        scrollDelta = InputManager.scrollDelta;

        HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput()
    {
        if (scrollDelta.y != 0)
        {
            newZoom -= scrollDelta.y * zoomAmount;
        }

        /*
        if (Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }*/
    }

    void HandleMovementInput()
    {
        newPosition = targetTransform.position;
        newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom); //Clamp y zoom
        newZoom.z = -Mathf.Clamp(newZoom.z, minZoom, maxZoom); //Clamp x zoom

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime); //Smooth Movement
        
        //transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime); //Smooth rotation
        
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime); //Smooth zoom

        cameraTransform.LookAt(transform.position); //Looking at player
    }
}