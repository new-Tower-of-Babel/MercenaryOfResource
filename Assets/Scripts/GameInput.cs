using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event Action<Vector3> OnAimAction = (mouseWorldPos) => { };
    
    private Controls controls;
    private Camera mainCamera;
    
    public bool IsAiming { get; private set; } 

    private void Awake()
    {
        Instance = this;

        mainCamera = Camera.main;
        
        controls = new Controls();
        controls.Player.AimStart.started += AimStarted;
        controls.Player.AimStart.canceled += AimCanceled;
        controls.Player.Aim.performed += Aim;
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
        controls.Player.AimStart.started -= AimStarted;
        controls.Player.AimStart.canceled -= AimCanceled;
        controls.Player.Aim.performed -= Aim;
    }

    private void Aim (InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = context.ReadValue<Vector2>();
        Ray mouseCameraRay = mainCamera.ScreenPointToRay (mouseScreenPos);
        Plane plane = new Plane (Vector3.up, Vector3.zero);

        if (plane.Raycast (mouseCameraRay, out float distance))
        {
            Vector3 mouseWorldPos = mouseCameraRay.GetPoint (distance);
            OnAimAction?.Invoke (mouseWorldPos);
        }
    }

    private void AimStarted (InputAction.CallbackContext context)
    {
        IsAiming = true;
    }
    
    private void AimCanceled (InputAction.CallbackContext context)
    {
        IsAiming = false;
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = controls.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
