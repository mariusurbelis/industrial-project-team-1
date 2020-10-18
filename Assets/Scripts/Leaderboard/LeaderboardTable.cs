using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    public GameObject[] positions;



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        int numberOfPlayers = QuizManager.eliminationList.Count;
        
        fillLeaderboard();

    }

    /// <summary>
    /// Fills the leaderboard with all of the players names
    /// </summary>
    void fillLeaderboard()
    {
        int counter = 0;
        for (int i = QuizManager.eliminationList.Count - 1; i >= 0; i--)
        {
            setNickname(counter, QuizManager.eliminationList[counter]);
            counter++;
        }
        for (int j = counter; j<4; j++)
        {
            positions[j].SetActive(false);
        }
    }
    
    /// <summary>
    /// Sets the nickname at the given position
    /// </summary>
    /// <param name="position">The position on the leaderboard thats being edited</param>
    /// <param name="name">The nickname of the player</param>
    void setNickname(int position, string name)
    {
        positions[position].transform.Find("nickname").GetComponent<Text>().text = name;
    }
    


}
