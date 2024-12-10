using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    enum State
    {
        Idle,
        Move
    }
    
    private static readonly int s_AnimID_IsMoving = Animator.StringToHash ("IsMoving");
    private static readonly int s_AnimID_IsAiming = Animator.StringToHash ("IsAiming");

    [SerializeField] private Player m_Player;
    [SerializeField] private Animator m_Animator;
    private State m_State = State.Idle;

    
    public Animator Animator => m_Animator;

    
    private void Update()
    {
        switch (m_State)
        {
        case State.Idle:
            IdleUpdate();
            break;
        
        case State.Move:
            MoveUpdate();
            break;
        }
        
    }

    private void IdleUpdate()
    {
        Vector2 moveInput = GameInput.Instance.GetMovementVectorNormalized();
        bool isAiming = GameInput.Instance.IsAiming;
        
        if (moveInput != Vector2.zero)
        {
            m_State = State.Move;
            m_Animator.SetBool (s_AnimID_IsMoving, true);
        }

        if (isAiming || m_Player.IsShooting)
        {
            m_Animator.SetBool (s_AnimID_IsAiming, true);
        }
        else
        {
            m_Animator.SetBool (s_AnimID_IsAiming, false);
        }
    }

    private void MoveUpdate()
    {
        Vector2 moveInput = GameInput.Instance.GetMovementVectorNormalized();
        bool isAiming = GameInput.Instance.IsAiming;
        
        if (moveInput == Vector2.zero)
        {
            m_State = State.Idle;
            m_Animator.SetBool (s_AnimID_IsMoving, false);
        }
        
        if (isAiming || m_Player.IsShooting)
        {
            m_Animator.SetBool (s_AnimID_IsAiming, true);
        }
        else
        {
            m_Animator.SetBool (s_AnimID_IsAiming, false);
        }
    }
}
