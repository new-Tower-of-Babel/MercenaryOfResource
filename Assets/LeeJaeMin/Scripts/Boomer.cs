using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AIState
{
    Run = 0,
    Attack
}

public class Boomer : Monster
{
    [SerializeField] private Image explosion;
    private AIState state = AIState.Run;
    private float lastAttackTime;
    

    protected override void Awake()
    {
        base.Awake();
        lastAttackTime = Time.time;
    }
    void Update()
    {

        switch (state)
        {
            case AIState.Run:
                MoveToPlayer();
                AttackCheck();
                break;
            case AIState.Attack:
                ExplosiveAttack();
                break;
        }
       
    }

    private void ExplosiveAttack()
    {
        explosion.fillAmount += 0.5f * Time.deltaTime;
        if (explosion.fillAmount >= 0.8f)
        {
            state = AIState.Run;
            explosion.gameObject.SetActive(false);
            lastAttackTime = Time.time;
        }

    }

    private void AttackCheck()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < data.attackDistance && Time.time - lastAttackTime >= data.attackRate)
        {
            explosion.gameObject.SetActive(true);
            explosion.fillAmount = 0.125f;
            state = AIState.Attack;
        }
    }
}
