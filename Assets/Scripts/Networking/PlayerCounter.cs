using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float timer = 0;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (PhotonNetwork.InRoom && TimerDone())
        {
            //text.text = $"Players: {PhotonNetwork.CurrentRoom.PlayerCount} ({PhotonNetwork.CurrentRoom.Name})";
        }
    }

    /// <summary>
    /// Advances the timer and returns true every half a second if called every frame.
    /// </summary>
    /// <returns>True if the timer is done</returns>
    private bool TimerDone()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            timer = 0;
            return true;
        }

        return false;
    }

}
