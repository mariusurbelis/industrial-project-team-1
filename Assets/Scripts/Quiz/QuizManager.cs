using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;
using Random = UnityEngine.Random;

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

    int[] ids = { 0, 1, 2, 3 };

    private void Start()
    {
        photonView.RPC("RPC_LoadQuestion", RpcTarget.AllBuffered, 0, ids);
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            photonView.RPC("RPC_LoadQuestion", RpcTarget.AllBuffered, Random.Range(0, 25), ids);
        }
    }

    [PunRPC]
    void RPC_LoadQuestion(int ID, int[] order)
    {
        Question question = QuestionManager.GetQuestion(ID);

        string[] answers = { question.correctAnswer, question.incorrectAnswer1, question.incorrectAnswer2, question.incorrectAnswer3 };

        GameObject.Find("Question Text").GetComponent<TextMeshProUGUI>().text = question.question;

        for (int i = 0; i < order.Length; i++)
        {
            answerOptions[order[i]].text = answers[i];
        }

    }

}
