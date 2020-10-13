using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text m_questionTextComponent;
    public Text m_timerTextComponent;
    public Sprite m_aliveHeartSprite;
    public Sprite m_deadHeartSprite;
    public Component m_heartContainer;  // Container of the heart objects
    public Image[] m_hearts;            // Array of the heart objects

    private int m_maxHearts = 3;

    // Initialises the display with default values
    private void Start()
    {
        SetQuestionText("sample text");
        SetTimerText(30);
        SetCurrentHearts(3);
    }

    // Sets the display for the question
    public void SetQuestionText(string questionText)
    {
        m_questionTextComponent.text = questionText;
    }
    
    // Sets the display for the timer
    public void SetTimerText(int timerText)
    {
        m_timerTextComponent.text = timerText.ToString();
    }

    // Sets the display for the player's current number of hearts
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
}
