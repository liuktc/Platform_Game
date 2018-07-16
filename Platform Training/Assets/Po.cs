using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Po : MonoBehaviour {
	GameObject Player;
	//bool speaked = false;
	bool[] speaked = { false, false };
	bool speaking = false;

	float PlayerStats;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame


	float distance(float x1,float y1,float x2,float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	void Speak()
	{
		speaking = true;
		if (speaked[0] == false)
		{
			GetComponent<DialogueTrigger>().TriggerDialogue();
			speaked[0] = true;
		}
		else
		{
			Debug.Log("2nd dialogue");
			transform.Find("2_dialogue").gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
		}
	}
	void StopSpeak()
	{
		speaking = false;
		GetComponent<DialogueTrigger>().StopDialogue();
	}
	void DisablePlayerMovement()
	{
		Player.GetComponent<PlayerInput>().ControlEnabled = false;
	}
	void EnablePlayerMovement()
	{
		Player.GetComponent<PlayerInput>().ControlEnabled = true;
	}
	void Update()
	{
		float dis = distance(transform.position.x, transform.position.y, Player.transform.position.x, Player.transform.position.y);
		if (dis <= 3 && speaking == false)
		{
			Speak();
		}
		if(dis >3)
		{
			StopSpeak();
		}
		if (GetComponent<DialogueTrigger>().IsDialogueEnded() == false)
		{
			DisablePlayerMovement();
		}
		else
		{
			EnablePlayerMovement();
		}	}
}
