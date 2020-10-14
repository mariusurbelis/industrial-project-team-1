using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswersManager : MonoBehaviour
{
    public GameObject[] answerArray;
    public Sprite m_closedTrapdoorSprite;
    private int[] answerOrder = new int[4];
    private Animator anim;

    /// <summary>
    /// Opens a trapdoor
    /// </summary>
    /// <param name="answer">The answer to open</param>
    public void OpenTrapdoor(GameObject answer)
    {
        if (answer.activeSelf)
        {
            anim = answer.GetComponentInChildren<Animator>();
            anim.Play("trapdoor_open");
        }
    }

    /// <summary>
    /// Closes a trapdoor
    /// </summary>
    /// <param name="answer">The answer to close</param>
    public void CloseTrapdoor(GameObject answer)
    {
        if (answer.activeSelf)
        {
            anim = answer.GetComponentInChildren<Animator>();
            anim.Play("trapdoor_close");
        }
    }

    /// <summary>
    /// Sets the answer text
    /// </summary>
    /// <param name="answerOptions">Array of answers to display</param>
    /// <param name="order">Order the answers should be displayed in</param>
    public void SetAnswers(string[] answerOptions, int[] order)
    {
        // If there are only two answers, question is true false, so take two options away
        if (answerOptions.Length == 4)
        {
            answerArray[2].SetActive(true);
            answerArray[3].SetActive(true);
            CloseTrapdoor(answerArray[2]);
            CloseTrapdoor(answerArray[3]);
        }
        else
        {
            answerArray[2].SetActive(false);
            answerArray[3].SetActive(false);
        }


        for (int i = 0; i < answerOptions.Length; i++)
        {
            // answerArray[i].GetComponentInChildren<Image>().sprite = m_closedTrapdoorSprite;
            answerArray[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i];
            answerOrder[i] = order[i];
        }
    }

}
