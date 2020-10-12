using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string username = "";
    public Color playerColor;

    public PhotonView photonView;
    public Rigidbody2D myBody;

    public int health = 3;

    public int selectedOption = -1;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        myBody = GetComponent<Rigidbody2D>();

        if (PlayerDataManager.ValueExists(PlayerDataManager.PlayerColor))
        {
            if (ColorUtility.TryParseHtmlString(PlayerDataManager.LoadData(PlayerDataManager.PlayerColor), out playerColor))
            {
                //Debug.Log("Player color successfully set");
            }
        }

        if (PlayerDataManager.ValueExists(PlayerDataManager.PlayerUsername))
        {
            username = PlayerDataManager.LoadData(PlayerDataManager.PlayerUsername);
        }

        gameObject.AddComponent<PlayerAvatar>();
        gameObject.AddComponent<PlayerID>();
    }

    public void RegisterRoundDone()
    {
        //if(!IsMe) return;

        //Debug.Log($"Player {username} selected {selectedOption} option");

        // Selected option to check against the correct answer
        if (selectedOption != QuizManager.currentCorrectAnswerID)
        {
            health--;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // Temporary
        Destroy(gameObject.GetComponent<PlayerMovement>());
        transform.position = new Vector2(0, -1.55f);
    }

    public bool IsMe => photonView.IsMine;

    public string PlayerID => photonView.ViewID.ToString();

    public string PlayerName => ((username.Length < 2) ? PlayerID : username);

    public static Player Me
    {
        get
        {
            foreach (Player player in FindObjectsOfType<Player>())
            {
                if (player.IsMe)
                {
                    return player;
                }
            }

            return null;
        }
    }
}
