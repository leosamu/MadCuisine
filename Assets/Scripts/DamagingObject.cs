using UnityEngine;
using System.Collections;

public class DamagingObject : MonoBehaviour {

	public float strength = 100.0f;
	public AudioClip damageDealClip;

	public float damageFactor;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(strength < 1.0f) {
			damageFactor = 0.0f;
		}
	}

	public float DealDamage() {
		Debug.Log (strength);
		strength -= 10.0f;
		return damageFactor;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player" || other.tag == "Duck")
		{
			// ... play the pickup sound effect.
			AudioSource.PlayClipAtPoint(damageDealClip, transform.position);

			if(damageFactor == 0.0f) {
				// Destroy the crate.
				Destroy(transform.root.gameObject);
			}
		}
	}
}
