using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDeathPopup : MonoBehaviour
{
    public TextMeshProUGUI m_deathText;
    public Image m_fadeImage;

    private bool fadeIn = false;
    private bool fadeOut = false;

    const float DEATH_TEXT_MAX_FADE = 1f;
    const float DEATH_TEXT_MAX_SIZE = 100f;
    const float DEATH_TEXT_SIZE_STEP = 0.09f;
    const float FADE_IMAGE_MAX_FADE = 0.6f;
    const float FADE_STEP = 0.005f;



    // Start is called before the first frame update
    void Start()
    {
        m_deathText.color = new Color(1f, 0f, 0.1665177f, 0f);
        m_fadeImage.color = new Color(0f, 0f, 0f, 0f);
        m_deathText.fontSize = 72f;
    }

    // Update is called once per frame
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
            if (m_deathText.color.a < DEATH_TEXT_MAX_FADE)
            {
                fadeIn = true;
                m_deathText.color = new Color(1f, 0f, 0.1665177f, Mathf.Min(m_deathText.color.a + FADE_STEP, DEATH_TEXT_MAX_FADE));
            }
            if(m_deathText.fontSize < DEATH_TEXT_MAX_SIZE)
            {
                fadeIn = true;
                m_deathText.fontSize = Mathf.Min(m_deathText.fontSize + DEATH_TEXT_SIZE_STEP, DEATH_TEXT_MAX_SIZE);
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
            if (m_deathText.color.a > 0f)
            {
                fadeOut = true;
                m_deathText.color = new Color(1f, 0f, 0.1665177f, Mathf.Max(m_deathText.color.a - FADE_STEP, 0f));
                //Set alpha to 0 when it is very small, as it was behaving weirdly
                if (m_deathText.color.a < 0.001f)
                {
                    m_deathText.color = new Color(0f, 0f, 0f, 0f);
                }
            }
            if (m_fadeImage.color.a > 0f)
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

    public void ShowScreen()
    {
        gameObject.SetActive(true);
        fadeIn = true;
        fadeOut = false;
    }

    public void HideScreen()
    {
        fadeOut = true;
        fadeIn = false;
    }
}
