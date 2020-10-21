using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWinPopup : MonoBehaviour
{
    public TextMeshProUGUI m_winText;
    public Image m_fadeImage;

    private bool fadeIn = false;
    private bool fadeOut = false;

    const float WIN_TEXT_MAX_FADE = 1f;
    const float WIN_TEXT_MAX_SIZE = 150f;
    const float WIN_TEXT_SIZE_STEP = 0.5f;
    const float FADE_IMAGE_MAX_FADE = 0.6f;
    const float FADE_STEP = 0.005f;



    // Start is called before the first frame update
    void Start()
    {
        m_winText.color = new Color(0f, 0.603f, 0.2824192f, 0f);
        m_fadeImage.color = new Color(0f, 0f, 0f, 0f);
        m_winText.fontSize = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn || fadeOut)
        {
            FadeScreen();
        }
    }

    private void FadeScreen()
    {
        if (fadeIn)
        {
            fadeIn = false;
            if (m_winText.color.a < WIN_TEXT_MAX_FADE)
            {
                fadeIn = true;
                m_winText.color = new Color(0f, 0.603f, 0.2824192f, Mathf.Min(m_winText.color.a + FADE_STEP, WIN_TEXT_MAX_FADE));
            }
            if (m_winText.fontSize < WIN_TEXT_MAX_SIZE)
            {
                fadeIn = true;
                m_winText.fontSize = Mathf.Min(m_winText.fontSize + WIN_TEXT_SIZE_STEP, WIN_TEXT_MAX_SIZE);
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
            if (m_winText.color.a > 0f)
            {
                fadeOut = true;
                m_winText.color = new Color(0f, 0.603f, 0.2824192f, Mathf.Max(m_winText.color.a - FADE_STEP, 0f));
                //Set alpha to 0 when it is very small, as it was behaving weirdly
                if (m_winText.color.a < 0.001f)
                {
                    m_winText.color = new Color(0f, 0f, 0f, 0f);
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
