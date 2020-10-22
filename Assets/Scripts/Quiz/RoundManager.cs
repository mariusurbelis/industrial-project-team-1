using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoundManager : MonoBehaviour
{
    public static float roundTimer = 0;
    public static float roundTime = 10f;

    public static bool gameRunning = true;

    [SerializeField] private TextMeshProUGUI timerText;

    public static bool gameDone = false;

    void Start()
    {
        gameDone = false;
        NextRound();
    }

    /// <summary>
    /// Creates a new round by resetting the timer and loading a new question. Also checks if game is over and calls leaderboard.
    /// </summary>
    private void NextRound()
    {
        Debug.Log("Enter NextRound");
        //Debug.Log("Next round starting");
        ResetTimer();
        roundEndInformed = false;
        QuizManager.LoadNewQuestion();
        StartCoroutine(WaitForPopupEnd());
    }

    private IEnumerator WaitForPopupEnd()
    {
        gameRunning = false;
        yield return new WaitForSeconds(3f);
        gameRunning = true;
        Sound.PlayNewRoundSound(Sound.newRoundSound);
        yield return null;
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
            if (gameRunning)
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
                    Debug.Log("inform");
                    player.RegisterRoundDone();
                }
                roundEndInformed = true;

                QuizManager.SetLives();

                StartCoroutine(StartNewRound());
            }
        }
        //If one player is left in multiplayer
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.CurrentRoom.PlayerCount == QuizManager.eliminationList.ToArray().Length && !gameDone)
        {
            Debug.Log("105");
            // Show that the player won
            FindObjectOfType<UIManager>().ShowWinPopup();
            StartCoroutine(LoadLeaderboard());
            gameDone = true;
        }
        //If player has lost all lives in single players
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && QuizManager.eliminationList.ToArray().Length == 1 && !gameDone)
        {
            Debug.Log("114");
            StartCoroutine(LoadLeaderboard());
            gameDone = true;
        }


    }

    private IEnumerator LoadLeaderboard()
    {
        yield return new WaitForSeconds(4f);
        QuizManager.LoadLeaderboard();
        yield return null;
    }

    /// <summary>
    /// Starts the first round.
    /// </summary>
    /// <returns>Returns time between new rounds</returns>
    private IEnumerator StartNewRound()
    {
        Debug.Log("Enter StartNewRound");
        // FREEZE START
        FindObjectOfType<UIManager>().OpenTrapdoors(QuizManager.currentCorrectAnswerID);
        Debug.Log("Enter 1");
        FindObjectOfType<UIManager>().SetTimerText(0);
        Debug.Log("Enter 2");
        //Problem is with this line
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Enter 3");
        FindObjectOfType<UIManager>().CloseTrapdoors();
        Debug.Log("Enter 4");
        // FREEZE END
        NextRound();
        Debug.Log("Enter 5");
        yield return null;
    }

}
