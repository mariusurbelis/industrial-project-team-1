using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerID : MonoBehaviour
{
    private TextMeshProUGUI nameText;

    private void Awake()
    {
        nameText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        nameText.text = Random.Range(1, 9).ToString();
    }
}
