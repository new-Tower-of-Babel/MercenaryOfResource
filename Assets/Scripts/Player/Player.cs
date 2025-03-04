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
    //private GunslingerStatsSO gunslingerStats;
    //private WeaponStatsSO weaponStats;
    private State state;

    //[SerializeField]private PlayData playData;

    public bool CanAttack { get; set; } = true;

    public bool IsShooting => shooting;

    public Vector3 AimDirection => aimDirection;
    
    public Vector3 TargetDirection => targetDirection;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        weapon = new Weapon();
        //gunslingerStats = PlayerStatsManager.GunslingerStats;
        //weaponStats = PlayerStatsManager.WeaponStats;
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
        //rb.velocity = moveDir * gunslingerStats.moveSpeed;
        rb.velocity = moveDir * PlayDataManager.Instance.characterPlayData.moveSpeed;
        
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
        
        if (GetClosestZombie (out GameObject zombie, out Vector3 direction))
        {
            shooting = true;
            targetDirection = direction;
        }
        else
        {
            shooting = false;
            return;
        }

        if (Time.time < lastFiredTime + 1f / PlayDataManager.Instance.weaponPlayData.fireRate)
            return;

        lastFiredTime = Time.time;
        weapon.Fire (firePoint.position, transform.rotation);
    }
    
    
    private bool GetClosestZombie(out GameObject zombie, out Vector3 direction)
    {
        float closestDistance = float.MaxValue;
        GameObject closestZombie = null;
        Vector3 closestDirection = Vector3.zero;

        var zombieList = ZombieManager.Instance.ActiveZombieList;
        for (int i = 0; i < zombieList.Count; i++)
        {
            Vector3 dir = zombieList[i].transform.position - transform.position;
            float dist = dir.sqrMagnitude;

            // 교전 거리 ?�에?�는 몬스?�만 고려.
            if (dist > engagementDistance * engagementDistance)
            {
                continue;
            }
            
            if (closestDistance > dist)
            {
                closestDistance = dist;
                closestZombie = zombieList[i];
                closestDirection = dir;
            }
        }

        if (closestZombie == null)
        {
            zombie = null;
            direction = Vector3.zero;
            return false;
        }
        else
        {
            zombie = closestZombie;
            direction = closestDirection.normalized;
            return true;
        }
    }
    
    
}
