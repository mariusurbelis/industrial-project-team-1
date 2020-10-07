using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectionController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected to {PhotonNetwork.CloudRegion} server");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected. {cause.ToString()}");
    }

    public void ConnectToLobby()
    {
        FindObjectOfType<LobbyController>().Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<LobbyController>().Disconnect();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<LobbyController>().Connect();
        }
    }
}
