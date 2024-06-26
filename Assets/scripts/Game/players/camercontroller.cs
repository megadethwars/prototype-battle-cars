using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target; // El vehículo a seguir
    public Vector3 offset; // Offset de la cámara
    public float damping = 1f; // Amortiguación del seguimiento
    public float rotationSmoothTime = 0.1f; // Tiempo de suavizado para la rotación
    public float smoothTime = 0.3f;

    public float proportionalGain = 1.0f; // Ganancia proporcional
    public float integralGain = 0.1f; // Ganancia integral
    private float integralSum;

    private Vector3 velocity = Vector3.zero;
    private float offsetZ = 0.0f;

    void Start()
    {
        FindLocalPlayer();
    }

    void FindLocalPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PhotonView photonView = player.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                target = player.transform;
                Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;
                transform.position = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
                offsetZ = desiredPosition.z;
                Debug.Log("Jugador local encontrado: " + player.name);
                return;
            }
        }
        Debug.Log("Jugador local no encontrado, buscando...");
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            FindLocalPlayer();
        }

        if (target != null)
        {
            Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
            transform.LookAt(target);
        }
    }


}
