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
        Attack();
    }
    
    private void GameInput_OnAimAction (Vector3 mouseWorldPos)
    {
        aimDir = mouseWorldPos - transform.position;
    }

    private void Attack()
    {
        if (!CanAttack)
            return;

        if (Time.time < lastFiredTime + 1f / weaponStats.fireRate)
            return;

        lastFiredTime = Time.time;
        
        if (GameInput.Instance.IsAiming)
        {
            weapon.Fire (firePoint.position, transform.rotation);
        }
        else
        {
            if (GetClosestMonster (out GameObject monster, out Vector3 direction))
            {
                weapon.Fire (firePoint.position, Quaternion.LookRotation (direction));
            }
        }
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
