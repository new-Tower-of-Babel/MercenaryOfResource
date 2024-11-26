using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gunslinger : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float fireDelay = 0.3f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private Rigidbody rb;

    private Vector3 aimDir;
    private float lastFiredTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameInput.Instance.OnAimAction += GameInput_OnAimAction;
    }

    private void FixedUpdate()
    {
        // move
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
        rb.velocity = moveDir * moveSpeed;
        
        // turn
        Vector3 dir = GameInput.Instance.IsAiming ? aimDir : moveDir;
        transform.forward = Vector3.Slerp (transform.forward, dir, Time.deltaTime * rotateSpeed);
    }

    private void Update()
    {
        if (GameInput.Instance.IsAiming)
        {
            if (Time.time >= lastFiredTime + fireDelay)
            {
                lastFiredTime = Time.time;
                if (projectilePrefab != null)
                {
                    var obj = ObjectPool.Instance.Spawn ("Projectile");
                    obj.GetComponent<Projectile>().Setup (firePoint.position, transform.rotation);
                    obj.SetActive (true);
                }
            }
            
        }
    }
    
    private void GameInput_OnAimAction (Vector3 mouseWorldPos)
    {
        aimDir = mouseWorldPos - transform.position;
    }
}
