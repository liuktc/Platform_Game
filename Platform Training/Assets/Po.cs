using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Po : MonoBehaviour {
	GameObject Player;
	bool speaked = false;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (distance(transform.position.x,transform.position.y,Player.transform.position.x,Player.transform.position.y) <= 3 && speaked == false)
		{
			GetComponent<DialogueTrigger>().TriggerDialogue();
			speaked = true;
		}
	}

	float distance(float x1,float y1,float x2,float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}
}
