using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomJoinArea : MonoBehaviour
{
    [SerializeField] private GameObject gameStartButton;

    private int maxPlayers = 2;

    private int playersInsideTheArea = 0;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            gameObject.SetActive(playersInsideTheArea == maxPlayers);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersInsideTheArea++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersInsideTheArea--;
        }
    }
}
