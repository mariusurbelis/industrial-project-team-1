using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBehaviour : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			Debug.Log("hit" + this.gameObject.name);
		}
	}
}
