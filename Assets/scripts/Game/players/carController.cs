using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f; // Velocidad del vehículo
    public float turnSpeed = 5f; // Velocidad de giro del vehículo

    public float motorForce = 1500f; // Fuerza del motor
    public float brakeForce = 3000f; // Fuerza de frenado

    private Rigidbody rb;
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


    private void HandleMovement()
    {
        // Movimiento hacia adelante y hacia atrás
        float moveInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * moveInput * speed * Time.deltaTime);
    }

    private void HandleSteeringv1()
    {
        // Giro del vehículo
        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }

    private void HandleMovementAndSteering()
    {
        // Movimiento hacia adelante y hacia atrás
        float moveInput = Input.GetAxis("Vertical");

        if (moveInput != 0)
        {
            // Solo mover y girar si hay input de movimiento
            transform.Translate(Vector3.back * moveInput * speed * Time.deltaTime);

            // Giro del vehículo
            float turnInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
        }
    }

    private void HandleMotor()
    {
        float moveInput = Input.GetAxis("Vertical");
        if (moveInput != 0)
        {
            Vector3 force = -transform.forward * moveInput * motorForce;
            Debug.Log(force);
            rb.AddForce(force);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.deltaTime);
        }
    }

    private void HandleSteering()
    {
        float turnInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(rb.velocity.magnitude) > 0.1f) // Girar solo si el vehículo se está moviendo
        {
            float steering = turnSpeed * turnInput;
            Quaternion turnRotation = Quaternion.Euler(0f, steering, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }


}
