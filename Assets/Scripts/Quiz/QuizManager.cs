using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public static int currentCorrectAnswerID = -1;

    private static QuizManager instance;

    [SerializeField] private GameObject[] options;
    private TextMeshProUGUI[] answerOptions;

    private PhotonView photonView;

    private void Awake()
    {
        instance = this;
        photonView = GetComponent<PhotonView>();

        answerOptions = new TextMeshProUGUI[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            answerOptions[i] = options[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    [PunRPC]
    void RPC_LoadQuestion(int ID, int[] order)
    {
        Question question = QuestionManager.GetQuestion(ID);

        string[] answers = { question.correctAnswer, question.incorrectAnswer1, question.incorrectAnswer2, question.incorrectAnswer3 };

        GameObject.Find("Question Text").GetComponent<TextMeshProUGUI>().text = question.question;

        currentCorrectAnswerID = order[0];

        for (int i = 0; i < order.Length; i++)
        {
            answerOptions[order[i]].text = answers[i];
        }
    }

    [PunRPC]
    void RPC_LoadLobby()
    {
        RoomController.LoadSceneByID(1);
    }

    [PunRPC]
    void RPC_SyncTimer(float timer)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            RoundManager.roundTimer = timer;
        }
    }

    public static void SyncTimer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            instance.photonView.RPC("RPC_SyncTimer", RpcTarget.AllBuffered, RoundManager.roundTimer);
        }
    }

    public static void LoadLobby()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            instance.photonView.RPC("RPC_LoadLobby", RpcTarget.AllBuffered);
        }
    }

    public static void LoadNewQuestion()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Randomize the ids array
            int[] ids = RandomizeOrder();
            //Debug.Log($"{ids[0]}, {ids[1]}, {ids[2]}, {ids[3]}");
            int questionID = Random.Range(0, 30);
            instance.photonView.RPC("RPC_LoadQuestion", RpcTarget.AllBuffered, questionID, ids);
            //instance.RPC_LoadQuestion(questionID, ids);
        }
    }

    private static int[] RandomizeOrder()
    {
        int[] order = new int[4];
        List<int> choices = new List<int>() { 0, 1, 2, 3 };
        for (int i = 0; i < 4; i++)
        {
            order[i] = choices[Random.Range(0, choices.Count)];
            choices.Remove(order[i]);
        }
        return order;
    }

}
