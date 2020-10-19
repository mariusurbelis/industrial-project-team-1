using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static State state = State.None;
    public enum State { None, Host, Join };

    [SerializeField] private Button joinButton = null;
    [SerializeField] private Button hostButton = null;
    [SerializeField] private Button backButton = null;
    [SerializeField] private Button instructionsButton = null;
    [SerializeField] private Button closeInstructionsButton = null;
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private GameObject navigationPanel = null;
    [SerializeField] private GameObject joinHostPanel = null;
    [SerializeField] private GameObject hostPanel = null;
    [SerializeField] private GameObject roomNameInput = null;
    [SerializeField] private GameObject instructionsPopUP = null;
    //[SerializeField] private GameObject gameName = null;
    //[SerializeField] private GameObject lobbyInfo = null;

    private void Start()
    {
        joinButton.onClick.AddListener(() =>
        {
            ShowScreen(joinHostPanel); state = State.Join; roomNameInput.SetActive(true);
            startGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "Join";
        });

        hostButton.onClick.AddListener(() =>
        {
            ShowHostScreen(); state = State.Host;
            startGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "Host";
        });

        backButton.onClick.AddListener(() => { ShowScreen(navigationPanel); state = State.None; });
        instructionsButton.onClick.AddListener(() => { ShowInstructions(); });
        closeInstructionsButton.onClick.AddListener(() => { ShowScreen(navigationPanel); });
    }

    private void ShowHostScreen()
    {
        ShowScreen(joinHostPanel);
        hostPanel.SetActive(true);
        roomNameInput.SetActive(false);
    }

    private void ShowScreen(GameObject activeObject)
    {
        HideUI();
        activeObject.SetActive(true);
    }

    private void HideUI()
    {
        navigationPanel.SetActive(false);
        joinHostPanel.SetActive(false);
        hostPanel.SetActive(false);
        instructionsPopUP.SetActive(false);
    }

    private void ShowInstructions()
    {
        HideUI();
        instructionsPopUP.SetActive(true);
    }
}

