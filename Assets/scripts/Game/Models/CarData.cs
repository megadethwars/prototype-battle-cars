using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : ScriptableObject
{
    public int health = 100;
    public int score = 0;
    public float motorForce = 1000000f;
    public float maxMotorForce = 2000000f;
    public float brakeForce = 1f;
    public float maxTurnSpeed = 30f;
    public float minTurnSpeed = 10f;
    public float maxSpeedForStableTurning = 50f;
    public float instabilityFactor = 2f;
    public float acceleration = 800f;
    public float maxSpeed = 10000f;
    public float airDrag = 0f;
    public float groundDrag = 2f;

    public CarData()
    {

    }
}
