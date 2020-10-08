using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static string username = "";
    public static Color playerColor;

    public PhotonView photonView;
    public Rigidbody2D myBody;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (playerColor == null)
        {
            playerColor = new Color(Random.value, Random.value, Random.value);
        }

        gameObject.AddComponent<PlayerAvatar>();
        gameObject.AddComponent<PlayerID>();
    }

    public bool IsMe => photonView.IsMine;

    public string PlayerID => photonView.ViewID.ToString();

    public string PlayerName => ((username.Length < 2) ? PlayerID : username);
}
