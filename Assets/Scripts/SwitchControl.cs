using UnityEngine;
using System.Collections;

public abstract class SwitchControl : MonoBehaviour
{
	public AudioClip switchONClip;
	public AudioClip switchOFFClip;
	public AudioClip stayONClip;
	
	
	public Sprite spriteON;
	public Sprite spriteOFF;
	public bool isActive = false;

	public float _callInterval = 5f;

	private int _activations = 0;
	private float _timeActive = 0f;
	private float _timeLastSetON = 0f;
	private float _lastCallTime = 0f;

	public abstract void switchedON();
	public abstract void switchedOFF();
	public abstract void switchStayON();

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other.tag + " entered trigger");
		// If the player enters the trigger zone...
		if(other.tag == "Player" || other.tag == "Duck")
		{
			_activations++;
			Debug.Log ("("+ _activations + " in)");
			if(!isActive)
			{
				GetComponent<SpriteRenderer>().sprite = spriteON;
				if(switchONClip != null)
					AudioSource.PlayClipAtPoint(switchONClip, transform.position);
				_timeLastSetON = Time.time;
				switchedON();
			}
			isActive = true;
		}
	}	
	
	void OnTriggerStay2D (Collider2D other)
	{
		if(other.tag == "Player" || other.tag == "Duck")
		{
			isActive = true;
			if(stayONClip != null)
				AudioSource.PlayClipAtPoint(stayONClip, transform.position);
			_timeActive = Time.time - _timeLastSetON;
			if(_timeActive > _lastCallTime + _callInterval) {
				_lastCallTime = _timeActive;
				Debug.Log (_activations + "things staying in the switch");
				switchStayON();
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		Debug.Log (other.tag + " exited trigger");
		if(other.tag == "Player" || other.tag == "Duck")
		{
			_activations--;
			Debug.Log ("("+ _activations + " in)");
			if(_activations < 1) 
			{
				isActive = false;
				GetComponent<SpriteRenderer>().sprite = spriteOFF;
				if(switchOFFClip != null)
					AudioSource.PlayClipAtPoint(switchOFFClip, transform.position);
				_timeActive = 0f;
				_lastCallTime = 0f;
				switchedOFF();
			}
		}
	}
}
