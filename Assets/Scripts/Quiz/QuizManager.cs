using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public static int currentCorrectAnswerID = -1;

    private static QuizManager instance;

    [SerializeField] private GameObject[] options = null;
    private TextMeshProUGUI[] answerOptions;
    //list of usernames of dead players in the order they were eliminated
    public static List<string> eliminationList = new List<string>();

    private static UIManager UIManager;
    private PhotonView photonView;

    private static List<int> usedIDs = new List<int>();

    private void Awake()
    {
        instance = this;

        currentCorrectAnswerID = -1;

        UIManager = FindObjectOfType<UIManager>();
        photonView = GetComponent<PhotonView>();

        answerOptions = new TextMeshProUGUI[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            answerOptions[i] = options[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        eliminationList.Clear();
        UIManager.SetInitialTime((int)RoundManager.roundTime);
    }

    /// <summary>
    /// Loads a question for every existing player.
    /// </summary>
    /// <param name="ID">ID used to pick a question from a list of questions</param>
    /// <param name="order">Array used to set the order of questions</param>
    /// <param name="multiple">Based on the bool value, the questions can be either multiple (true) or boolean (false)</param>
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

        currentCorrectAnswerID = order[0];
        UIManager.SetQuestionText(question.question);


        if (Player.Me)
        {
            //Debug.Log($"{Player.Me.Username} cont?: {Player.Me.IsDead}");

            if (!Player.Me.IsDead)
            {
                Debug.Log("Showing the screen");
                UIManager.ShowNextRoundScreen(question.question, 3f);
            }
        }

        string[] finalAnswers = new string[answers.Count];

        for (int i = 0; i < order.Length; i++)
        {
            finalAnswers[order[i]] = answers[i];
            //answerOptions[order[i]].text = answers[i];
        }

        FindObjectOfType<AnswersManager>().SetAnswers(finalAnswers, order);
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
    /// Loads a leaderboard for every existing player.
    /// </summary>
    [PunRPC]
    void RPC_LoadLeaderboard()
    {
        RoomController.LoadSceneByID(3);
    }

    /// <summary>
    /// Synchronises the timer for every existing player.
    /// </summary>
    /// <param name="timer">Time to which the timer variable is set</param>
    [PunRPC]
    void RPC_SyncTimer(float timer)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            RoundManager.roundTimer = timer;
        }
        UIManager.SetTimerText(Mathf.RoundToInt(RoundManager.roundTimer));
    }

    /// <summary>
    /// Syncs everyone's the timer that is running on the host machine.
    /// </summary>
    public static void SyncTimer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            instance.photonView.RPC("RPC_SyncTimer", RpcTarget.AllBuffered, RoundManager.roundTimer);
        }
    }

    /// <summary>
    /// Loads the lobby scene for every player.
    /// </summary>
    public static void LoadLobby()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            instance.photonView.RPC("RPC_LoadLobby", RpcTarget.AllBuffered);
        }
    }

    /// <summary>
    /// Loads the leaderboard scene for every player.
    /// </summary>
    public static void LoadLeaderboard()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            instance.photonView.RPC("RPC_LoadLeaderboard", RpcTarget.AllBuffered);
        }
    }

    /// <summary>
    /// Tells the UI to set the health display to the current player's health.
    /// </summary>
	public static void SetLives()
    {
        UIManager.SetCurrentHearts((Player.Me) ? Player.Me.health : 0);
    }

    /// <summary>
    /// Loads a new random question that has not been shown before.
    /// </summary>
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

            if (instance == null)
            {
                Debug.LogError("No instance!");

                if (instance.photonView == null)
                {
                    Debug.LogError("No PhotonView!");
                }
            }

            instance.photonView.RPC("RPC_LoadQuestion", RpcTarget.AllBuffered, questionID, ids, multiple);
            //instance.RPC_LoadQuestion(questionID, ids);
        }
    }

    /// <summary>
    /// Randomizes the order of the IDs in an array to make the answer order random.
    /// </summary>
    /// <param name="multiple">If it is multiple choice or true/false question</param>
    /// <returns></returns>
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
    public static void LoadHomeScreen()
    {
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        //if (PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
        //if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
        SceneManager.LoadScene("ConnectionScene");
    }
    public static void LoadLobbyScreen()
    {
        if (PhotonNetwork.InRoom) { SceneManager.LoadScene("LobbyScene"); }
    }

}
