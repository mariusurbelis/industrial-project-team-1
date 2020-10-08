using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    private Player player;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (player.IsMe)
        {
            spriteRenderer.color = Player.playerColor;
            player.photonView.RPC("RPC_SetColor", RpcTarget.AllBuffered, spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b);
        }
    }

    [PunRPC]
    void RPC_SetColor(float r, float g, float b)
    {
        spriteRenderer.color = new Color(r, g, b);
    }

}
