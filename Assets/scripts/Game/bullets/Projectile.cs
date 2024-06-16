using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime = 5f;  // Tiempo de vida en segundos
    public GameObject explosionEffect;

    private float lifeTimer;

    protected virtual void Start()
    {
        lifeTimer = lifeTime;
        Initialize();
    }

    protected virtual void Update()
    {
        Move();
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            DestroyProjectile();
        }
    }

    protected virtual void Initialize()
    {
        // Código de inicialización común a todos los proyectiles
    }

    protected virtual void Move()
    {
        // Movimiento común a todos los proyectiles
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
        DestroyProjectile();
    }

    protected virtual void HandleCollision(Collision collision)
    {
        
        // Código de manejo de colisión común a todos los proyectiles
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
