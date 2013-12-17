using UnityEngine;
using System.Collections;

public class PressurePlateControl : SwitchControl
{
	public Switchable[] related_objects;

	public override void switchedON(){
		Debug.Log (ToString() + " switched ON");
	}

	public override void switchedOFF(){
		Debug.Log (ToString() + " switched OFF");
	}

	public override void switchStayON(){
		//Debug.Log (ToString() + " is ON"); //oh shut up already
	}
}
