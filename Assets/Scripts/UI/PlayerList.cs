using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour
{
    [SerializeField] private GameObject playerPanelPrefab;
    [SerializeField] private Transform playerListTransform;

    private void Start()
    {
        StartCoroutine(SpawnPlayerPanels());
    }

    private void InstantiatePlayerPanels()
    {
        foreach (Player player in FindObjectsOfType<Player>())
        {
            Debug.Log("Player panel spawned");
            GameObject playerPanel = Instantiate(playerPanelPrefab, playerListTransform);
            playerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.PlayerName;
            playerPanel.transform.Find("Player Image").GetComponent<Image>().color = player.playerColor;
        }
    }

    private IEnumerator SpawnPlayerPanels()
    {
        yield return new WaitForSeconds(1f);
        InstantiatePlayerPanels();
        yield return null;
    }
}
