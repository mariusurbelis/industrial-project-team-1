using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private GameObject[] options;
    private TextMeshProUGUI[] answerOptions;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        
        answerOptions = new TextMeshProUGUI[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            answerOptions[i] = options[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void LoadQuestion()
    {
        Question question = QuestionManager.GetQuestion();
        answerOptions[0].text = question.correctAnswer;
        answerOptions[1].text = question.falseAnswer;
    }

    private void Start()
    {
        //LoadQuestion();

        int[] ids = { 0, 1, 2, 3 };

        photonView.RPC("RPC_LoadQuestion", RpcTarget.AllBuffered, 0, ids);
    }

    [PunRPC]
    void RPC_LoadQuestion(int ID, int[] order)
    {
        Debug.Log("Loaded q");

        Question question = QuestionManager.GetQuestion();

        string[] answers = { question.correctAnswer, question.incorrectAnswer1, question.incorrectAnswer2, question.incorrectAnswer3 };

        GameObject.Find("Question Text").GetComponent<TextMeshProUGUI>().text = question.question;

        for (int i = 0; i < order.Length; i++)
        {
            answerOptions[order[i]].text = answers[i];
        }

    }

}
