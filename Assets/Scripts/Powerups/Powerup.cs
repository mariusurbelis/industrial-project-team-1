using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Powerup : MonoBehaviour
{
    public enum PowerupType { None, Bomb, Hint, Fan, Half, Ice, LightsOut, Star, WildCard };

    [SerializeField] private PowerupType myType = PowerupType.None;

    public Sprite powerUpIcon = null; //variable to store powerup Icons of the prefab
    private Inventory inventory;


    private void Start()
    {
        inventory = GameObject.Find("Logic").GetComponent<Inventory>();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().powerup != PowerupType.None)
            {
                Destroy(gameObject);
                return;
            }  
            if (collision.gameObject.GetComponent<Player>().IsMe)
            {

                collision.gameObject.GetComponent<Player>().PickUpPowerup(myType);
                //Debug.Log("Collison with:" + myType);
                //initialising gameobject prefab with the powerup picked up in game with the relevant powerup in the powerups folder prefab
                GameObject powerIconPrefab = (GameObject)Resources.Load(Path.Combine("Powerups", myType.ToString()), typeof(GameObject));

                //Debug.Log("PowerupPrefab:" + powerIconPrefab);
                powerUpIcon = powerIconPrefab.GetComponent<SpriteRenderer>().sprite;

                Debug.Log("PowerupIcon:" + powerUpIcon);

                inventory.AddPowerup(powerUpIcon);

                Destroy(gameObject);
            }
        }
    }
}
