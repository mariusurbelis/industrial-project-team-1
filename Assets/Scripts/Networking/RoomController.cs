using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{
    public static int maxPlayers = 0;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    /// <summary>
    /// Callback when a room is joined.
    /// </summary>
    public override void OnJoinedRoom()
    {
        LoadSceneByID(1);
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + " " + maxPlayers);
        
        /*if (PhotonNetwork.CurrentRoom.PlayerCount >= maxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }*/
    }

    /// <summary>
    /// Loads a scene for everyone connected to a room.
    /// </summary>
    /// <param name="ID">Build ID of a scene to be loaded</param>
    public static void LoadSceneByID(int ID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Debug.Log($"Loading Scene {ID}");
            PhotonNetwork.LoadLevel(ID);
        }
    }
}
