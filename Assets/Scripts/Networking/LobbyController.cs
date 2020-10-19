using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LobbyController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    //[SerializeField] private int RoomSize = 20;
    [SerializeField] private GameObject connectButton = null;
    [SerializeField] private Slider playerCountSlider = null;
    [SerializeField] private TextMeshProUGUI sliderValue = null;

    private void Start()
    {
        UpdateSliderText();
    }

    /// <summary>
    /// Once connection is established enables auto scene sync and connects to a lobby.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        ToggleConnectButton(true);
    }

    /// <summary>
    /// Creates a room or joins one if already exists and sets the chosen user's username, stores it in PlayerPrefs.
    /// </summary>
    public void Connect()
    {
        switch (MenuController.state)
        {
            case MenuController.State.Host:
                Host();
                break;
            case MenuController.State.Join:
                Join();
                break;
        }
    }

    public void Join()
    {
        ToggleConnectButton(false);
        PlayerDataManager.ClearData();
        PlayerDataManager.SaveData(PlayerDataManager.PlayerUsername, GameObject.Find("Username InputField").GetComponent<TextMeshProUGUI>().text);
        PlayerDataManager.SaveData(PlayerDataManager.PlayerColor, $"#{ColorUtility.ToHtmlStringRGB(new Color(Random.value, Random.value, Random.value))}");
        string chosenRoomName = GameObject.Find("Room Name InputField").GetComponent<TextMeshProUGUI>().text.ToUpper();

        string finalRoomCode = "";

        for (int i = 0; i < 4; i++)
            finalRoomCode = string.Concat(finalRoomCode, chosenRoomName[i]);

        if (!PhotonNetwork.InRoom) PhotonNetwork.JoinRoom(finalRoomCode);
        //Debug.Log($"{finalRoomCode} ({finalRoomCode.Length})");
    }

    public void Host()
    {
        ToggleConnectButton(false);
        PlayerDataManager.ClearData();
        PlayerDataManager.SaveData(PlayerDataManager.PlayerUsername, GameObject.Find("Username InputField").GetComponent<TextMeshProUGUI>().text);
        PlayerDataManager.SaveData(PlayerDataManager.PlayerColor, $"#{ColorUtility.ToHtmlStringRGB(new Color(Random.value, Random.value, Random.value))}");
        //string chosenRoomName = GameObject.Find("Room Name InputField").GetComponent<TextMeshProUGUI>().text;

        string chosenRoomName = GenerateRoomCode();

        //CreateRoom(chosenRoomName);

        PhotonNetwork.JoinOrCreateRoom(chosenRoomName, new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)(int)playerCountSlider.value }, TypedLobby.Default);
        //if (!PhotonNetwork.InRoom) PhotonNetwork.JoinRoom("MainRoom");

        //Debug.Log($"{chosenRoomName} ({chosenRoomName.Length})");
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

    public void UpdateSliderText()
    {
        sliderValue.text = $"Max Players: {playerCountSlider.value}";
        RoomController.maxPlayers = (int)playerCountSlider.value;
    }

    private void ToggleConnectButton(bool active)
    {
        connectButton.SetActive(active);
    }

    private string GenerateRoomCode()
    {
        string finalRoomCode = "";

        for (int i = 0; i < 4; i++)
        {
            finalRoomCode = string.Concat(finalRoomCode, (char)('a' + Random.Range(0, 26)));
        }

        return finalRoomCode.ToUpper();
    }

    /// <summary>
    /// Disconnects from the room.
    /// </summary>
    public void Disconnect()
    {
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        ToggleConnectButton(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //CreateRoom();
    }
}
