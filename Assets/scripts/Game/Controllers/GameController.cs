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
        float randomx = Random.Range(200.0f, 500.0f);
        float randomz = Random.Range(0.0f, 100.0f);
        Debug.Log(randomx);
        GameObject player = PhotonNetwork.Instantiate("Sphere", new Vector3(randomx, 0, randomz), Quaternion.identity);
        Debug.Log("---------TRATANDO DE ISNTANCIAR---");
        //Instantiate(carro, new Vector3(500, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
