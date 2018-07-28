using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

	//[HideInInspector]
	public float Angle;
	//float add = 0;
	float lastAngle;

	public Text DebugText;

	//[HideInInspector]
	//public GameObject Weapon_Pivot;
	/*[HideInInspector]
	public Collider2D Weapon_Collider;*/
	//[HideInInspector]
	//public GameObject Weapon_Sprite;
	[HideInInspector]
	public WeaponStatus Weapon_Status;
	//[HideInInspector]
	public GameObject Player;

	[HideInInspector]
	public bool left, right,lastLeft,lastRight;

	public GameObject Projectile;
	public float FireRate;
	private float LastFire;

	/*public Animator Anim_Weapon;
	public Animator Anim_Angle;*/

	/*public float Attack1_Force_x;
	public float Attack1_Force_y;
	public float Attack1_Force_Time;
	public float Attack1_Damage;
	public float Attack1_Vertical_Multiplicator;

	public float Attack1Rate;
	private float LastAttack1;

	public float Attack3Rate;
	private float LastAttack3;*/

	public Attack[] attackList = new Attack[2];
	[HideInInspector]
	public bool isAttacking;
	[HideInInspector]
	public int attackIndex;

	public Animator UIAttackAnimator;
	public Animator UIAttack2Animator;
	public Animator WeaponAnimator;

	void Start()
	{
		//Anim = transform.FindChild("Weapon").GetComponent<Animator>()
		Player = GameObject.FindWithTag("Player");
		//Weapon_Pivot = GameObject.FindWithTag("Weapon");
		//Weapon_Collider = GameObject.Find("WeaponSprite_Collider").GetComponent<Collider2D>();
		//Weapon_Sprite = GameObject.Find("WeaponSprite_Collider");
		if (Input.GetJoystickNames().Length > 0)
		{
			//Debug.Log("Controller connected");
		}
	}
	void Update () {
		/*if (Input.GetJoystickNames().Length > 0){
			//Debug.Log("Controller connected");
			Angle = JoystickAngle();
		}
		else {
			//Debug.Log("No controller connected");
			Angle = MouseAngle();
		}*/

		Angle = MouseAngle();
		//add = 0;

		//Debug.Log(Weapon_Status.Attack1);

		ApplyAngle();

		lastLeft = left;
		lastRight = right;

		UIAttack2Animator.SetBool("Enabled", Player.GetComponent<PlayerInput>().Attack2Active);
	}

	float JoystickAngle()
	{
		float Horizontal = Input.GetAxis("RightStickX");
		float Vertical = Input.GetAxis("RightStickY");

		if ((Horizontal<0.05 && Horizontal > - 0.05) && (Vertical < 0.05 && Vertical > -0.05) && lastLeft){
			return 180;
		}
		return -Mathf.Atan2(Vertical, Horizontal) * Mathf.Rad2Deg;
	}

	float MouseAngle()
	{
		float MouseX = Input.mousePosition.x;
		float MouseY = Input.mousePosition.y;

		Camera MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		GameObject Origin = Player.gameObject;

		float OriginX = MainCamera.WorldToScreenPoint(Origin.transform.position).x;
		float OriginY = MainCamera.WorldToScreenPoint(Origin.transform.position).y;

		float relMouseX = MouseX - OriginX;
		float relMouseY = MouseY - OriginY;

		float AngleTemp = Mathf.Atan(Mathf.Abs(relMouseY) / Mathf.Abs(relMouseX)) * Mathf.Rad2Deg;

		if (relMouseX >= 0 && relMouseY >= 0)
		{
			return AngleTemp;
		}
		if (relMouseX < 0 && relMouseY >= 0)
		{
			return 180 - AngleTemp;
		}
		if (relMouseX < 0 && relMouseY < 0)
		{
			return AngleTemp + 180;
		}
		if (relMouseX >= 0 && relMouseY < 0)
		{
			return 360 - AngleTemp;
		}
		return 0;
	}

	void ApplyAngle()
	{
		if (Angle < 0)
		{
			Angle += 360;
		}
		//ClampAngle();
		CalculateFlip();

		//DebugText.text = "α = " + Angle;
		transform.eulerAngles = new Vector3(0,0,Angle);
	}

	void ClampAngle()
	{
		if (Angle >= 230 && Angle <= 270)
		{
			Angle = 230;
		}
		if (Angle >= 270 && Angle <= 310)
		{
			Angle = 310;
		}
	}

	void CalculateFlip()
	{
		if ((Angle <= 90 && Angle >= 0) || (Angle >= 270 && Angle <= 360))
		{
			right = true;
			left = false;

			GameObject.FindWithTag("Weapon").transform.localScale = new Vector3(1, 1, 1);
		}
		if ((Angle > 90 && Angle <= 180) || (Angle < 270 && Angle >= 180))
		{
			right = false;
			left = true;
			GameObject.FindWithTag("Weapon").transform.localScale = new Vector3(1, -1, 1);
		}
	}

	public void Attack(int index)
	{
		if (Time.time > attackList[index].attackRate + attackList[index].lastAttack)
		{
			attackIndex = index;
			isAttacking = true;
			AddForce(index);
			attackList[index].lastAttack = Time.time;
			if (attackList[index].isDirectional)
			{
				if (leftAngle())
				{
					WeaponAnimator.SetTrigger(attackList[index].triggerAnimationNameLeft);
				}
				else
				{
					WeaponAnimator.SetTrigger(attackList[index].triggerAnimationNameRight);
				}
			}
			else
			{
				WeaponAnimator.SetTrigger(attackList[index].triggerAnimationName);
			}
		}
	}
	public void StopAttack()
	{
		isAttacking = false;
	}
	bool leftAngle()
	{
		if (Angle >= 90 && Angle <= 270)
		{
			return true;
		}
		return false;
	}
	/*public void Attack1()
	{
		if (Time.time > Attack1Rate + LastAttack1)
		{
			UIAttackAnimator.SetTrigger("Use");
			Weapon_Status.Attack1 = true;
			//Debug.Log("Attack with angle = " + Angle);
			LastAttack1 = Time.time;
			//AddForce();
			//Anim_Weapon.SetTrigger("Attack");
			if (Angle >= 90 && Angle <= 270)
			{
				WeaponAnimator.SetTrigger("Attack_Left");
			}
			else
			{
				WeaponAnimator.SetTrigger("Attack_Right");
			}
			//add += 30;
		}
	}
	public void Attack1Stop()
	{
		Weapon_Status.Attack1 = false;
	}*/

	public void AttackThrow()
	{
		if (Time.time > FireRate + LastFire)
		{
			GameObject.FindObjectOfType<AudioManager>().Play("Shuriken_Throw");
			UIAttack2Animator.SetTrigger("Use");
			Weapon_Status.Attack2 = true;
			GameObject instance;
			instance = (GameObject)Instantiate(Projectile, gameObject.transform.Find("Pivot").Find("HandSprite").gameObject.transform.position, Quaternion.Euler(0, 0, Angle));
			instance.GetComponent<Projectile>().Fired_By_Player = true;
			Destroy(instance, 5.0f);
			LastFire = Time.time;
		}
	}

	/*public void Attack2()
	{
		if (Time.time > Attack3Rate + LastAttack3)
		{
			WeaponAnimator.SetTrigger("Lunge");
			Weapon_Status.Attack2 = true;
			LastAttack1 = Time.time;
			//AddForce();
		}
	}
	public void Attack3Stop()
	{
		Weapon_Status.Attack2 = false;
	}*/

	public void Defend()
	{
		Weapon_Status.Defend = true;
		//WeaponAnimator.SetBool("IsDefending", true);
		//Weapon_Sprite.transform.localPosition = new Vector3(Weapon_Sprite.transform.localPosition.x, 0.0f, Weapon_Sprite.transform.localPosition.z);	}

	public void Stop_Defend()
	{
		Weapon_Status.Defend = false;
		//WeaponAnimator.SetBool("IsDefending", false);
		//Weapon_Sprite.transform.localPosition = new Vector3(Weapon_Sprite.transform.localPosition.x, 0.35f, Weapon_Sprite.transform.localPosition.z);	}

	public struct WeaponStatus {
		public bool Attack1, Attack2, Defend;
		public void reset()
		{
			Attack1 = Attack2 = Defend = false;
		}
	}

	public void AddForce(int i)
	{
		Vector2 Force;
		if (Player.GetComponent<Controller2D>().collisions.below)
		{
			Force = new Vector2(Mathf.Cos(Angle * Mathf.Deg2Rad) * attackList[i].attack_Force_x * Time.deltaTime, Mathf.Sin(Angle * Mathf.Deg2Rad) * attackList[i].attack_Force_y * Time.deltaTime * attackList[i].attack_Vertical_Multiplicator);
		}
		else
		{
			Force = new Vector2(Mathf.Cos(Angle* Mathf.Deg2Rad) * attackList[i].attack_Force_x* Time.deltaTime, Mathf.Sin(Angle* Mathf.Deg2Rad) * attackList[i].attack_Force_y* Time.deltaTime);
		}
		Player.GetComponent<Player>().AddDirectionalInput(Force, attackList[i].attack_Force_Time);
	}
}
