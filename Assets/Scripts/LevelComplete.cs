using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {

	bool levelFinished;
	// Use this for initialization
	private Color textColor;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.tag == "Duck")
		{
			levelFinished = true;				
			GameObject.Find("Chef").GetComponent<PlayerControl>().enabled = false;
		}
	}

	void OnGUI() {
		if(levelFinished) {
			GUI.Box(new Rect(Screen.width/2-200, Screen.height/2-150, 400, 300), "If you are here is because you are a winner.");
			if (GUI.Button(new Rect(Screen.width/2 -75 , Screen.height/2 - 50, 150, 100), "Quit")) {
				Application.LoadLevel("SandBox");
				print("Bye!");
			}
		}
		
	}
}
