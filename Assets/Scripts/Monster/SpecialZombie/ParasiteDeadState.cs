using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ParasiteDeadState : DeadStateBase
{
    public GameObject explosionImage;
    public ParticleSystem explosionEffect;

    public Image explosionRange;
    public Image explosionGauge;
    public float collisionRange;

    public void Initialize(GameObject imagePrefab)
    {
        explosionImage = imagePrefab;
        explosionRange = imagePrefab.transform.GetChild(0).GetComponent<Image>();
        explosionGauge = imagePrefab.transform.GetChild(1).GetComponent<Image>();
    }

    public override void EnterState(ZombieBase zombie)
    {
        base.EnterState(zombie);

        explosionImage.SetActive(true);

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
        collisionRange = explosionRange.rectTransform.sizeDelta.x;

        while (elapsedTime < animationLength)
        {
            elapsedTime += Time.deltaTime;
            float curSize = Mathf.Lerp(0, collisionRange, elapsedTime / animationLength);
            explosionGauge.rectTransform.sizeDelta = new Vector2(curSize, curSize);
            
            yield return null;
        }

        explosionGauge.rectTransform.sizeDelta = new Vector2 (collisionRange, collisionRange);
    }

    private void TriggerExplosion()
    {
        // 폭발 이펙트 생성
        if (explosionEffect != null)
        {
            explosionEffect.Play();
        }

        // 폭발 범위 내의 객체에 데미지 주기
        Collider[] colliders = Physics.OverlapSphere(zombie.transform.position, collisionRange);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                // player hit
                //PlayDataManager.Instance.characterPlayData.nowHealth--;
            }
            else if(collider.TryGetComponent<ZombieBase>(out var zombie))
            {
                zombie.TakeDamage(10);
            }
        }

        // Disable zombie object
        zombie.gameObject.SetActive(false);
        zombie.Initialize();
        explosionGauge.rectTransform.sizeDelta = new Vector2(0, 0);
    }
}
