using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public UIManager UIManager;
    private float timePassed = 30;
    private int timer = 5;

    private void Start()
    {
        UIManager.OpenTrapdoors(1);
        timePassed = 25;
        timer = 10;
        UIManager.ShowDeathPopup();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed -= Time.deltaTime;

        timer = (int)Mathf.Ceil(timePassed);

        UIManager.SetTimerText(timer);

        // Test setting different questions/ answers
        if (timer <= 15)
        {
            UIManager.SetCurrentHearts(0);
            UIManager.SetQuestionText("test3");

            string[] answers = { "a1", "a2", "a3", "a4" };
            int[] order = { 0, 3, 1, 2 };
            UIManager.SetAnswers(answers, order);

            
        }
        else if (timer <= 20)
        {
            UIManager.SetCurrentHearts(1);
            UIManager.SetQuestionText("test2");

            string[] answers = { "b1", "b2", "b3", "b4" };
            int[] order = { 0, 3, 1, 2 };
            UIManager.SetAnswers(answers, order);

            UIManager.HideNextRoundScreen();
            UIManager.HideDeathPopup();



        }
        else if (timer <= 25)
        {
            UIManager.SetCurrentHearts(2);
            UIManager.SetQuestionText("test1");

            string[] answers = { "a1", "a2"};
            int[] order = { 0, 1};
            UIManager.SetAnswers(answers, order);

            UIManager.CloseTrapdoors();
            UIManager.ShowNextRoundScreen("test1");
        }
        
        
    }
}
