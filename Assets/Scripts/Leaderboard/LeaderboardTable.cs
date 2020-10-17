using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    public GameObject templatePosition;
    GameObject[] positionClone;
    public Text[] nicknames;
    List<GameObject> clonesArray = new List<GameObject>();



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        int numberOfPlayers = QuizManager.eliminationList.Count;
        positionClone = new GameObject[numberOfPlayers+1];
        nicknames = new Text[numberOfPlayers+1];
        
        createClones(numberOfPlayers);
        fillLeaderboard(numberOfPlayers);

    }

    /// <summary>
    /// Fills the leaderboard with all of the players names
    /// </summary>
    /// <param name="numberOfPlayers">number of players in the room</param>
    void fillLeaderboard(int numberOfPlayers)
    {
        int counter = 0;
        for (int i = numberOfPlayers - 1; i >= 0; i--)
        {
            setNickname(counter, QuizManager.eliminationList[i]);
            counter++;
        }
    }

    /// <summary>
    /// This function instantiates the clones and formats the positions depending on the amount of players
    /// </summary>
    /// <param name="numberOfPlayers">number of players in the room</param>
    void createClones(int numberOfPlayers)
    {
        if (numberOfPlayers <= 10) //if true, keep the positions in one colomn
        {
            for (int i = numberOfPlayers-1; i >=0; i--)
            {
                setPosition(i, GameObject.Find("Position").GetComponent<Text>());
                positionClone[i] = Instantiate(templatePosition) as GameObject; // clone position
                positionClone[i].transform.SetParent(templatePosition.transform.parent); //change dimensions of clones to match the template

                positionClone[i].GetComponent<RectTransform>().localScale = templatePosition.GetComponent<RectTransform>().localScale;
                positionClone[i].transform.position = templatePosition.transform.position;

                positionClone[i].transform.Translate(0,0-(i * 15), 0);
            }
        }
        else //if true, then format the positions so that it is in two columns instead of one
        {
            for (int i = numberOfPlayers-1 ; i >=0; i--)
            {
                setPosition(i, GameObject.Find("Position").GetComponent<Text>());

                positionClone[i] = Instantiate(templatePosition) as GameObject; //clone position
                positionClone[i].transform.SetParent(templatePosition.transform.parent);
                positionClone[i].GetComponent<RectTransform>().localScale = templatePosition.GetComponent<RectTransform>().localScale;
                positionClone[i].transform.position = templatePosition.transform.position;
                if (i < 10) //column one
                {
                    positionClone[i].transform.Translate(-90, 0 - (i * 15), 0); //move position to the correct row
                }
                else //column two
                {

                    positionClone[i].transform.Translate(90, 0 - ((i-10) * 15), 0); //move possition to the porrect row
                }
            }
        }
        templatePosition.transform.Translate(0, 1000, 0);
        addClonesToArray(); 
    }

    /// <summary>
    /// Sets the nickname at the given position
    /// </summary>
    /// <param name="position">The position on the leaderboard thats being edited</param>
    /// <param name="name">The nickname of the player</param>
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
        for (int j = this.transform.childCount - 1; j >= 0; j--)
        {
            clonesArray.Add(this.transform.GetChild(j).gameObject);
        }
    }

    /// <summary>
    /// Changes the text for each position to show the correct placings
    /// </summary>
    /// <param name="pos">the position to be added to the leaderboard</param>
    /// <param name="textObject">the text object that is being edited</param>
    void setPosition(int pos, Text textObject)
    {
        textObject.text = (pos + 1).ToString();
        if (pos == 0)
            textObject.text += "st";
        else if (pos == 1)
            textObject.text += "nd";
        else if (pos == 2)
            textObject.text += "rd";
        else
            textObject.text += "th";

    }


}
