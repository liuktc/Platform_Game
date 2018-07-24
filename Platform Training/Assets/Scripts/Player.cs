using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {


	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	public float powerOf2ndJump = .5f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float maxJumpVelocity2ndJump;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
	bool secondJumpAvaible;

	Controller2D controller;

	Vector2 directionalInput;
	Vector2 directionalInputAdd;
	bool wallSliding;
	int wallDirX;

	public Text DebugTest;

	private GameObject hand;

	float PlayerAngle;

	void Start() {
		hand = GameObject.FindWithTag("Hand");
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		maxJumpVelocity2ndJump = Mathf.Abs(gravity) * timeToJumpApex * powerOf2ndJump;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	void Update() {
		PlayerAngle = MouseAngle();
		CalculateFlip();
		if (controller.collisions.below || controller.collisions.left || controller.collisions.right)
		{
			secondJumpAvaible = false;
		}
		//OnJumpInputDown();

		//DebugTest.text = " Up = " + controller.collisions.above + "\n Down = " + controller.collisions.below + "\n Right = " + controller.collisions.right + "\n Left = " + controller.collisions.left + "\n 2nd jump = " + secondJumpAvaible;

		CalculateVelocity ();
		HandleWallSliding ();

		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}

	}
	float MouseAngle()
	{
		float MouseX = Input.mousePosition.x;
		float MouseY = Input.mousePosition.y;

		Camera MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		GameObject Origin = gameObject;

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


	void CalculateFlip()
	{
		if ((PlayerAngle <= 90 && PlayerAngle >= 0) || (PlayerAngle >= 270 && PlayerAngle <= 360))
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else {
			GetComponent<SpriteRenderer>().flipX = false;
		}
	}
	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input + directionalInputAdd;
		//Debug.Log(directionalInput);
	}

	public void AddDirectionalInput(Vector2 Force, float time)
	{
		directionalInputAdd = Force;
		//Debug.Log("Added " + directionalInputAdd);
        StartCoroutine(Example(time));
		//directionalInputAdd = new Vector2(0, 0);
	}
	IEnumerator Example(float time)
	{
		//print(Time.time);
		yield return new WaitForSeconds(time);
		directionalInputAdd = new Vector2(0, 0);
		//print(Time.time);	 }

	public void OnJumpInputDown()
	{
		if (controller.collisions.rightCol)
		{
			if (wallSliding && controller.collisions.rightCol.tag != "NoHit")
			{
				secondJumpAvaible = true;
				controller.collisions.right = false;
				controller.collisions.left = false;
				if (wallDirX == directionalInput.x)
				{
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (directionalInput.x == 0)
				{
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else
				{
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
		}
		if (controller.collisions.leftCol)
		{
			if (wallSliding && controller.collisions.leftCol.tag != "NoHit")
			{
				secondJumpAvaible = true;
				controller.collisions.right = false;
				controller.collisions.left = false;
				if (wallDirX == directionalInput.x)
				{
					velocity.x = -wallDirX* wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (directionalInput.x == 0)
				{
					velocity.x = -wallDirX* wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else
				{
					velocity.x = -wallDirX* wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
		}
		if (controller.collisions.below || secondJumpAvaible == true)
			{
					if (controller.collisions.below)
					{
						secondJumpAvaible = true;
						controller.collisions.below = false;
					}
					else
					{
						secondJumpAvaible = false;
					}
					if (controller.collisions.slidingDownMaxSlope)
					{
						if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
						{ // not jumping against max slope
							velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
							velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
						}
					}
					else
					{
						if (secondJumpAvaible)
						{
							velocity.y = maxJumpVelocity;
						}
						else
						{
							velocity.y = maxJumpVelocity2ndJump;
						}
					}
				}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}
		

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += (gravity * Time.deltaTime) + (directionalInput.y);
		//directionalInputAdd = new Vector2(0,0);
	}
}
