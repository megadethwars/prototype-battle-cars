using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyHit : System.EventArgs
{
    public int damage;
    public OnEnemyHit(int damage) { this.damage = damage; }
}
