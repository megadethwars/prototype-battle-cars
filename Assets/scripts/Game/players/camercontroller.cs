using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target; // El vehículo a seguir
    public Vector3 offset; // Offset de la cámara
    public float damping = 1f; // Amortiguación del seguimiento
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 desiredPosition = target.position + target.rotation * offset;
        Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;
        transform.position = desiredPosition;
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        
        transform.LookAt(target); // Hace que la cámara mire hacia el vehículo
    }
}
