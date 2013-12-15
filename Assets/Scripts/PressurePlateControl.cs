using UnityEngine;
using System.Collections;

public class PressurePlateControl : MonoBehaviour
{
	public AudioClip switchClip;		// Sound for when the bomb crate is picked up.


	private Sprite spriteON, spriteOFF;
	private bool _active = false;

	void Awake()
	{
		Object pressPlateStates = Resources.Load("pressure-plate_0");
		Debug.Log("Object loaded:" + pressPlateStates.ToString());
		Sprite pressPlateState = Resources.Load<Sprite>("pressure-plate.png");
		//Sprite[] pressPlateStates = Resources.LoadAll<Sprite>("Art/Objects/pressure-plate");
		spriteOFF = pressPlateState;
		//spriteON = pressPlateStates[1];
		//spriteOFF = Resources.Load<Sprite>("Art/Objects/pressure-plate_0");
		//spriteON = Resources.Load<Sprite>("Art/Objects/pressure-plate_1");
		GetComponent<SpriteRenderer>().sprite = spriteOFF;
	}

	void FixedUpdate () {
		_active = false;
		GetComponent<SpriteRenderer>().sprite = spriteOFF;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player" || other.tag == "Duck")
		{
			if(!_active)
			{
				// ... play the pickup sound effect.
				AudioSource.PlayClipAtPoint(switchClip, transform.position);
			}
			GetComponent<SpriteRenderer>().sprite = spriteON;

			// Destroy the crate.
			//Destroy(transform.root.gameObject);
		}
		/*
		// Otherwise if the crate lands on the ground...
		else if(other.tag == "ground" && !landed)
		{
			// ... set the animator trigger parameter Land.
			anim.SetTrigger("Land");
			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;		
		}
		*/
	}
}
