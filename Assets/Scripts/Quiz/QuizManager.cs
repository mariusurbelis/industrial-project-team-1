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

    [SerializeField] private GameObject[] options = null;
    private TextMeshProUGUI[] answerOptions;

    private UIManager uiManager;
    private PhotonView photonView;

	private static List<int> usedIDs = new List<int>();

    private void Awake()
    {
        instance = this;

        uiManager = FindObjectOfType<UIManager>();
        photonView = GetComponent<PhotonView>();

        answerOptions = new TextMeshProUGUI[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            answerOptions[i] = options[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    /// <summary>
    /// Loads a question for every existing player.
    /// </summary>
    /// <param name="ID">ID used to pick a question from a list of questions.</param>
    /// <param name="order">Array used to set the order of questions.</param>
    /// <param name="multiple">Based on the bool value, the questions can be either multiple (true) or boolean (false).</param>
    [PunRPC]
    void RPC_LoadQuestion(int ID, int[] order, bool multiple)
    {

		Question question = QuestionManager.GetQuestion(ID, multiple);

		List<string> answers = new List<string>();

		if (multiple)
		{
			answers.Add(question.correctAnswer);
			answers.Add(question.incorrectAnswer1);
			answers.Add(question.incorrectAnswer2);
			answers.Add(question.incorrectAnswer3);
		}
		else
		{
			answers.Add(question.correctAnswer);
			answers.Add(question.falseAnswer);
		}
		answers.ToArray();
        uiManager.SetQuestionText(question.question);
        currentCorrectAnswerID = order[0];
		//--------------------------------------------------------------- we send answers array to whatever function displays the options
		for (int i = 0; i < order.Length; i++)
        {
            answerOptions[order[i]].text = answers[i];
        }
    }
    /// <summary>
    /// Loads a lobby for every existing player.
    /// </summary>
    [PunRPC]
    void RPC_LoadLobby()
    {
        RoomController.LoadSceneByID(1);
    }
    /// <summary>
    /// Synchronises the timer for every existing player.
    /// </summary>
    /// <param name="timer"></param>
    [PunRPC]
    void RPC_SyncTimer(float timer)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            RoundManager.roundTimer = timer;
        }
        uiManager.SetTimerText(Mathf.RoundToInt(RoundManager.roundTimer));
    }
    /// <summary>
    /// 
    /// </summary>
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

	public static void SetLives()
    {
        instance.uiManager.SetCurrentHearts((Player.Me) ? Player.Me.health : 0);
    }

    public static void LoadNewQuestion()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Randomize the ids array
			//Debug.Log($"{ids[0]}, {ids[1]}, {ids[2]}, {ids[3]}");

			int questionID;
			bool multiple = Mathf.RoundToInt(Random.value) == 0;
            int[] ids = RandomizeOrder(multiple);

			if (multiple)
			{
				questionID = Random.Range(0, QuestionManager.QuantityMultiple);
				while (usedIDs.Contains(questionID))
				{
					questionID = Random.Range(0, QuestionManager.QuantityMultiple);
				}
				usedIDs.Add(questionID);
			}
			else
			{
				questionID = Random.Range(0, QuestionManager.QuantityBoolean);
				while (usedIDs.Contains(questionID))
				{
					questionID = Random.Range(0, QuestionManager.QuantityBoolean);
				}
				usedIDs.Add(questionID);
			}

            instance.photonView.RPC("RPC_LoadQuestion", RpcTarget.AllBuffered, questionID, ids, multiple);
            //instance.RPC_LoadQuestion(questionID, ids);
        }
    }


    private static int[] RandomizeOrder(bool multiple)
    {
		if (multiple)
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
		else
		{
			int[] order = new int[2];
			List<int> choices = new List<int>() { 0, 1 };
			for (int i = 0; i < 2; i++)
			{
				order[i] = choices[Random.Range(0, choices.Count)];
				choices.Remove(order[i]);
			}
			return order;
		}
    }

}
