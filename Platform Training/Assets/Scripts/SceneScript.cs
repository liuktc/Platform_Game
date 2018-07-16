using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour {
	public string SceneToLoad;

	GameObject Player;
	// Use this for initialization
	void Start()
	{
		Player = GameObject.FindWithTag("Player");
	}
	public void Collision(Collider2D col)
	{
		Debug.Log("AA");
		if (col.tag == "Player")
		{
			SceneManager.LoadScene(SceneToLoad);
		}
	}
}
