using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private TrailRenderer trailRenderer;

    private Rigidbody rb;
    private float startedTime;

    
    public void Setup(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        
        startedTime = Time.time;
        rb.velocity = transform.forward * moveSpeed;
        
        trailRenderer.Clear();
    }

    
    private void Awake()
    {
         rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (Time.time - startedTime >= lifeTime)
        {
            ObjectPool.Instance.Despawn (gameObject);
            gameObject.SetActive (false);
        }
    }
}
