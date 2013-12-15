using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{	
	public float health = 100f;					// The entity's health.
	public float repeatDamagePeriod = 2f;		// How frequently the entity can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the entity is damaged.
	public float hurtForce = 0f;				// The force with which the entity is pushed when hurt.
	public float damageResistance = 1.0f;		// The amount of damage to take when enemies touch the player

	public SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the entity was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).

	void Awake ()
	{
		// Setting up references.
		//healthBar = GameObject.Find("HealthBarFill").GetComponent<SpriteRenderer>();
		// Getting the intial scale of the healthbar (whilst the entity has full health).
		healthScale = healthBar.transform.localScale;
	}

	/*
	void OnCollisionEnter2D (Collision2D col)
	{
		
		Debug.Log ("Player collision");
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
				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
				else
				{
					// Find all of the colliders on the gameobject and set them all to be triggers.
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols)
					{
						c.isTrigger = true;
					}

					// Move all sprite parts of the player to the front
					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer s in spr)
					{
						s.sortingLayerName = "GUI";
					}

					// ... disable user Player Control script
					GetComponent<PlayerControl>().enabled = false;

					// ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
					GetComponentInChildren<Gun>().enabled = false;

					// ... Trigger the 'Die' animation state
					anim.SetTrigger("Die");
				}
			}
		}
	}
	*/

	public void TakeDamage (DamagingObject damaging)
	{		
		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - damaging.transform.position + Vector3.up * 5f;
		
		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		float damageReceived = damaging.DealDamage();
		rigidbody2D.AddForce(hurtVector * hurtForce * damageReceived);

		Debug.Log (ToString() + " taking " + damageReceived + " damage!");
		// Reduce the player's health by 10.
		health -= damageReceived / (damageResistance + 1.0f);
		
		// Update what the health bar looks like.
		UpdateHealthBar();
		
		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the entity's health.
		//healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the entity's health.
		Debug.Log ("Updating Health: " + healthScale.x + " " + health);
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}
