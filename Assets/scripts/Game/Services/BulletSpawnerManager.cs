using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerManager : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Start()
    {
        EventManager.Instance.Subscribe<OnShoot>(OnShoot);
    }

    void OnDestroy()
    {
        EventManager.Instance.Unsubscribe<OnShoot>(OnShoot);
    }

    void OnShoot(EventArgs e)
    {
        OnShoot shootEvent = e as OnShoot;
        if (shootEvent != null && bulletPrefab != null)
        {
            Instantiate(bulletPrefab, shootEvent.position, shootEvent.rotation);
            //Debug.Log("Bullet fired!");
        }
    }
}
