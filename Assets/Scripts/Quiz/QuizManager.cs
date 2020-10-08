using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] options;

	void LoadQuestion()
	{
		Question question = QuestionManager.GetQuestion();
		options[0].GetComponentInChildren<TextMeshProUGUI>().text = question.correctAnswer;
		options[1].GetComponentInChildren<TextMeshProUGUI>().text = question.falseAnswer;
	}
	private void Start()
	{
		LoadQuestion();
	}
}
