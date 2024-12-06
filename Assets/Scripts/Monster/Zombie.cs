using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zombie : MonoBehaviour
{
    //public Animator Animator { get; private set; }
    //public CharacterController Controller { get; private set; }
    ////public ZombieAnimationData AnimationData { get; private set; }

    //private void Awake()
    //{
    //    Animator = GetComponentInChildren<Animator>();
    //    Controller = GetComponent<CharacterController>();
    //}

    //private void Start()
    //{
    //    //AnimationData.Initialize();
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    private IState currentState;

    public float health = 100f;
    public Transform player;
    private Animator animator;

    public IdleState idleState;
    public ChasingState chasingState;
    public AttackingState attackingState;
    public DeadState deadState;

    void Start()
    {
        animator = GetComponent<Animator>();

        idleState = new IdleState(this);
        chasingState = new ChasingState(this);
        attackingState = new AttackingState(this);
        deadState = new DeadState(this);

        currentState = idleState;
        currentState.Enter();
    }

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(IState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public Transform GetPlayer() => player;
    public Animator GetAnimator() => animator;

    public void TakeDamage(float damage)
    {
        if (currentState != deadState)
        {
            health -= damage;
            if (health <= 0)
            {
                ChangeState(deadState);
            }
        }
    }
}
