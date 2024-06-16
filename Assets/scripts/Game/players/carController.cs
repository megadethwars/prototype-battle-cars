using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class carController : MonoBehaviour
{
    // Start is called before the first frame update

    public CarData carData;


    //public float motorForce = 10000f; // Fuerza del motor
    //public float maxMotorForce = 20000f; // Fuerza máxima del motor

    //public float brakeForce = 1f; // Fuerza de frenado

    //public float maxTurnSpeed = 30f; // Velocidad máxima de giro del vehícul>o
    //public float minTurnSpeed = 10f; // Velocidad mínima de giro del vehículo

    //public float maxSpeedForStableTurning = 50f; // Velocidad máxima para giro estable
    //public float instabilityFactor = 2f; // Factor de inestabilidad a altas velocidades

    //public float acceleration = 100f; // Incremento de fuerza por segundo
    //public float maxSpeed = 500f; // Velocidad máxima del vehículo

    //public float airDrag = 0f; // Drag cuando el objeto está en el aire
    //public float groundDrag = 2f;

    //public int score;
    //public int health = 100;

    bool isGrounded=false;

    public Transform bulletSpawnPoint;
    private void Awake()
    {
        carData = new CarData();
        Debug.Log(carData.acceleration);
    }

    private Rigidbody rb;
    private float currentMotorForce;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = carData.airDrag;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            HandleMotor();
        }
        
        HandleSteering();
 
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("ground"))
        {
            rb.drag = carData.groundDrag; // Aplicar drag cuando está en colisión con el plano
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            rb.drag = carData.airDrag; // Eliminar drag cuando no está en colisión con el plano
            isGrounded = false;
        }
    }


    

    

    private void HandleMotor()
    {
        
        float moveInput = Input.GetAxis("Vertical");

        

        if (Input.GetKeyDown(KeyCode.W))
        {

            currentMotorForce = 0.0f;
           
            
        }

        // Detectar si la tecla W es liberada
        if (Input.GetKeyUp(KeyCode.W))
        {

            currentMotorForce = 0.0f;
        }

        if (moveInput > 0)
        {
            // Incrementar la fuerza del motor gradualmente
            currentMotorForce += carData.acceleration * Time.deltaTime;
            currentMotorForce = Mathf.Clamp(currentMotorForce, 0, carData.maxMotorForce);
          
        }
        else if (moveInput < 0)
        {
        //    // Incrementar la fuerza del motor gradualmente en reversa
            currentMotorForce -= carData.acceleration * Time.deltaTime;
            currentMotorForce = Mathf.Clamp(currentMotorForce, -carData.maxMotorForce, 0);
         
        }
        else
        {
            // Reducir gradualmente la fuerza del motor cuando no se presiona ninguna tecla
            currentMotorForce = Mathf.Lerp(currentMotorForce, 0, Time.deltaTime * 10f);
        }

        if (moveInput != 0)
        {
            Vector3 force = -transform.forward  * currentMotorForce;
            if (rb.velocity.magnitude < carData.maxSpeed)
            //if (rb.velocity.magnitude < maxSpeed || Vector3.Dot(rb.velocity, transform.forward) < 0)
            {
                rb.AddForce(force);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, carData.brakeForce * Time.deltaTime);
        }
    }

    private void HandleSteering()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float currentSpeed = rb.velocity.magnitude;

        // Calcular la velocidad de giro proporcional a la velocidad del vehículo
        float speedFactor = Mathf.Clamp(currentSpeed / carData.maxSpeedForStableTurning, 0f, 1f);
        float turnSpeed = Mathf.Lerp(carData.minTurnSpeed, carData.maxTurnSpeed, speedFactor);

        if (Mathf.Abs(currentSpeed) > 0.05f) // Girar solo si el vehículo se está moviendo
        {
            float steering = turnSpeed * turnInput * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, steering, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }


        // Simular inestabilidad a altas velocidades
        if (currentSpeed > carData.maxSpeedForStableTurning && Mathf.Abs(turnInput) > 0.1f)
        {
            //rb.AddForce(Vector3.right * turnInput * instabilityFactor, ForceMode.Impulse);
        }
    }

    void OnPlayerHit(EventArgs e)
    {
        OnPlayerHit eventArgs = e as OnPlayerHit;
        carData.health -= eventArgs.damage;
        Debug.Log("Player hit! Health: " + carData.health);
    }

    void Shoot()
    {
        // Implement shooting logic here
        // If enemy is hit:
        //EventManager.Instance.TriggerEvent<OnEnemyHit>(new OnEnemyHit(10));
       
        if (bulletSpawnPoint != null)
        {
            EventManager.Instance.TriggerEvent<OnShoot>(new OnShoot(bulletSpawnPoint.position, bulletSpawnPoint.rotation));
            //Debug.Log("Shoot event triggered");
        }
    }


    public void SendDataToServer()
    {
        StartCoroutine(SendDataCoroutine());
    }

    private IEnumerator SendDataCoroutine()
    {
        string json = JsonUtility.ToJson(carData);
        UnityWebRequest www = UnityWebRequest.Put("https://yourserver.com/api/playerstate", json);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data sent successfully");
        }
        else
        {
            Debug.Log("Error sending data: " + www.error);
        }
    }


}
