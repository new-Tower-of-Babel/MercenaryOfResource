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
    private static readonly int s_AnimID_IsMovingBack= Animator.StringToHash ("IsMovingBack");
    private static readonly int s_AnimID_IsMovingLeftStrafe= Animator.StringToHash ("IsMovingLeftStrafe");
    private static readonly int s_AnimID_IsMovingRightStrafe= Animator.StringToHash ("IsMovingRightStrafe");

    [SerializeField] private Player m_Player;
    [SerializeField] private Animator m_Animator;
    private State m_State = State.Idle;

    
    public Animator Animator => m_Animator;

    
    private void Update()
    {
        Vector2 moveInput = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveVector = new Vector3 (moveInput.x, 0f, moveInput.y);
        bool isAiming = GameInput.Instance.IsAiming;

        if (moveInput != Vector2.zero) {
            m_Animator.SetBool (s_AnimID_IsMoving, true);
            
            if (isAiming || m_Player.IsShooting) {
                float dot;
                Vector3 cross;
                
                if (isAiming) {
                    dot = Vector3.Dot (m_Player.AimDirection, moveVector);
                    cross = Vector3.Cross (m_Player.AimDirection, moveVector);
                } else {
                    dot = Vector3.Dot (m_Player.TargetDirection, moveVector);
                    cross = Vector3.Cross (m_Player.TargetDirection, moveVector);
                }
                
                //Debug.Log ($"{m_Player.AimDirection}x{moveVector}={cross}");

                m_Animator.SetBool (s_AnimID_IsMovingBack, false);
                m_Animator.SetBool (s_AnimID_IsMovingLeftStrafe, false);
                m_Animator.SetBool (s_AnimID_IsMovingRightStrafe, false);
                
                if (dot < -0.7f) {
                    m_Animator.SetBool (s_AnimID_IsMovingBack, true);
                } else if (dot >= -0.7f && dot <= 0.7f) {
                    if (cross.y > 0) {
                        m_Animator.SetBool (s_AnimID_IsMovingRightStrafe, true);
                    } else if (cross.y < 0){
                        m_Animator.SetBool (s_AnimID_IsMovingLeftStrafe, true);
                    }
                }
            }
        } else {
            m_Animator.SetBool (s_AnimID_IsMoving, false);
            m_Animator.SetBool (s_AnimID_IsMovingBack, false);
            m_Animator.SetBool (s_AnimID_IsMovingLeftStrafe, false);
            m_Animator.SetBool (s_AnimID_IsMovingRightStrafe, false);
        }
        
        if (isAiming || m_Player.IsShooting) {
            m_Animator.SetBool (s_AnimID_IsAiming, true);
        } else {
            m_Animator.SetBool (s_AnimID_IsAiming, false);
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
