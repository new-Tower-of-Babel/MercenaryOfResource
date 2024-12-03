using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private enum State
    {
        Standard,
        AutoTargeting,
        ManualAiming,
    }
    
    
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float engagementDistance = 20f;

    private Rigidbody rb;

    private bool shooting;
    private Vector3 targetDirection;
    private Vector3 aimDirection;
    private float lastFiredTime = 0f;
    private WeaponBase weapon;
    private GunslingerStatsSO gunslingerStats;
    private WeaponStatsSO weaponStats;
    private State state;

    public bool CanAttack { get; set; } = true;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        weapon = new Revolver();
        gunslingerStats = PlayerStatsManager.GunslingerStats;
        weaponStats = PlayerStatsManager.WeaponStats;
    }

    private void Start()
    {
        GameInput.Instance.OnAimAt += GameInput_OnAimAt;
    }

    private void FixedUpdate()
    {
        // move
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
        rb.velocity = moveDir * gunslingerStats.moveSpeed;
        
        // turn
        Vector3 dir;
        
        if (GameInput.Instance.IsAiming)
            dir = aimDirection;
        else
        {
            if (shooting)
                dir = targetDirection;
            else
                dir = moveDir;
        }
        
        transform.forward = Vector3.Slerp (transform.forward, dir, Time.deltaTime * rotateSpeed);
    }

    private void Update()
    {
        Attack();
    }
    
    private void GameInput_OnAimAt (Vector3 mouseWorldPos)
    {
        aimDirection = (mouseWorldPos - transform.position).normalized;
    }

    private void Attack()
    {
        if (!CanAttack)
            return;
        
        if (GetClosestMonster (out GameObject monster, out Vector3 direction))
        {
            shooting = true;
            targetDirection = direction;
        }
        else
        {
            shooting = false;
            return;
        }

        if (Time.time < lastFiredTime + 1f / weaponStats.fireRate)
            return;

        lastFiredTime = Time.time;
        weapon.Fire (firePoint.position, transform.rotation);
    }
    
    
    private bool GetClosestMonster(out GameObject monster, out Vector3 direction)
    {
        float closestDistance = float.MaxValue;
        GameObject closestMonster = null;
        Vector3 closestDirection = Vector3.zero;

        var monsters = MonsterSpawner.Instance.ActiveMonsters;
        for (int i = 0; i < monsters.Count; i++)
        {
            Vector3 dir = monsters[i].transform.position - transform.position;
            float dist = dir.sqrMagnitude;

            // 교전 거리 안에있는 몬스터만 고려.
            if (dist > engagementDistance * engagementDistance)
            {
                continue;
            }
            
            if (closestDistance > dist)
            {
                closestDistance = dist;
                closestMonster = monsters[i];
                closestDirection = dir;
            }
        }

        if (closestMonster == null)
        {
            monster = null;
            direction = Vector3.zero;
            return false;
        }
        else
        {
            monster = closestMonster;
            direction = closestDirection.normalized;
            return true;
        }
    }
}
