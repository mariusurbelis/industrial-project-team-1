using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    [SerializeField] private int RoomSize = 20;
    [SerializeField] private GameObject connectButton;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        connectButton.SetActive(true);
    }

    public void Connect()
    {
        connectButton.SetActive(false);
        PhotonNetwork.JoinOrCreateRoom("MainRoom", new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize }, TypedLobby.Default);
        //if (!PhotonNetwork.InRoom) PhotonNetwork.JoinRoom("MainRoom");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        TextMeshProUGUI roomListText = GameObject.Find("INFO").GetComponent<TextMeshProUGUI>();

        roomListText.text = "";

        foreach (RoomInfo room in roomList)
        {
            roomListText.text += $"{room.Name}: {room.PlayerCount}/{room.MaxPlayers}";
        }

        if (roomList.Count == 0)
        {
            roomListText.text = "No Rooms Online";
        }
    }

    public void Disconnect()
    {
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating a room");
        //int randomRoomNumber = UnityEngine.Random.Range(10000, 99999);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        //PhotonNetwork.CreateRoom("Room-" + randomRoomNumber, roomOptions);
        PhotonNetwork.CreateRoom("MainRoom", roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
}
