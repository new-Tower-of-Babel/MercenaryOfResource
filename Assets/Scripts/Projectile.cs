using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField]
    public float MoveSpeed { get; set; }
    
    [field: SerializeField]
    public float LifeTime { get; set; }

    private Rigidbody rb;
    private float startedTime;

    private void Awake()
    {
         rb = GetComponent<Rigidbody>();
         rb.velocity = transform.forward * MoveSpeed;
         startedTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startedTime >= LifeTime)
        {
            Destroy (gameObject);
        }
    }
}
