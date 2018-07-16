using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour {
	public float Max_Health;
	[HideInInspector]
	public float Health;
	public Slider Life_Slider;
	// Use this for initialization
	void Start () {
		SetMaxLife();
	}
	
	// Update is called once per frame
	void Update () {
		Life_Slider.maxValue = Max_Health;
		Life_Slider.value = Health;
		if (Health <= 0)
		{
			SceneManager.LoadScene("Dojo");
		}
	}

	public void GetDamage(float damage)
	{
		Debug.Log(damage + " damage");
		Health -= damage;
	}
	public void SetMaxLife()
	{
		Health = Max_Health;
	}
}
