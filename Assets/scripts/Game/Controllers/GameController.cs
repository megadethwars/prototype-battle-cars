using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject carro;
    private void Awake()
    {
      
    } 
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("Sphere", new Vector3(450, 0, 0), Quaternion.identity);
        Debug.Log("---------TRATANDO DE ISNTANCIAR---");
        //Instantiate(carro, new Vector3(500, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
