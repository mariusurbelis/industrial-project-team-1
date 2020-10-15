using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
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

    /**
   * Method to hide display panels based.
   */
    private void ShowScreen(GameObject activeObject)
    {
        HideUI();
        activeObject.SetActive(true);
    }


    /**
     * Method to hide no relevant UI panels 
     */
    private void HideUI()
    {
        navigationPanel.SetActive(false);
        userPanel.SetActive(false);
        hostPanel.SetActive(false);
        instructionsPopUP.SetActive(false);
   
    }

    /**
     * Method to display the instructions panel while hiding the other non relevant panels
     */
    private void ShowInstructions()
    {
        HideUI();
        instructionsPopUP.SetActive(true);
      
    }
}

