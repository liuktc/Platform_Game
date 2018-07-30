using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;	public bool useController;

	void Awake()
	{
		if (instance != null)
		{
            Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Update () {
		
	}
}
