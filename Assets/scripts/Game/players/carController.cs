using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    // Start is called before the first frame update
    
    

    //public float motorForce = 10000; // Fuerza del motor
    public float maxMotorForce = 20000f; // Fuerza máxima del motor

    public float brakeForce = 1f; // Fuerza de frenado

    public float maxTurnSpeed = 30f; // Velocidad máxima de giro del vehícul>o
    public float minTurnSpeed = 10f; // Velocidad mínima de giro del vehículo

    public float maxSpeedForStableTurning = 50f; // Velocidad máxima para giro estable
    public float instabilityFactor = 2f; // Factor de inestabilidad a altas velocidades

    public float acceleration = 100f; // Incremento de fuerza por segundo
    public float maxSpeed = 50f; // Velocidad máxima del vehículo

    private Rigidbody rb;
    private float currentMotorForce;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    HandleMovementAndSteering();
    //}

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
    }


    //private void HandleMovement()
    //{
    //    // Movimiento hacia adelante y hacia atrás
    //    float moveInput = Input.GetAxis("Vertical");
    //    transform.Translate(Vector3.forward * moveInput * speed * Time.deltaTime);
    //}

    //private void HandleSteeringv1()
    //{
    //    // Giro del vehículo
    //    float turnInput = Input.GetAxis("Horizontal");
    //    transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    //}

    //private void HandleMovementAndSteering()
    //{
    //    // Movimiento hacia adelante y hacia atrás
    //    float moveInput = Input.GetAxis("Vertical");

    //    if (moveInput != 0)
    //    {
    //        // Solo mover y girar si hay input de movimiento
    //        transform.Translate(Vector3.back * moveInput * speed * Time.deltaTime);

    //        // Giro del vehículo
    //        float turnInput = Input.GetAxis("Horizontal");
    //        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    //    }
    //}

    private void HandleMotor()
    {
        float moveInput = Input.GetAxis("Vertical");

        if (moveInput > 0)
        {
            // Incrementar la fuerza del motor gradualmente
            currentMotorForce += acceleration * Time.deltaTime;
            //currentMotorForce = Mathf.Clamp(currentMotorForce, 0, maxMotorForce);
        }
        else if (moveInput < 0)
        {
        //    // Incrementar la fuerza del motor gradualmente en reversa
            currentMotorForce -= acceleration * Time.deltaTime;
        //    currentMotorForce = Mathf.Clamp(currentMotorForce, -maxMotorForce, 0);
        }
        else
        {
            // Reducir gradualmente la fuerza del motor cuando no se presiona ninguna tecla
            currentMotorForce = Mathf.Lerp(currentMotorForce, 0, Time.deltaTime * 5);
        }

        if (moveInput != 0)
        {
            Vector3 force = -transform.forward  * currentMotorForce;
            if (rb.velocity.magnitude < maxSpeed)
            //if (rb.velocity.magnitude < maxSpeed || Vector3.Dot(rb.velocity, transform.forward) < 0)
            {
                rb.AddForce(force);
            }
        }
    }

    private void HandleSteering()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float currentSpeed = rb.velocity.magnitude;

        // Calcular la velocidad de giro proporcional a la velocidad del vehículo
        float speedFactor = Mathf.Clamp(currentSpeed / maxSpeedForStableTurning, 0f, 1f);
        float turnSpeed = Mathf.Lerp(minTurnSpeed, maxTurnSpeed, speedFactor);

        if (Mathf.Abs(currentSpeed) > 0.05f) // Girar solo si el vehículo se está moviendo
        {
            float steering = turnSpeed * turnInput * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, steering, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }


        // Simular inestabilidad a altas velocidades
        if (currentSpeed > maxSpeedForStableTurning && Mathf.Abs(turnInput) > 0.1f)
        {
            rb.AddForce(Vector3.right * turnInput * instabilityFactor, ForceMode.Impulse);
        }
    }


}
