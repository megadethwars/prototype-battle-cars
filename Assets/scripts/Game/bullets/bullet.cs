using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : Projectile
{
    public float trackingSpeed = 5f;
    public Transform target;
    public float secondsToDestroy = 5f;

    
    protected override void Start()
    {
        // Código de inicialización específico para misiles, no llama al método base
        InitializeBullet();
        StartCoroutine(destroyBulletCourrutine());
    }

    protected override void Update()
    {
        // Lógica específica para misiles, no llama al método base
        TrackTarget();
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }

    private void InitializeBullet()
    {
        // Inicialización específica para misiles
        //Debug.Log("Missile initialized");
    }

    private void TrackTarget()
    {
        //if (target != null)
        //{
        //    Vector3 direction = (target.position - transform.position).normalized;
        //    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), trackingSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    // Movimiento alternativo si no hay objetivo
        //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //}
    }

    protected override void HandleCollision(Collision collision)
    {
        // Manejo de colisión específico para misiles

        //if (explosionEffect != null)
        //{
        //    Instantiate(explosionEffect, transform.position, transform.rotation);
        //}
        //Debug.Log("colisiono");
        DestroyProjectile();
        
    }

    private IEnumerator destroyBulletCourrutine()
    {
        //Debug.Log("destruido");
        yield return new WaitForSeconds(secondsToDestroy);
        
        Destroy(gameObject);

    }
}
