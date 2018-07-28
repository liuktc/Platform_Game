using UnityEngine;
[System.Serializable]
public class Attack{
	public float attack_Force_x;
	public float attack_Force_y;
	public float attack_Force_Time;
	public float attack_Damage;
	public float attack_Vertical_Multiplicator;

	public float attackRate;
	[HideInInspector]
	public float lastAttack;

	public string triggerAnimationName;

	public bool isDirectional;
	public string triggerAnimationNameLeft;
	public string triggerAnimationNameRight;
}
