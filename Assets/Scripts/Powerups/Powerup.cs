﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType { None, Bomb, Half, Fan, Hint, Eyes, LightsOut, Star, Wildcard };

    [SerializeField] private PowerupType myType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().powerup != PowerupType.None)
            {
                collision.gameObject.GetComponent<Player>().PickUpPowerup(myType);
                Destroy(gameObject);
            }
        }
    }
}
