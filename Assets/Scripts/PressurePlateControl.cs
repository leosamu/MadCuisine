using UnityEngine;
using System.Collections;

public class PressurePlateControl : MonoBehaviour
{
	public AudioClip switchClip;		// Sound for when the bomb crate is picked up.


	private SpriteRenderer spriteON, spriteOFF;
	private bool active = false;

	void Awake()
	{
		// Setting up the reference.
		spriteOFF = transform.Find("pressPlateOFFSprite").gameObject;
		spriteON = transform.Find("pressPlateONSprite").gameObject;
		spriteOFF.sortingOrder = 1;
	}

	void FixedUpdate () {
		active = false;
		spriteON.sortingOrder = 0;
		spriteOFF.sortingOrder = 1;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player" || other.tag == "Duck")
		{
			if(!active)
			{
				// ... play the pickup sound effect.
				AudioSource.PlayClipAtPoint(switchClip, transform.position);
				// Increase the number of bombs the player has.
				spriteON.sortingOrder = 1;
				spriteOFF.sortingOrder = 0;
				active = true;
			}

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
