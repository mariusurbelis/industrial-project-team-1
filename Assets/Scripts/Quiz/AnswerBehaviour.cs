using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBehaviour : MonoBehaviour
{
	[SerializeField] private int optionID = -1;
    /// <summary>
    /// Checks if a player movement collides with the gameObject attached to.
	/// If a player object is detected the player's selected option will be set.
    /// </summary>
    /// <param name="collider">Colider used to check if a player object is intersecting</param>
    void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			collider.gameObject.GetComponent<Player>().selectedOption = optionID;
		}
	}
	/// <summary>
	/// Checks if a player movement collides with the gameObject attached to.
	/// If a player object is detected the player's selected option will be set.
	/// When player is no longer detected, player's selected options is set to -1.
	/// </summary>
	/// <param name="collider">Colider used to check if a player object is intersecting</param>
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			collider.gameObject.GetComponent<Player>().selectedOption = -1;
		}
	}
}
