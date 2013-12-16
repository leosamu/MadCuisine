using UnityEngine;
using System.Collections;

public class DuckStatus : Damageable
{	
//	public float health 	= 100f;					// The player's health.
	public float doomness	= 0f;					// Time warming up doom effects while held by the player

//	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
//	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public AudioClip[] yellClips;				// Array of clips to play when the player is damaged.
//	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
//	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player

	//private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	//private float lastHitTime;					// The time at which the player was last hit.
	//private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	//private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player

	/*
	void Awake ()
	{
		// Setting up references.
		//playerControl = GetComponent<PlayerControl>();
		healthBar = GameObject.Find("DuckHealthBarFill").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
//		healthScale = healthBar.transform.localScale;
	}

	//*/
	void OnCollisionEnter2D (Collision2D col)
	{
		/*
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "DamagingObject")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod)
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col); 
					lastHitTime = Time.time; 
				}
				// If the duck doesn't have health, do some stuff, let him fall into the river to reload the level.
				else
				{
					// Find all of the colliders on the gameobject and set them all to be triggers.
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols)
					{
						c.isTrigger = true;
					}

					// Move all sprite parts of the duck to the front
					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer s in spr)
					{
						s.sortingLayerName = "GUI";
					}
					// ... Trigger the 'KABOOM' animation state
					anim.SetTrigger("KABOOM");
				}
			}
		}
		else*/ if(col.gameObject.tag == "Player") 	// If the player enters the trigger zone...
		{
			Debug.Log("Player can has Duck" + col.ToString());
			// ... play the pickup sound effect.
			//AudioSource.PlayClipAtPoint(pickupClip, transform.position);

			col.gameObject.GetComponent<PlayerControl>().canHasDuck();
		}
	}

	void OnCollisionExit2D (Collision2D col)
	{
		if(col.gameObject.tag == "Player") 	// If the player enters the trigger zone...
			col.gameObject.GetComponent<PlayerControl>().noCanHasDuck();
	}
	/*
	void TakeDamage (Collision2D damaging)
	{
		// Make sure the player can't jump.
//		playerControl.jump = false;

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - damaging.transform.position + Vector3.up * 5f;

		DamagingObject d = damaging.gameObject.GetComponent<DamagingObject>();
		// Add a force to the player in the direction of the vector and multiply by the hurtForce.

		float damageReceived = d.DealDamage();
		rigidbody2D.AddForce(hurtVector * hurtForce * damageReceived);

		// Reduce the player's health by 10.
		health -= damageReceived;

		// Update what the health bar looks like.
		UpdateHealthBar();

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}


	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		//healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
	*/
}
