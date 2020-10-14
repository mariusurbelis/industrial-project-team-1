using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBehaviour : MonoBehaviour
{
	[SerializeField] private int optionID = -1;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			collider.gameObject.GetComponent<Player>().selectedOption = optionID;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			collider.gameObject.GetComponent<Player>().selectedOption = -1;
		}
	}
}
