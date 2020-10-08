using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static string username = "";

    public PhotonView photonView;
    public Rigidbody2D myBody;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        myBody = GetComponent<Rigidbody2D>();
    }

    public bool IsMe => photonView.IsMine;

    public string PlayerID => photonView.ViewID.ToString();

    public string PlayerName => ((username.Length < 2) ? PlayerID : username);
}
