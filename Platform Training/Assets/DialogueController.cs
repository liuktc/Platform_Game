using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour {
	GameObject Player;
	//bool speaked = false;
	bool speaked_first = false;
	bool speaking = false;


	public bool Loop_Last_Speech;
	public int Number_Of_Speech;

	int Sentences_Speaked = 0;
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
		if (speaked_first == false)
		{
			GetComponent<DialogueTrigger>().TriggerDialogue();
			Sentences_Speaked++;
			speaked_first = true;
		}
		else
		{
			if (Sentences_Speaked < Number_Of_Speech)
			{
				Sentences_Speaked++;
				//Debug.Log(Sentences_Speaked + "nd dialogue");
				transform.Find(Sentences_Speaked + "_dialogue").gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
			}
			else
			{
				if (Number_Of_Speech == 1)
				{
					GetComponent<DialogueTrigger>().TriggerDialogue();
				}
				else
				{
					if (Loop_Last_Speech == true)
					{
						//Debug.Log(Sentences_Speaked + "nd dialogue");
						transform.Find(Sentences_Speaked + "_dialogue").gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
					}
				}
			}
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
