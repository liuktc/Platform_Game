using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour {
	public float Life = 100;
	public Slider Life_Slider;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Life_Slider.value = Life;
		if (Life == 0)
		{
			Application.Quit();
		}
	}

	public void GetDamage(float damage)
	{
		Debug.Log(damage + " damage");
		Life -= damage;
	}
}
