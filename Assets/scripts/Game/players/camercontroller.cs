using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target; // El vehículo a seguir
    public Vector3 offset; // Offset de la cámara
    public float damping = 1f; // Amortiguación del seguimiento
    public float rotationSmoothTime = 0.1f; // Tiempo de suavizado para la rotación
    public float smoothTime = 0.3f;

    public float proportionalGain = 1.0f; // Ganancia proporcional
    public float integralGain = 0.1f; // Ganancia integral
    private float integralSum;

    private Vector3 velocity = Vector3.zero;
    private float offsetZ=0.0f;

    void Start()
    {
        Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
        offsetZ = desiredPosition.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;


        // Suavizar la posición de la cámara
        //transform.position = desiredPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.LookAt(target); // Hace que la c�mara mire hacia el veh�culo

        //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        
        // Suavizar la rotación de la cámara
        //Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothTime);
    }

    
}
