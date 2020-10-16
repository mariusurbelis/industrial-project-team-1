using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentMode : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Dev Test Room", new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)(int)2 }, TypedLobby.Default);
    }

    private int yAxisOffset = 10;

    void OnGUI()
    {
        yAxisOffset = 100;

        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Room: {PhotonNetwork.CurrentRoom.Name}");
        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Players: {PhotonNetwork.CurrentRoom.PlayerCount}");
        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Health: {Player.Me?.health}");
        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Selection: {Player.Me?.selectedOption}");
        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Correct: {QuizManager.currentCorrectAnswerID}");
    }
}
