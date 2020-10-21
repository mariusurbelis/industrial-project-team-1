using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{


    private TextMeshProUGUI powerupText;
    public static Image icon;
    public static Button removeButton;

    /// <summary>
    /// Add powerup into the players powerup inventory and enables it to be used
    /// </summary>
    public void AddPowerup(Sprite newPowerup)
    {
        if (Player.Me.powerup.ToString()!="None")
        {

            Debug.Log("Sprite passed:" + newPowerup);

            icon.sprite = newPowerup;
            Debug.Log("Sprite obtained:" + icon.sprite);
            icon.enabled = true;
            removeButton.interactable = true;
        }
    }


    /// <summary>
    /// Drop powerup from players inventory and disable it to be used
    /// </summary>
    public void DropPowerupIcon()
    {
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        Player.Me.DropPowerup();
    }

    void Start()
    {
        powerupText = GameObject.Find("PowerupText").GetComponent<TextMeshProUGUI>();
        icon = GameObject.Find("PowerUpIcon").GetComponent<Image>();
        removeButton = GameObject.Find("DropPowerupBtn").GetComponent<Button>();
    }

    void Update()
    {
        if (Player.Me)
        {
            powerupText.text = "Player has: " + Player.Me.powerup.ToString();
          
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PowerUpClicked();
        }
    }

    public void PowerUpClicked()
    {
        Player.Me.UsePowerup();
        DropPowerupIcon();

    }

   

    

}





