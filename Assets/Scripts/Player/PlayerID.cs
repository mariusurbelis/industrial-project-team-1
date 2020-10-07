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

    [PunRPC]
    void RPC_SetName(string name)
    {
        nameText.text = name;
    }

}
