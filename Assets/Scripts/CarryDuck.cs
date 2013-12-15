using UnityEngine;
using System.Collections;

public class CarryDuck : MonoBehaviour
{
	public AudioClip[] duckClips;		// Sound for when the bomb crate is picked up.
	public AudioClip  pickupClip;

	public GUIText duckWarning;
	private Animator anim;				// Reference to the animator component.
	private bool 	_carried = false;		// Whether or not the player has the duck

	private float 	_pickupTime = 0.0f;
	private float _carriedTime = 0.0f;
	private float _lastEventTime = 0.0f;
	private float _lastEventDuration;

	public float 	duckEventInterval = 5.0f;

	private bool _showDuckWarning = false;
	private Color _duckWarningColor;
	private Vector3 _duckWarningPos;
		
	void Awake()
	{
		// Setting up the reference.
		_duckWarningColor = duckWarning.color;
		_duckWarningColor.a = 0.0f;
		duckWarning.color = _duckWarningColor;
		_duckWarningPos = duckWarning.transform.position;
		_duckWarningPos.y = -0.5f;
		duckWarning.transform.position = _duckWarningPos;
		anim = transform.root.GetComponent<Animator>();

	}

	public void hasDuck(){
		// ... play the pickup sound effect.
		AudioSource.PlayClipAtPoint(pickupClip, transform.position);
		_pickupTime = Time.time;
		_lastEventTime = _pickupTime;
		_carried = true;
	}

	bool timeIsRightForEvent (float time) {
		if ( _carriedTime - _lastEventTime >= duckEventInterval) {
			_lastEventTime = _carriedTime;
			return true;
		}
		return false;
	}

	void FixedUpdate() {
		if(_showDuckWarning) { //an event is going on
			if(Time.fixedTime > (_lastEventTime + _lastEventDuration)) {
			   lastDuckEventFinished();
			}
			else {
				float warnIncr = (Time.fixedTime - _lastEventTime);
				warnIncr = warnIncr >= 1.0f ? warnIncr - 1.0f : warnIncr;
				_duckWarningColor.a = warnIncr;			
				duckWarning.color = _duckWarningColor;
				_duckWarningPos.y = -0.5f + warnIncr;
				duckWarning.transform.position = _duckWarningPos;

			}
		}
		else if(_carried) {
			_carriedTime = Time.fixedTime - _pickupTime;
			if ( timeIsRightForEvent( _carriedTime) ) {
				_showDuckWarning = true;
				launchDuckEvent ( Random.value);
			}
		}
	}

	void lastDuckEventFinished() {
		_showDuckWarning = false;
		_duckWarningPos.y = -0.5f;
		duckWarning.transform.position = _duckWarningPos;
		_duckWarningColor.a = 0.0f;
		duckWarning.color = _duckWarningColor;
	}

	void launchArmageddon() {
		_lastEventDuration = 5.0f; //esto dentro de cada evento
	}

	bool launchDuckEvent(float value) {
		//HALA CHATOS, INCHAOS A METER COSAS XDDDDD
		//return true if effect was applied, false otherwise so that the roll can be repeated
		if( value > 0.0f){ // always
			launchArmageddon();
			return true;
		}
		return false;
	}
}