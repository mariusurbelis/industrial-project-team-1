﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour
{
    public static PlayerList instance;

    [SerializeField] private GameObject playerPanelPrefab;
    [SerializeField] private Transform playerListTransform;

    private List<GameObject> playerPanels = new List<GameObject>();

    private void Awake() => instance = this;

    private void Start() => StartCoroutine(SpawnPlayerPanels());
        
    private void DrawPlayerPanels()
    {
        for (int i = playerPanels.Count - 1; i >= 0; i--)
        {
            Destroy(playerPanels[i]);
        }

        playerPanels.Clear();

        foreach (Player player in FindObjectsOfType<Player>())
        {
            Debug.Log("Player panel spawned");
            GameObject playerPanel = Instantiate(playerPanelPrefab, playerListTransform);

            playerPanels.Add(playerPanel);

            if (QuizManager.eliminationList.Contains(player.Username))
            {
                playerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.Username;
                playerPanel.transform.Find("Player Image").GetComponent<Image>().color = player.PlayerColor - Color.black * 0.9f;
            }
            else
            {
                playerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.Username;
                playerPanel.transform.Find("Player Image").GetComponent<Image>().color = player.PlayerColor;
            }
        }
    }

    public static void UpdateList()
    {
        instance.DrawPlayerPanels();
    }

    private IEnumerator SpawnPlayerPanels()
    {
        yield return new WaitForSeconds(1f);
        DrawPlayerPanels();
        yield return null;
    }
}
