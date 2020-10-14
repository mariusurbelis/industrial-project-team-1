using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    
    public GameObject current;
    public Text[] nicknames = new Text[6];
    public GameObject[] Results = new GameObject[6];
    
    


    // Start is called before the first frame update. This method instantiates the array of nicknames so the table can be set
    void Start()
    {
        //Instantiate array of nicknames
        nicknames[0] = GameObject.Find("LeaderboardCanvas/First/nickname").GetComponent<Text>();
        nicknames[1] = GameObject.Find("LeaderboardCanvas/Second/nickname").GetComponent<Text>();
        nicknames[2] = GameObject.Find("LeaderboardCanvas/Third/nickname").GetComponent<Text>();
        nicknames[3] = GameObject.Find("LeaderboardCanvas/Fourth/nickname").GetComponent<Text>();
        nicknames[4] = GameObject.Find("LeaderboardCanvas/Fifth/nickname").GetComponent<Text>();
        nicknames[5] = GameObject.Find("LeaderboardCanvas/Sixth/nickname").GetComponent<Text>();

        

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
