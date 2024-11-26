using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;

    private Rigidbody rb;

    private Vector3 aimDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameInput.Instance.OnAimAction += GameInput_OnAimAction;
    }

    private void Update()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
        rb.velocity = moveDir * moveSpeed;
        //transform.position += moveDir * moveSpeed * Time.deltaTime

        Vector3 dir = GameInput.Instance.IsAiming ? aimDir : moveDir; 
        transform.forward = Vector3.Slerp (transform.forward, dir, Time.deltaTime * rotateSpeed);
    }
    
    private void GameInput_OnAimAction (Vector3 mouseWorldPos)
    {
        aimDir = mouseWorldPos - transform.position;
        Debug.Log (mouseWorldPos);
    }

}
