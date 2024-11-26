using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;

    private Rigidbody rb;
    private float startedTime;

    
    public void Setup(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        
        startedTime = Time.time;
        rb.velocity = transform.forward * moveSpeed;
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
