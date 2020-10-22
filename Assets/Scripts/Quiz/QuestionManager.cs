using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class QuestionManager : MonoBehaviour
{
    private static string jsonString = "";
    private static List<Question> MCquestions = new List<Question>();
    private static List<Question> Bquestions = new List<Question>();

    void Start()
    {
        jsonString = "";
        QuantityBoolean = 0;
        QuantityMultiple = 0;
        MCquestions.Clear();
        Bquestions.Clear();
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
    /// <returns>Returns a finish signal whenever the web request has been sent</returns>
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
                        jsonString = jsonData.ToString()
                          .Replace("&#033;", "!")
                          .Replace("&#034;", "''")
                          .Replace("&quot;", "''")
                          .Replace("&#035;", "#")
                          .Replace("&#036;", "'")
                          .Replace("&#037;", "%")
                          .Replace("&#038;", "&")
                          .Replace("&amp;", "&")
                          .Replace("&#039;", "'")
                          .Replace("&#040;", "(")
                          .Replace("&#041;", ")")
                          .Replace("&#047;", "/")
                          .Replace("&#064;", "@")
                          .Replace("&#009;", "")
                          .Replace("&#010;", "")
                          .Replace("&#013;", "")
                          .Replace("&#160;", "")
                          .Replace("&#092;", "'")
                          .Replace("&agrave;", "à")
                          .Replace("&aacute;", "á")
                          .Replace("&apos;", "'")
                          .Replace("&acirc;", "â")
                          .Replace("&atilde;", "ã")
                          .Replace("&Agrave;", "À")
                          .Replace("&Aacute;", "Á")
                          .Replace("&#Acirc;", "Â")
                          .Replace("&Atilde;", "Ã")
                          .Replace("&egrave;", "è")
                          .Replace("&eacute;", "é")
                          .Replace("&ecirc;", "ê")
                          .Replace("&euml;", "ë")
                          .Replace("&igrave;", "ì")
                          .Replace("&iacute;", "í")
                          .Replace("&ograve;", "ò")
                          .Replace("&oacute;", "ó")
                          .Replace("&ugrave;", "ù")
                          .Replace("&uacute;", "ú")
                          .Replace("&Egrave;", "È")
                          .Replace("&Eacute;", "É")
                          .Replace("&Ecirc;", "Ê")
                          .Replace("&Euml;", "Ë")
                          .Replace("&Igrave;", "Ì")
                          .Replace("&Iacute;", "Í")
                          .Replace("&Ograve;", "Ò")
                          .Replace("&Oacute;", "Ó")
                          .Replace("&Ugrave;", "Ù")
                          .Replace("&Uacute;", "Ú")
                          .Replace("&ntilde;", "ñ")
                          .Replace("&Ntilde;", "Ñ")
                          ;
                        ProcessJSON();
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
                QuantityMultiple++;
            }
            else if (type == "boolean")
            {
                string firstIA = D["results"][i]["incorrect_answers"][0];
                Question newQuestion = new Question(category, type, difficulty, question, correctAnswer, firstIA);
                Bquestions.Add(newQuestion);
                QuantityBoolean++;
            }
        }
    }

    /// <summary>
    /// Gets a question from the questions arrays.
    /// </summary>
    /// <param name="ID">Question ID in the array</param>
    /// <param name="multiple">Whether the question is true/false or multiple choice</param>
    /// <returns>Returns the question selected from the array of questions</returns>
    public static Question GetQuestion(int ID, bool multiple)
    {
        Debug.Log("Enter GetQuestion");
        return multiple ? MCquestions[ID] : Bquestions[ID];
    }

    /// <summary>
    /// Used to get the quantity of multiple choice questions in the JSON file.
    /// </summary>
	public static int QuantityMultiple { get; private set; } = 0;

    /// <summary>
    /// Used to get the quantity of boolean questions in the JSON file.
    /// </summary>
	public static int QuantityBoolean { get; private set; } = 0;

}
