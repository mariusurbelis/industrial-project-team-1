using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public UIManager UIManager;
    private float timePassed = 30;
    private int timer = 30;

    // Update is called once per frame
    void Update()
    {
        timePassed -= Time.deltaTime;

        timer = (int)Mathf.Ceil(timePassed);

        UIManager.SetTimerText(timer);

        if(timer <= 25)
        {
            UIManager.SetCurrentHearts(2);
            UIManager.SetQuestionText("test1");
        }
        if (timer <= 20)
        {
            UIManager.SetCurrentHearts(1);
            UIManager.SetQuestionText("test2");
        }
        if (timer <= 15)
        {
            UIManager.SetCurrentHearts(0);
            UIManager.SetQuestionText("test3");
        }
    }
}
