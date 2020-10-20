using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextRoundManager : MonoBehaviour
{
    public TextMeshProUGUI m_questionText;
    public Image m_fadeImage;
    public Image m_questionBoxImage;

    private bool fadeIn = false;
    private bool fadeOut = false;

    const float QUESTION_BOX_MAX_FADE = 1f;
    const float FADE_IMAGE_MAX_FADE = 0.6f;
    const float FADE_STEP = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        m_questionText.text = "";
        fadeIn = false;
        fadeOut = false;
    }

    void Update()
    {
        if(fadeIn || fadeOut)
        {
            FadeScreen();
        }
    }

    private void FadeScreen()
    {
        if (fadeIn)
        {
            fadeIn = false;
            if (m_questionBoxImage.color.a < QUESTION_BOX_MAX_FADE)
            {
                fadeIn = true;
                m_questionBoxImage.color = new Color(1f, 1f, 1f, Mathf.Min(m_questionBoxImage.color.a + FADE_STEP, QUESTION_BOX_MAX_FADE));
            }
            if (m_fadeImage.color.a < FADE_IMAGE_MAX_FADE)
            {
                fadeIn = true;
                m_fadeImage.color = new Color(0f, 0f, 0f, Mathf.Min(m_fadeImage.color.a + FADE_STEP, FADE_IMAGE_MAX_FADE));
            }
        }

        if (fadeOut)
        {
            fadeOut = false;
            if (m_questionBoxImage.color.a > 0)
            {
                fadeOut = true;
                m_questionBoxImage.color = new Color(1f, 1f, 1f, Mathf.Max(m_questionBoxImage.color.a - FADE_STEP, 0f));
            }
            if (m_fadeImage.color.a > 0)
            {
                fadeOut = true;
                m_fadeImage.color = new Color(0f, 0f, 0f, Mathf.Max(m_fadeImage.color.a - FADE_STEP, 0f));
            }

            if (!fadeOut && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ShowScreen(string questionText)
    {
        m_questionText.text = questionText;
        gameObject.SetActive(true);
        fadeIn = true;
    }

    public void HideScreen()
    {
        m_questionText.text = "";
        fadeOut = true;
    }
}
