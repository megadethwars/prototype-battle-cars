using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject carro;
    private void Awake()
    {
      
    } 
    void Start()
    {
        float randomx = Random.Range(100.0f, 500.0f);
        float randomz = Random.Range(0.0f, 200.0f);
       
        //GameObject player = PhotonNetwork.Instantiate("Sphere", new Vector3(randomx, 0, randomz), Quaternion.identity);
        GameObject player = PhotonNetwork.Instantiate("carrito", new Vector3(randomx, 0, randomz), Quaternion.identity);
        Debug.Log("---------TRATANDO DE INSTANCIAR---");
        //Instantiate(carro, new Vector3(500, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Jugador {otherPlayer.NickName} ha dejado la sala");
        // Eliminar los objetos asociados con el jugador que se desconectó
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView photonView = obj.GetComponent<PhotonView>();
            if (photonView != null && photonView.Owner == otherPlayer)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(obj);
                }
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Soy el nuevo Master Client");
        }
    }
}
