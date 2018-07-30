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
	public bool autoPlay;
	public KeyCode buttonKeyboardTrigger;

	public float distanceTrigger;

	int Sentences_Speaked = 0;
	float PlayerStats;

	public Animator anim;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame


	float distance(float x1,float y1,float x2,float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0, 255, 255, 0.3f);
		Gizmos.DrawSphere(transform.position, distanceTrigger);	}

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
				Debug.Log(Sentences_Speaked + "nd dialogue");
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
						Debug.Log(Sentences_Speaked + "nd dialogue");
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
		if (speaking && Input.GetKeyDown(buttonKeyboardTrigger))
		{
			FindObjectOfType<DialogueManager>().DisplayNextSentence();
			anim.SetBool("in", false);
		}
		if (dis <= distanceTrigger && speaking == false)
		{
			anim.SetBool("in", true);
			if (autoPlay)
			{
				Speak();
			}
			else
			{
				if (Input.GetKeyDown(buttonKeyboardTrigger))
				{
					Speak();
				}
			}
		}
		if(dis > distanceTrigger)
		{
			anim.SetBool("in", false);
			StopSpeak();
		}
		if (!GetComponent<DialogueTrigger>().IsDialogueEnded())
		{
			DisablePlayerMovement();
		}
		else
		{
			EnablePlayerMovement();
		}
		if (Number_Of_Speech == Sentences_Speaked && !Loop_Last_Speech)
		{
			anim.SetBool("in", false);
		}
		if (speaking)
		{
			anim.SetBool("in", false);
		}	}
	void PlayAnimation()
	{
		anim.SetBool("in", true);
	}
}
