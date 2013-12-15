using UnityEngine;
using System.Collections;

public class DamagingObject : MonoBehaviour {

	public float strength = 100.0f;
	public AudioClip damageDealClip;

	public float 	damageFactor;
	public float	_DPSPeriod = 3.0f;
	private bool 	_active = false;
	private float _enterTime = 0.0f;
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
			if (!_active) {
				_active = true;
				_enterTime = Time.time;
				// ... play the pickup sound effect.
				AudioSource.PlayClipAtPoint(damageDealClip, transform.position);
				Damageable d = other.gameObject.GetComponent<Damageable>();
				d.TakeDamage(this);
			}
			if(damageFactor == 0.0f) {
				// Destroy the crate.
				Destroy(transform.root.gameObject);
			}
		}
	}
	void OnTriggerStay2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player" || other.tag == "Duck")
		{
			_active = true;
			float ctime = Time.time;
			if( ctime >= _enterTime + _DPSPeriod) {
				Debug.Log("Damaging!");
				_enterTime = ctime;
				//Deal Damage
				Damageable d = other.gameObject.GetComponent<Damageable>();
				d.TakeDamage(this);

				AudioSource.PlayClipAtPoint(damageDealClip, transform.position);
				if(damageFactor == 0.0f) {
					// Destroy the crate.
					Debug.Log("Oh, the irony!");
					Destroy(transform.root.gameObject);
				}
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player" || other.tag == "Duck")
		{
			_active = false;
		}
	}
}