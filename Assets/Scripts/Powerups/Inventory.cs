using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
  
    public static Inventory instance;

  
    private TextMeshProUGUI powerupText;
    public Image icon;
    public Button removeButton;
    

    public void AddPowerup(Sprite newPowerup)
    {
        Debug.Log("Sprite passed:" + newPowerup);
          
        icon.sprite = newPowerup;
        Debug.Log("Sprite obtained:" + icon.sprite);
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void DropPowerup()
    {
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    void Start()
    {
        powerupText = GameObject.Find("PowerupText").GetComponent<TextMeshProUGUI>();
       
       
    }

    void Update()
    {
        if (Player.Me)
        {
            powerupText.text = "Player has: " + Player.Me.powerup.ToString();
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            UsePowerup();
        }
    }

    public void UsePowerup()
    {
        Player.Me.UsePowerup();
        DropPowerup();
    }
   
}




    
