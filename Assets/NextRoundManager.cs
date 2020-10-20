using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextRoundManager : MonoBehaviour
{
    public TextMeshProUGUI m_questionText;

    // Start is called before the first frame update
    void Start()
    {
        m_questionText.text = "";
    }

    public void ShowScreen(string questionText)
    {
        m_questionText.text = questionText;
        gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        m_questionText.text = "";
        gameObject.SetActive(false);
    }
}
