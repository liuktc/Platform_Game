using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	public GameObject baseDot;

	void Update()
	{
		Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		if (Input.GetMouseButtonDown(0))
		{
			Instantiate(baseDot, objPosition, baseDot.transform.rotation);
		}
	}
}