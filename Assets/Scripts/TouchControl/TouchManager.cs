/*/
* Script by Devin Curry
	* www.Devination.com
		* www.youtube.com/user/curryboy001
		* Please like and subscribe if you found my tutorials helpful :D
			/*/
using UnityEngine;
using System.Collections;

public class TouchManager : TouchLogic 
{
	public TouchLogic[] touches2Manage;
	
	void OnTouchEndedAnywhere()
	{
		foreach(TouchLogic obj in touches2Manage)
			if(obj.touch2Watch > TouchLogic.currTouch)
				obj.touch2Watch--;
	}
}