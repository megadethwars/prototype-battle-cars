using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public Text statusText;
    //public InputField roomNameInput;
    public Button createRoomButton;
    public Button joinRoomButton;
    public InputField roomCreated;
    public InputField roomJoined;
    //public Button listRoomsButton;
    //public Button leaveRoomButton;

    private void Start()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
        createRoomButton.onClick.AddListener(() => CreateRoom(roomCreated.text.Trim()));
        joinRoomButton.onClick.AddListener(() => JoinRoom(roomJoined.text.Trim()));
        //listRoomsButton.onClick.AddListener(ListRooms);
        //leaveRoomButton.onClick.AddListener(LeaveRoom);
        PhotonNetwork.SendRate = 20; // Envía datos 20 veces por segundo
        PhotonNetwork.SerializationRate = 10; // Serializa datos 10 veces por segundo
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor de Photon");
        statusText.text = "Conectado al servidor de Photon";
        PhotonNetwork.NickName = "megadethwars";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado del servidor de Photon: " + cause);
        statusText.text = "Desconectado del servidor de Photon: " + cause;
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.PlayerTtl = 60000; // 60 segundos (1 minuto)

        // Mantener la sala activa por un tiempo incluso si todos los jugadores se desconectan temporalmente
        roomOptions.EmptyRoomTtl = 300000;
        if (roomName == "")
        {
            statusText.text = "invalid room";
            return;
        }
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Sala creada exitosamente");
        statusText.text = "Sala creada exitosamente";
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error al crear la sala: " + message);
        statusText.text = "Error al crear la sala: " + message;
    }

    public void JoinRoom(string roomName)
    {
        if (roomName == "")
        {
            statusText.text = "invalid room";
            return;
        }
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a la sala: " + PhotonNetwork.CurrentRoom.Name);
        statusText.text = "Unido a la sala: " + PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error al unirse a la sala: " + message);
        statusText.text = "Error al unirse a la sala: " + message;
    }

    public void ListRooms()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log("Sala: " + roomInfo.Name + " | Jugadores: " + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers);
            statusText.text += "\nSala: " + roomInfo.Name + " | Jugadores: " + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Has dejado la sala");
        statusText.text = "Has dejado la sala";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
