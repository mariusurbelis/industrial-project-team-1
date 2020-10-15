using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button joinButton = null;
    [SerializeField] private Button hostButton = null;
    [SerializeField] private Button settingsButton = null;
    [SerializeField] private Button backButton = null;
    [SerializeField] private Button instructionsButton = null;
    [SerializeField] private Button closeInstructionsButton = null;
    [SerializeField] private GameObject navigationPanel = null;
    [SerializeField] private GameObject userPanel = null;
    [SerializeField] private GameObject hostPanel = null;
    [SerializeField] private GameObject instructionsPopUP = null;
    [SerializeField] private GameObject gameName = null;
    [SerializeField] private GameObject lobbyInfo = null;

    private void Start()
    {
        joinButton.onClick.AddListener(() => { ShowScreen(userPanel); });
        hostButton.onClick.AddListener(() => { ShowHostScreen(); });
        backButton.onClick.AddListener(() => { ShowScreen(navigationPanel); });
        instructionsButton.onClick.AddListener(() => { ShowInstructions(); });
        closeInstructionsButton.onClick.AddListener(() => { ShowScreen(navigationPanel); });
    }

    private void ShowHostScreen()
    {
        ShowScreen(userPanel);
        hostPanel.SetActive(true);
    }

    private void ShowScreen(GameObject activeObject)
    {
        HideUI();
        activeObject.SetActive(true);
    }

    private void HideUI()
    {
        navigationPanel.SetActive(false);
        userPanel.SetActive(false);
        hostPanel.SetActive(false);
        instructionsPopUP.SetActive(false);
   
    }

    private void ShowInstructions()
    {
        HideUI();
        instructionsPopUP.SetActive(true);
      
    }
}

