using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;
public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelPrincipal;
    FirebaseUser user;
    
    private void Awake()
    {
        GameObject[] panels = GameObject.FindGameObjectsWithTag("UIPanel");
        foreach (GameObject child in panels)
        {
            if (child != panelPrincipal)
            {
                child.SetActive(false);
            }
            
        }
        user = FirebaseAuthManager.Instance.User;
        Debug.Log(user.Email);
    }
    public void jugar()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void opciones()
    {
        Debug.Log("entrando al menu opciones");
    }

    public void atras()
    {
        Debug.Log("Hacia atras");
    }

    public void navigate(GameObject panel)
    {
        // hasrc visibles todos los paneles encontradoes en la escena, menos el panel del argumento
        GameObject[] panels = GameObject.FindGameObjectsWithTag("UIPanel"); 
        foreach(GameObject child in panels)
        {
            if (child != panel)
            {
                
                child.SetActive(false);
            }
            
        }
        panel.SetActive(true);
    }
   
    
}
    