﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoundManager : MonoBehaviour
{
    public static float roundTimer = 0;
    public static float roundTime = 10f;

    [SerializeField] private TextMeshProUGUI timerText;

    public static bool gameDone = false;
    private int players = 0;

    void Start()
    {
        Debug.Log("NUMBER OF PLAYERS: " + players);
        NextRound();
    }

    /// <summary>
    /// Creates a new round by resetting the timer and loading a new question. Also checks if game is over and calls leaderboard
    /// </summary>
    private void NextRound()
    {
        //Debug.Log("Next round starting");
        ResetTimer();
        roundEndInformed = false;
        QuizManager.LoadNewQuestion();
        Sound.PlayNewRoundSound(Sound.newRoundSound);
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    private void ResetTimer()
    {
        roundTimer = roundTime;
    }

    private float syncTimer = 0;
    private bool roundEndInformed = false;

    void Update()
    {
        //timerText.text = "" + Mathf.Round(roundTimer * 10) / 10;

        if (roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;

            if (PhotonNetwork.IsMasterClient)
            {
                if (syncTimer > 0)
                {
                    syncTimer -= Time.deltaTime;
                }
                else
                {
                    syncTimer = 1f;
                    QuizManager.SyncTimer();
                }
            }

        }
        else
        {
            if (!roundEndInformed && !gameDone)
            {
                // DONE
                roundTimer = 0;

                // Inform people
                foreach (Player player in FindObjectsOfType<Player>())
                {
                    player.RegisterRoundDone();
                }
                roundEndInformed = true;

                QuizManager.SetLives();

                StartCoroutine(StartNewRound());
            }
        }
        //If one player is left in multiplayer
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.CurrentRoom.PlayerCount == QuizManager.eliminationList.ToArray().Length&&!gameDone)
        {
            StartCoroutine(LoadLeaderboard());
            gameDone = true;
        }
        //If player has lost all lives in single players
        else if(PhotonNetwork.CurrentRoom.PlayerCount==1 && QuizManager.eliminationList.ToArray().Length==1 && !gameDone)
        {
            StartCoroutine(LoadLeaderboard());
            gameDone = true;
        }
        

    }

    private IEnumerator LoadLeaderboard()
    {
        yield return new WaitForSeconds(1.5f);
        QuizManager.LoadLeaderboard();
        yield return null;
    }

    /// <summary>
    /// Starts the first round.
    /// </summary>
    /// <returns>Returns time between new rounds</returns>
    private IEnumerator StartNewRound()
    {
        FindObjectOfType<UIManager>().OpenTrapdoors(QuizManager.currentCorrectAnswerID);
        FindObjectOfType<UIManager>().SetTimerText(0);
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<UIManager>().CloseTrapdoors();
        NextRound();
        yield return null;
    }


    private void addToPlayerCounter()
    {
        players++;
    }
}
