/*/
* Script by Devin Curry
	* www.Devination.com
		* www.youtube.com/user/curryboy001
		* Please like and subscribe if you found my tutorials helpful :D
			/*/
			using UnityEngine;
using System.Collections;

public class TouchLogic : MonoBehaviour 
{
	public static int currTouch = 0;//so other scripts can know what touch is currently on screen
	private Ray ray;//this will be the ray that we cast from our touch into the scene
	private RaycastHit rayHitInfo = new RaycastHit();//return the info of the object that was hit by the ray
	[HideInInspector]
	public int touch2Watch = 64;

	public virtual void OnTouchBegan()	{}
	public virtual void OnTouchEnded()	{}
	public virtual void OnTouchMoved()	{}
	public virtual void OnTouchStayed() {}
	public virtual void OnTouchBeganAnyWhere(){}
	public virtual void	OnTouchBegan3D(GameObject obj){}
	public virtual void OnTouchEndedAnywhere(){}
	public virtual void OnTouchEnded3D(GameObject obj){}
	public virtual void OnTouchMovedAnywhere(){}
	public virtual void OnTouchMoved3D(GameObject obj){}
	public virtual void OnTouchStayedAnywhere(){}
	public virtual void OnTouchStayed3D(GameObject obj){}

	void Update () 
	{
		//is there a touch on screen?
		if(Input.touches.Length <= 0)
		{
			//if no touches then execute this code
		}
		else //if there is a touch
		{
			//loop through all the the touches on screen
			for(int i = 0; i < Input.touchCount; i++)
			{
				currTouch = i;
				//executes this code for current touch (i) on screen
				if(this.guiTexture != null && (this.guiTexture.HitTest(Input.GetTouch(i).position)))
				{
					//if current touch hits our guitexture, run this code
					if(Input.GetTouch(i).phase == TouchPhase.Began)
					{
						OnTouchBegan();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended)
					{					
						OnTouchEnded();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Moved)
					{
						OnTouchMoved();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Stationary)
					{
						OnTouchStayed();
					}
				}
				
				//outside so it doesn't require the touch to be over the guitexture
				ray = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(i).position);//creates ray from screen point position
				switch(Input.GetTouch(i).phase)
				{
				case TouchPhase.Began:
					//OnTouchBeganAnywhere();
					OnTouchBeganAnyWhere();
					if(Physics.Raycast(ray, out rayHitInfo))
						OnTouchBegan3D(rayHitInfo.transform.gameObject);
					break;
				case TouchPhase.Ended:
					//OnTouchEndedAnywhere();
					OnTouchEndedAnywhere();
					if(Physics.Raycast(ray, out rayHitInfo))
						OnTouchEnded3D(rayHitInfo.transform.gameObject);
					break;
				case TouchPhase.Moved:
					//OnTouchMovedAnywhere();
					OnTouchMovedAnywhere();
					if(Physics.Raycast(ray, out rayHitInfo))
						OnTouchMoved3D(rayHitInfo.transform.gameObject);
					break;
				case TouchPhase.Stationary:
					//OnTouchStayedAnywhere();
					OnTouchStayedAnywhere();
					if(Physics.Raycast(ray, out rayHitInfo))
							OnTouchStayed3D(rayHitInfo.transform.gameObject);
					break;
				}
			}
		}
	}
}