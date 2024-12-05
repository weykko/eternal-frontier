using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private float CameraDistance = 5f;
    [SerializeField] private float CameraZoomSpeed = 2f;
    [SerializeField] private float MinCameraDistance = 2f;
    [SerializeField] private float MaxCameraDistance = 10f;

    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput);

        if (Input.GetKey(KeyCode.Space))
        {
            Velocity.y = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Velocity.y = -1f;
        }

        Controller.Move(moveVector * Speed * Time.deltaTime);
        Controller.Move(Velocity * Speed * Time.deltaTime);

        Velocity.y = 0f;
    }

    private void MovePlayerCamera()
    {
        Vector3 cameraMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (cameraMovement.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraMovement);
            PlayerCamera.rotation = Quaternion.Lerp(PlayerCamera.rotation, targetRotation, CameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            CameraDistance += CameraZoomSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            CameraDistance -= CameraZoomSpeed * Time.deltaTime;
        }

        CameraDistance = Mathf.Clamp(CameraDistance, MinCameraDistance, MaxCameraDistance);
        PlayerCamera.localPosition = new Vector3(0f, 0f, -CameraDistance);
    }
}
