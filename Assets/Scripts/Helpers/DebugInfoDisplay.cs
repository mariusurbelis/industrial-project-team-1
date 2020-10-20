using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInfoDisplay : MonoBehaviour
{
    private int yAxisOffset = 10;

    void OnGUI()
    {
        yAxisOffset = 400;


        foreach (string s in QuizManager.eliminationList)
        {
            GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), s);
        }

        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Players: {PhotonNetwork.CurrentRoom.PlayerCount}");
        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Health: {Player.Me?.health}");
        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Selection: {Player.Me?.selectedOption}");
        //GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Correct: {QuizManager.currentCorrectAnswerID}");
    }
}
