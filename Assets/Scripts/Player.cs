using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private Transform firePoint;

    private Rigidbody rb;

    private Vector3 aimDir;
    private float lastFiredTime = 0f;
    private WeaponBase weapon;
    private GunslingerStatsSO gunslingerStats;
    private WeaponStatsSO weaponStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        weapon = new Revolver();
        gunslingerStats = PlayerStatsManager.GunslingerStats;
        weaponStats = PlayerStatsManager.WeaponStats;
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
        rb.velocity = moveDir * gunslingerStats.moveSpeed;
        
        // turn
        Vector3 dir = GameInput.Instance.IsAiming ? aimDir : moveDir;
        transform.forward = Vector3.Slerp (transform.forward, dir, Time.deltaTime * rotateSpeed);
    }

    private void Update()
    {
        if (GameInput.Instance.IsAiming)
        {
            if (Time.time >= lastFiredTime + 1f / weaponStats.fireRate)
            {
                lastFiredTime = Time.time;
                weapon.Fire (firePoint.position, transform.rotation);
            }
            
        }
    }
    
    private void GameInput_OnAimAction (Vector3 mouseWorldPos)
    {
        aimDir = mouseWorldPos - transform.position;
    }
}
