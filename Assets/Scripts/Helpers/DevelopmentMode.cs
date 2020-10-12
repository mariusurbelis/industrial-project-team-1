using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentMode : MonoBehaviour
{
    private int yAxisOffset = 10;

    void OnGUI()
    {
        yAxisOffset = 100;

        GUI.Label(new Rect(new Vector2(10, yAxisOffset += 15), new Vector2(200, 20)), $"Health: {Player.Me.health}");
    }
}
