using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] int sceneID = 1;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
		LoadSceneByID(1);
		
    }

    public static void LoadSceneByID(int ID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"Loading Scene {ID}");
            PhotonNetwork.LoadLevel(ID);
        }
    }

    void Update()
    {
        
    }
}
