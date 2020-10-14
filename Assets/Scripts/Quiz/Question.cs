using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question
{
	public string category;
	public string type;
	public string difficulty;
	public string question;
	public string correctAnswer;
	public string[] incorrectAnswers;
	public string incorrectAnswer1;
	public string incorrectAnswer2;
	public string incorrectAnswer3;
	public string falseAnswer;

	/// <summary>
	/// Constructor for multiple choice questions.
	/// </summary>
	/// <param name="category">Category of the question</param>
	/// <param name="type">Type of question (either multiple or boolean)</param>
	/// <param name="difficulty">Difficulty of the question</param>
	/// <param name="question">Question text</param>
	/// <param name="correctAnswer">Correct answer for question</param>
	/// <param name="incorrectAnswer1">First incorrect answer for question</param>
	/// <param name="incorrectAnswer2">Second incorrect answer for question</param>
	/// <param name="incorrectAnswer3">Third incorrect answer for question</param>
	public Question(string category, string type, string difficulty, string question, string correctAnswer, string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
	{
		this.category = category;
		this.type = type;
		this.difficulty = difficulty;
		this.question = question;
		this.correctAnswer = correctAnswer;
		this.incorrectAnswer1 = incorrectAnswer1;
		this.incorrectAnswer2 = incorrectAnswer2;
		this.incorrectAnswer3 = incorrectAnswer3;
		incorrectAnswers = new string[3];
		incorrectAnswers[0] = incorrectAnswer1;
		incorrectAnswers[1] = incorrectAnswer2;
		incorrectAnswers[2] = incorrectAnswer3;
	}
	/// <summary>
	/// Constructor for true/false (boolean) questions.
	/// </summary>
	/// <param name="category">Category of the question</param>
	/// <param name="type">Type of question (either multiple or boolean)</param>
	/// <param name="difficulty">Difficulty of the question</param>
	/// <param name="question">Question text</param>
	/// <param name="correctAnswer">Correct answer for question</param>
	/// <param name="falseAnswer">False answer for question</param>
	public Question(string category, string type, string difficulty, string question, string correctAnswer, string falseAnswer)
	{
		this.category = category;
		this.type = type;
		this.difficulty = difficulty;
		this.question = question;
		this.correctAnswer = correctAnswer;
		this.falseAnswer = falseAnswer;
	}

}
