using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static float roundTimer = 0;

    [SerializeField] private TextMeshProUGUI timerText;

    void Start()
    {
        ResetTimer();
        StartCoroutine(Round());
    }

    private IEnumerator Round()
    {
        while (Time.time < roundTimer)
        {
            //Debug.Log("Timer running");
            yield return null;
        }

        foreach (Player player in FindObjectsOfType<Player>())
        {
            player.RegisterRoundDone();
        }
    }

    void Update()
    {
        timerText.text = "" + TimeLeft;
    }

    private void ResetTimer() => roundTimer = Time.time + 10f;


    public float TimeLeft => (roundTimer - Time.time > 0) ? Mathf.Round((roundTimer - Time.time) * 10) / 10 : 0;

}
