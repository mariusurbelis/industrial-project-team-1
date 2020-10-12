using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomJoinArea : MonoBehaviour
{
    [SerializeField] private GameObject gameStartButton;

    private int maxPlayers = 1;

    private int playersInsideTheArea = 0;

    private void Awake() => gameStartButton.GetComponent<Button>().onClick.AddListener(LoadTheGame);

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            gameStartButton.SetActive(playersInsideTheArea >= maxPlayers);
        }
    }

    private void LoadTheGame()
    {
        RoomController.LoadSceneByID(2);
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
