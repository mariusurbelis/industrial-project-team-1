using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerID : MonoBehaviour
{
    private Player player;
    private TextMeshProUGUI nameText;

    private void Awake()
    {
        player = GetComponent<Player>();
        nameText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (player.IsMe)
        {
            nameText.text = player.PlayerName;
            player.photonView.RPC("RPC_SetName", RpcTarget.AllBuffered, player.PlayerName);
        }
    }

    /// <summary>
    /// Sets the player username.
    /// </summary>
    /// <param name="name">Username to set</param>
    [PunRPC]
    void RPC_SetName(string name)
    {
        nameText.text = name;
    }

    public string PlayerDisplayName => nameText.text;
}
