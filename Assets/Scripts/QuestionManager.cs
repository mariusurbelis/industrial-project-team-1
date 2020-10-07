using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

public class QuestionManager : MonoBehaviour
{
	private string jsonString;
	public List<Question> MCquestions;
	public List<Question> Bquestions;
	public int MCamount = 0;
	public int Bamount = 0;

	// Start is called before the first frame update
	void Start()
	{
		jsonString = File.ReadAllText(Application.dataPath + "/questionsAnswers.json");
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
			if(type == "multiple")
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
			for(int j = 0; j < MCamount; j++)
			{
				Debug.Log(MCquestions[j].incorrectAnswers[2]);
			}
		}

	}

}
