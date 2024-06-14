using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;

    

    void Start()
    {
        EventManager.Instance.Subscribe<OnEnemyHit>(OnEnemyHit2);
        Debug.Log("suscrito a evento");
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe<OnEnemyHit>(OnEnemyHit2);
    }


    void OnEnemyHit2(EventArgs e)
    {   

        OnEnemyHit eventArgs = e as OnEnemyHit;
        health -= eventArgs.damage;
        Debug.Log("Player hit! Health: " + health);
    }

}
