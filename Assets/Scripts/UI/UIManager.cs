using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public AnswersManager answersManager;

	public TextMeshProUGUI m_questionTextComponent;
	public TextMeshProUGUI m_timerTextComponent;
	public Sprite m_aliveHeartSprite;
	public Sprite m_deadHeartSprite;
	public Sprite m_openTrapdoorSprite;
	public Component m_heartContainer;  // Container of the heart objects
	public Image[] m_hearts;            // Array of the heart objects

	private int m_maxHearts = 3;

	/// <summary>
	/// Sets the question text
	/// </summary>
	/// <param name="questionText">Text to display as the question</param>
	public void SetQuestionText(string questionText)
	{
		m_questionTextComponent.text = questionText;
	}

	/// <summary>
	/// Sets the answers
	/// </summary>
	/// <param name="answerOptions">Array of answer text to display</param>
	/// <param name="order">The order answers should be displayed in</param>
	public void SetAnswers(string[] answerOptions, int[] order)
	{
		answersManager.SetAnswers(answerOptions, order);
	}

	/// <summary>
	/// Set the text on the timer
	/// </summary>
	/// <param name="timerText">Text to display on the timer</param>
	public void SetTimerText(int timerText)
	{
		m_timerTextComponent.text = timerText.ToString();
	}

	/// <summary>
	/// Sets the display for the player's current number of hearts
	/// </summary>
	/// <param name="currentHearts">Number of hearts the player has left</param>
	public void SetCurrentHearts(int currentHearts)
	{
		// Sets each heart to "alive" or "dead"
		for (int i = 0; i < m_maxHearts; i++)
		{
			if (i < currentHearts)
			{
				m_hearts[i].GetComponent<Image>().sprite = m_aliveHeartSprite;  // Alive heart
			}
			else
			{
				m_hearts[i].GetComponent<Image>().sprite = m_deadHeartSprite;   // Dead heart
			}
		}
	}

	/// <summary>
	/// Opens all the trapdoors, except for the correct answer
	/// </summary>
	/// <param name="correctAnswer">The trapdoor to keep closed ie. 0 = topleft, 1 = topright</param>
	public void OpenTrapdoors(int correctAnswer)
	{
		for (int i = 0; i < answersManager.answerArray.Length; i++)
		{
			// Don't open the correct answer
			if (i == correctAnswer)
			{
				continue;
			}
			answersManager.OpenTrapdoor(answersManager.answerArray[i]);
		}
	}

	/// <summary>
	/// Closes all the open trapdoors
	/// </summary>
	public void CloseTrapdoors()
	{
		for (int i = 0; i < answersManager.answerArray.Length; i++)
		{
			Sprite trapdoorSprite = answersManager.answerArray[i].GetComponentInChildren<Image>().sprite;
			// Check if trapdoor is open
			if (trapdoorSprite == m_openTrapdoorSprite)
			{
				answersManager.CloseTrapdoor(answersManager.answerArray[i]);
			}
		}
	}
}
