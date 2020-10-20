using TMPro;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    [SerializeField] private GameObject playerPanelPrefab;

    private void Start()
    {
        foreach (Player player in FindObjectsOfType<Player>())
        {
            GameObject playerPanel = Instantiate(playerPanelPrefab, transform);
            playerPanel.GetComponentInChildren<TextMeshProUGUI>().text = player.PlayerName;
        }
    }
}
