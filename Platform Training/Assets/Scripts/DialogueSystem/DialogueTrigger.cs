using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
	public void StopDialogue()
	{
		FindObjectOfType<DialogueManager>().EndDialogue();
	}
	public bool IsDialogueEnded()
	{
		if (FindObjectOfType<DialogueManager>().sentences.Count == 0)
		{
			return true;
		}
		return false;
	}

}
