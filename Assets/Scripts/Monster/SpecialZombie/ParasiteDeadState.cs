using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ParasiteDeadState : DeadStateBase
{
    public GameObject explosionEffect;  // Explode effect
    public Image explosionCircle;       // Explode circle image
    public float explosionRadius = 5f;  // Range

    public override void EnterState(ZombieBase zombie)
    {
        base.EnterState(zombie);

        explosionCircle.fillAmount = 0.0f;

        // Disable zombie after animation using coroutine
        zombie.StartCoroutine(PlayDeathAnimationAndExplode());
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private IEnumerator PlayDeathAnimationAndExplode()
    {
        // Dead anim trigger
        zombie.animator.SetTrigger("Dead");

        // Get current animation length
        float animationLength = zombie.animator.GetCurrentAnimatorStateInfo(0).length;

        // Explode circle coroutine
        zombie.StartCoroutine(ExplosionCoroutine(animationLength));

        // Wait animation time
        yield return new WaitForSeconds(animationLength);

        TriggerExplosion();
    }

    private IEnumerator ExplosionCoroutine(float animationLength)
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationLength)
        {
            elapsedTime += Time.deltaTime;
            explosionCircle.fillAmount = Mathf.Clamp01(elapsedTime / animationLength);
            yield return null;
        }

        explosionCircle.fillAmount = 1f;
    }

    private void TriggerExplosion()
    {
        // 폭발 이펙트 생성
        if (explosionEffect != null)
        {
            UnityEngine.Object.Instantiate(explosionEffect, zombie.transform.position, Quaternion.identity);
        }

        // 폭발 범위 내의 객체에 데미지 주기
        Collider[] colliders = Physics.OverlapSphere(zombie.transform.position, explosionRadius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                // player hit
            }
        }

        // Disable zombie object
        zombie.gameObject.SetActive(false);
        zombie.Initialize();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(zombie.transform.position, explosionRadius);
    }
}
