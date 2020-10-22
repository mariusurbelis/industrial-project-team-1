using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBehaviour : MonoBehaviour
{
	[SerializeField] private int optionID = -1;
	public GameObject highlight;
    /// <summary>
    /// Checks if a player movement collides with the gameObject attached to.
	/// If a player object is detected the player's selected option will be set.
    /// </summary>
    /// <param name="collider">Colider used to check if a player object is intersecting</param>
    void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			Player player = collider.gameObject.GetComponent<Player>();
			player.selectedOption = optionID;
			if(player.IsMe)
            {
				highlight.SetActive(true);
			}
			
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
			Player player = collider.gameObject.GetComponent<Player>();
			player.selectedOption = -1;
			if (player.IsMe)
			{
				highlight.SetActive(false);
			}
		}
	}
}
