using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class QuestionManager : MonoBehaviour
{
    private static string jsonString;
    private static List<Question> MCquestions;
    private static List<Question> Bquestions;
    private static int MCamount = 0;
    private static int Bamount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetJsonData();
    }
    /// <summary>
    /// Starts a Coroutine to get JSON data from API
    /// </summary>
    public void GetJsonData()
    {
        StartCoroutine(RequestWebService());
    }
    /// <summary>
    /// Requests a web request to the API and parses the data from the API to a JSON file.
    /// </summary>
    /// <returns>Returns a finish signal whenever the web request has been sent.</returns>
    IEnumerator RequestWebService()
    {
        string getDataUrl = "https://api.urbelis.dev/project?key=questions";

        using (UnityWebRequest webData = UnityWebRequest.Get(getDataUrl))
        {

            yield return webData.SendWebRequest();
            if (webData.isNetworkError || webData.isHttpError)
            {
                print(webData.error);
            }
            else
            {
                if (webData.isDone)
                {
                    //JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));
                    JSONNode jsonData = JSON.Parse(webData.downloadHandler.text);

                    if (jsonData == null)
                    {
                        print("---------------- NO DATA ----------------");
                    }
                    else
                    {
                        Debug.Log(jsonData.ToString());
                        jsonString = jsonData.ToString().Replace("&#039;", "'").Replace("&quot;", "\"");
                        ProcessJSON();
                        Debug.Log(jsonString);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Processes the data from the JSON file retrieved from the API.
    /// Stores each question in the JSON file, into a question object.
    /// Depending on the type of question (multiple choice/boolean) it will call different constructors for the questions.
    /// </summary>
    private void ProcessJSON()
    {
        var D = JSON.Parse(jsonString);
        int length = D["results"].Count;
        // List of multiple choice questions
        MCquestions = new List<Question>();
        // List of boolean true/false questions
        Bquestions = new List<Question>();

        for (int i = 0; i < length; i++)
        {
            string category = D["results"][i]["category"];
            string type = D["results"][i]["type"];
            string difficulty = D["results"][i]["difficulty"];
            string question = D["results"][i]["question"];
            string correctAnswer = D["results"][i]["correct_answer"];
            if (type == "multiple")
            {
                string firstIA = D["results"][i]["incorrect_answers"][0];
                string secondIA = D["results"][i]["incorrect_answers"][1];
                string thirdIA = D["results"][i]["incorrect_answers"][2];
                Question newQuestion = new Question(category, type, difficulty, question, correctAnswer, firstIA, secondIA, thirdIA);
                MCquestions.Add(newQuestion);
                MCamount++;
            }
            else if (type == "boolean")
            {
                string firstIA = D["results"][i]["incorrect_answers"][0];
                Question newQuestion = new Question(category, type, difficulty, question, correctAnswer, firstIA);
                Bquestions.Add(newQuestion);
                Bamount++;
            }
        }
    }

    /// <summary>
    /// Gets a question from the questions arrays.
    /// </summary>
    /// <param name="ID">Question ID in the array</param>
    /// <param name="multiple">Whether the question is true/false or multiple choice</param>
    /// <returns></returns>
    public static Question GetQuestion(int ID, bool multiple)
    {
        return multiple ? MCquestions[ID] : Bquestions[ID];
    }

    /// <summary>
    /// Used to get the quantity of multiple choice questions in the JSON file.
    /// </summary>
	public static int QuantityMultiple
	{
		get
		{
			return MCamount;
		}
	}
    /// <summary>
    /// Used to get the quantity of boolean questions in the JSON file.
    /// </summary>
	public static int QuantityBoolean
	{
		get
		{
			return Bamount;
		}
	}

}
