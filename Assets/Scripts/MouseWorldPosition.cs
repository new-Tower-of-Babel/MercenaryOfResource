using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWorldPosition : MonoBehaviour
{
    public static MouseWorldPosition Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        
    }

    public Vector3 GetPosition()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray mouseCameraRay = MainCamera.Instance.Get().ScreenPointToRay (mouseScreenPos);
        Plane plane = new Plane (Vector3.up, Vector3.zero);

        if (plane.Raycast (mouseCameraRay, out float distance))
        {
            return mouseCameraRay.GetPoint (distance);
        }

        return Vector3.zero;
    }
}
