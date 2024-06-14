using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerHit : EventArgs
{
    public int damage;
    public OnPlayerHit(int damage) { this.damage = damage; }
}
