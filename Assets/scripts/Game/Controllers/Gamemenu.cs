using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenuPanel;
    bool isPaused = false;
    void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Reanudar el juego
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo esté normalizado
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("LobbyScene"); // Asegúrate de que la escena del menú principal esté correctamente nombrada
    }

    public void QuitGame()
    {
        Application.Quit(); // Esto solo funciona en una build del juego, no en el editor
    }


}
