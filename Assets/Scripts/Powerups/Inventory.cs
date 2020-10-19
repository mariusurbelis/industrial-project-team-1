using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private TextMeshProUGUI powerupText;

    
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
            Player.Me.UsePowerup();
        }
    }

   
}
