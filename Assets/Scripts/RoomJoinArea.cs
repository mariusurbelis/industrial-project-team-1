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

    /// <summary>
    /// Loads the main game scene.
    /// </summary>
    private void LoadTheGame()
    {
        RoomController.LoadSceneByID(2);
    }

    /// <summary>
    /// Increases the players inside the area counter.
    /// </summary>
    /// <param name="collision">Collider that enters the trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersInsideTheArea++;
        }
    }

    /// <summary>
    /// Decreases the players inside the area counter.
    /// </summary>
    /// <param name="collision">Collider that exits the trigger</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersInsideTheArea--;
        }
    }
}
