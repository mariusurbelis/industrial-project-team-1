using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoundManager : MonoBehaviour
{
    public static float roundTimer = 0;
    private static float roundTime = 10f;

    [SerializeField] private TextMeshProUGUI timerText;

    private bool gameDone = false;

    void Start()
    {
        NextRound();
    }
    /// <summary>
    /// Creates a new round by resetting the timer and loading a new question.
    /// </summary>
    private void NextRound()
    {
        //Debug.Log("Next round starting");
        ResetTimer();
        roundEndInformed = false;
        QuizManager.LoadNewQuestion();
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
            if (!roundEndInformed)
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


        Debug.Log("PM: " + FindObjectsOfType<PlayerMovement>().Length);

        if (FindObjectsOfType<PlayerMovement>().Length == 0 && !gameDone && PhotonNetwork.IsMasterClient)
        {
            QuizManager.LoadLobby();
            Debug.Log("Loading lobby");
            gameDone = true;
        }
    }
    /// <summary>
    /// Starts the first round.
    /// </summary>
    /// <returns>Returns time between new rounds</returns>
    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(0.75f);
        NextRound();
        yield return null;
    }

}
