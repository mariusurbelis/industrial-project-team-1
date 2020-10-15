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
    public GameObject[] Results;
    
    


    // Start is called before the first frame update. This method instantiates the array of nicknames so the table can be set
    void Start()
    {
        numberOfPlayers = 11;
        positionClone = new GameObject[numberOfPlayers+1];
        nicknames = new Text[numberOfPlayers];
        Results = new GameObject[numberOfPlayers];
        //Instantiate array of nicknames
        //nicknames[0] = GameObject.Find("LeaderboardCanvas/First/nickname").GetComponent<Text>();
        //nicknames[1] = GameObject.Find("LeaderboardCanvas/Second/nickname").GetComponent<Text>();
        //nicknames[2] = GameObject.Find("LeaderboardCanvas/Third/nickname").GetComponent<Text>();
        //nicknames[3] = GameObject.Find("LeaderboardCanvas/Fourth/nickname").GetComponent<Text>();
        //nicknames[4] = GameObject.Find("LeaderboardCanvas/Fifth/nickname").GetComponent<Text>();
        //nicknames[5] = GameObject.Find("LeaderboardCanvas/Sixth/nickname").GetComponent<Text>();


        //Store = current.gameObject; // set store equal to parent of box
        //createClonesss();
        //positionClones();
        createClones();
    }

    void positionClones()
    {
        templatePosition.transform.Translate(0, -600, 0);
        if (numberOfPlayers < 10)
        {
            Debug.Log("Players less than 10");
            for (int i = 1; i < numberOfPlayers; i++)
            {
                Debug.Log(i);
                //GameObject.Find("Position").GetComponent<Text>().text = "HELP";//(i).ToString() + "th";
                

                positionClone[i].transform.Translate(512, 0 - (i * 40), 0);
                GameObject.Find("Position").GetComponent<Text>().text = "Score : ";

            }
        }
    }


    void createClonesss()
    {
        for (int i = 1; i < numberOfPlayers; i++)
        {
            positionClone[i] = Instantiate(templatePosition) as GameObject; // clone box
            positionClone[i].transform.SetParent(templatePosition.transform.parent);
            
        }
    }

    void createClones()
    {

        if (numberOfPlayers < 10)
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
        else
        {
            Debug.Log("Players more than 10");
            templatePosition.transform.Translate(-280, 0, 0);
            //templatePosition.transform.localScale += new Vector3(-0.25f, -0.25f, -0.25f);
            for (int i = numberOfPlayers-1 ; i >=0; i--)
            {

                GameObject.Find("Position").GetComponent<Text>().text = (i+1).ToString()+"th";
                

                Debug.Log("DDDD");
                positionClone[i] = Instantiate(templatePosition) as GameObject; // clone box
                positionClone[i].transform.SetParent(templatePosition.transform.parent);
                if (i < 10)
                {
                    Debug.Log(i);
                    Debug.Log(433 - (i * 50));
                    positionClone[i].transform.Translate(512, 383 - (i * 50), 0);
                }
                else
                {
                    Debug.Log(i);
                    Debug.Log(433 - ((i-9) * 50));
                    positionClone[i].transform.Translate(1000,383 - ((i-10) * 50), 0);
                }
                
            }
            positionClone[numberOfPlayers-1].transform.Translate(-280, 0, 0);
        }
        
    }

    //Sets the nickname at the given position
    void setNickname(int position, string name)
    {
        nicknames[position].text = name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
