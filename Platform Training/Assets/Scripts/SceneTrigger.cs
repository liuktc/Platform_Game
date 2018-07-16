using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
	{
		FindObjectOfType<SceneScript>().Collision(col);
	}
}
