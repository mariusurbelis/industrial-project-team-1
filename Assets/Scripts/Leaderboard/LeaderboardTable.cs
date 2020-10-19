using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardElementPrefab = null;
    [SerializeField] private GameObject leaderboardListPanel = null;
    [SerializeField] private Button homeButton = null;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        homeButton.onClick.AddListener(LoadHomeScreen);

        if (PhotonNetwork.IsMasterClient)
        {
            QuizManager.eliminationList.Reverse();
            GetComponent<PhotonView>().RPC("RPC_FillLeaderboard", RpcTarget.AllBuffered, QuizManager.eliminationList.ToArray(), 0);
        }
    }

    /// <summary>
    /// Fills the leaderboard with all of the players names.
    /// </summary>
    [PunRPC]
    public void RPC_FillLeaderboard(string[] eliminationList, int pointlessInt)
    {
        for (int i = 0; i < eliminationList.Length; i++)
        {
            GameObject leaderboardElement = Instantiate(leaderboardElementPrefab, leaderboardListPanel.transform);
            leaderboardElement.gameObject.transform.Find("Nickname").GetComponent<TextMeshProUGUI>().text = eliminationList[i];
            leaderboardElement.gameObject.transform.Find("Position").GetComponent<TextMeshProUGUI>().text = PositionFromIndex(i);
        }
    }

    /// <summary>
    /// Makes an int position into a position string.
    /// </summary>
    /// <param name="index">Index of the position</param>
    /// <returns>The string of the position with a suffix</returns>
    private string PositionFromIndex(int index)
    {
        switch(index)
        {
            case 0:
                return "1st";
            case 1:
                return "2nd";
            case 2:
                return "3rd";
            default:
                return $"{index + 1}th";
        }
    }

    private void LoadHomeScreen()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("ConnectionScene");
    }
}
