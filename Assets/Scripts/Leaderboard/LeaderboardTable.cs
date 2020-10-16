using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    public int numberOfPlayers;
    public GameObject templatePosition;
    GameObject[] positionClone;
    public Text[] nicknames;
    public GameObject canvas;
    List<GameObject> clonesArray = new List<GameObject>();



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        numberOfPlayers = QuizManager.eliminationList.Count;
        positionClone = new GameObject[numberOfPlayers+1];
        nicknames = new Text[numberOfPlayers];
        
        createClones();
        fillLeaderboard();

    }

    void fillLeaderboard()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            setNickname(i, QuizManager.eliminationList[i]);
        }
    }

    /// <summary>
    ///This function instantiates the clones and formats them depending on the amount of players
    ///</summary>
    void createClones()
    {
        if (numberOfPlayers < 10) //if true, keep the positions in one colomn
        {
            Debug.Log("Players less than 10");
            for (int i = numberOfPlayers-1; i >=0; i--)
            {
                GameObject.Find("Position").GetComponent<Text>().text = (i+1).ToString() + "th";
                positionClone[i] = Instantiate(templatePosition) as GameObject; // clone position
                positionClone[i].transform.SetParent(templatePosition.transform.parent);
                positionClone[i].transform.Translate(512, 383 - (i * 40), 0);
            }
        }
        else //if true, then format the positions so that it is in two columns instead of one
        {
            templatePosition.transform.Translate(-250, 0, 0);
            for (int i = numberOfPlayers-1 ; i >=0; i--)
            {

                GameObject.Find("Position").GetComponent<Text>().text = (i+1).ToString()+"th";
                
                positionClone[i] = Instantiate(templatePosition) as GameObject; //clone position
                positionClone[i].transform.SetParent(templatePosition.transform.parent);
                if (i < 10) //column one
                {
                    positionClone[i].transform.Translate(512, 383 - (i * 50), 0);
                }
                else //column two
                {
                    positionClone[i].transform.Translate(1020,383 - ((i-10) * 50), 0);
                }
                
            }
            positionClone[numberOfPlayers - 1].transform.Translate(-250, 0, 0);
        }
        addClonesToArray(); 
    }

    /// <summary>
    ///Sets the nickname at the given position
    ///</summary>
    void setNickname(int position, string name)
    {
        clonesArray[position].transform.Find("nickname").GetComponent<Text>().text = name;
        if (position == 0) //Also change the nick if the position is 0, as that is the template
        {
            templatePosition.transform.Find("nickname").GetComponent<Text>().text = name;
        }
    }

    /// <summary>
    ///This function adds all the created clones to an array, it is called in the createClones function
    ///</summary>
    void addClonesToArray()
    {
        for (int j = canvas.transform.childCount - 1; j >= 0; j--)
        {
            clonesArray.Add(canvas.transform.GetChild(j).gameObject);
        }
    }

    

    
}
