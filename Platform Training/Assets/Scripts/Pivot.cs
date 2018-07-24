using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour {
	public void StopAttack()
	{
		if (GameObject.FindWithTag("Hand"))
		{
			GameObject.FindWithTag("Hand").GetComponent<WeaponController>().Attack1Stop();
		}
	}
}
