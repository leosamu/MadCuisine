using UnityEngine;
using System.Collections;

public class Joystick : TouchLogic 
{
	public PlayerControl player;
	public float maxJoyDelta = 0.05f;
	public float rotateSpeed = 100.0f;
	private Vector3 oJoyPos; 
	public Vector3 joyDelta;
	private Transform joyTrans = null;

	//[NEW]//cache initial rotation of player so pitch and yaw don't reset to 0 before rotating
	private Vector3 oRotation;
	void Start () 
	{
		joyTrans = this.transform;
		oJoyPos = joyTrans.position;
	}
	
	public override void OnTouchBegan()
	{
		//Used so the joystick only pays attention to the touch that began on the joystick
		touch2Watch = TouchLogic.currTouch;
	}
	
	public override void OnTouchMovedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			//move the joystick
			joyTrans.position = MoveJoyStick();
			ApplyDeltaJoy();
		}
	}
	
	public override void OnTouchStayedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			ApplyDeltaJoy();
		}
	}
	
	public override void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe so joystick never gets stuck with no fingers on screen
		if(TouchLogic.currTouch == touch2Watch || Input.touches.Length <= 0)
		{
			//move the joystick back to its orig position
			joyTrans.position = oJoyPos;
			joyDelta= new Vector3(0,0,0);
			ApplyDeltaJoy();
			touch2Watch = 64;
		}
	}
	
	void ApplyDeltaJoy()
	{
		//send move message
			player.h=joyDelta.x;
			player.v=joyDelta.z;


	}
	
	Vector3 MoveJoyStick()
	{
		//convert the touch position to a % of the screen to move our joystick
		float x = Input.GetTouch (touch2Watch).position.x / Screen.width,
		y = Input.GetTouch (touch2Watch).position.y / Screen.height;
		//combine the floats into a single Vector3 and limit the delta distance
		Vector3 position = new Vector3 (Mathf.Clamp(x, oJoyPos.x - maxJoyDelta, oJoyPos.x + maxJoyDelta),
		                                Mathf.Clamp(y, oJoyPos.y - maxJoyDelta, oJoyPos.y + maxJoyDelta), 0);
		//joyDelta used for moving the player
		joyDelta = new Vector3(position.x - oJoyPos.x, 0, position.y - oJoyPos.y).normalized;
		//position used for moving the joystick
		return position;
	}
	
	void LateUpdate()
	{

	}
}