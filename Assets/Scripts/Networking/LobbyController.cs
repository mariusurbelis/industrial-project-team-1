using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LobbyController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    [SerializeField] private int RoomSize = 20;
    [SerializeField] private GameObject connectButton = null;

    /// <summary>
    /// Once connection is established enables auto scene sync and connects to a lobby.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        connectButton.SetActive(true);
    }

    /// <summary>
    /// Creates a room or joins one if already exists and sets the chosen user's username, stores it in PlayerPrefs.
    /// </summary>
    public void Connect()
    {
        connectButton.SetActive(false);

        PlayerDataManager.ClearData();

        PlayerDataManager.SaveData(PlayerDataManager.PlayerUsername, GameObject.Find("Username InputField").GetComponent<TextMeshProUGUI>().text);
        //PlayerDataManager.SaveData(PlayerDataManager.PlayerColor, $"#{ColorUtility.ToHtmlStringRGB(playerColor)}");
        PlayerDataManager.SaveData(PlayerDataManager.PlayerColor, $"#{ColorUtility.ToHtmlStringRGB(new Color(Random.value, Random.value, Random.value))}");

        //Debug.Log($"Player Color: {PlayerDataManager.LoadData(PlayerDataManager.PlayerColor)}");

        string chosenRoomName = GameObject.Find("Room Name InputField").GetComponent<TextMeshProUGUI>().text;

        //CreateRoom(chosenRoomName);

        PhotonNetwork.JoinOrCreateRoom( chosenRoomName , new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize }, TypedLobby.Default);
        //if (!PhotonNetwork.InRoom) PhotonNetwork.JoinRoom("MainRoom");
    }

    /// <summary>
    /// Callback once something changes in the room list.
    /// </summary>
    /// <param name="roomList">List of rooms</param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        TextMeshProUGUI roomListText = GameObject.Find("INFO").GetComponent<TextMeshProUGUI>();

        roomListText.text = "";

        foreach (RoomInfo room in roomList)
        {
            roomListText.text += $"{room.Name}: {room.PlayerCount}/{room.MaxPlayers}\n";
        }

        if (roomList.Count == 0)
        {
            roomListText.text = "No Rooms Online";
        }
    }

    /// <summary>
    /// Disconnects from the room.
    /// </summary>
    public void Disconnect()
    {
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        //CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //CreateRoom();
    }
}
