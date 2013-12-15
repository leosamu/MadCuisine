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
	public Transform duckTransform;
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
		_lastEventTime = 0.0f;
		_carried = true;
	}
	
	public void noHasDuck(){
		// ... play the pickup sound effect.
		AudioSource.PlayClipAtPoint(pickupClip, transform.position);
		_lastEventTime = 0.0f;
		_carried = false;
		_showDuckWarning = false;
		Transform cheft = GameObject.Find("chefSprite").transform;
		duckTransform.position = cheft.position + (cheft.position - duckTransform.position) * 1.5f;
	}

	bool timeIsRightForEvent (float time) {
		if ( _carriedTime - _lastEventTime >= duckEventInterval) {
			_lastEventTime = _carriedTime;
			return true;
		}
		return false;
	}

	void FixedUpdate() {		
		duckWarning.enabled = _showDuckWarning;
		duckTransform.collider2D.enabled=true;
		if(_carried) {
			Transform cheft = GameObject.Find("chefSprite").transform;
			duckTransform.collider2D.enabled=false;
			duckTransform.position = cheft.position;
			//duckTransform.position += Vector3.up*0.5f;
			_carriedTime = Time.fixedTime - _pickupTime;
			//Debug.Log ("Carried time:" + _carriedTime);

			if(_showDuckWarning) { //an event is going on
				float warnIncr = (_carriedTime - _lastEventTime);
				//Debug.Log("Warn Incr:" + warnIncr);
				if(warnIncr > _lastEventDuration) {
				   lastDuckEventFinished();
				}
				else {
					warnIncr = warnIncr / (2.0f*_lastEventDuration);
					_duckWarningColor.a = 0.5f + warnIncr;
					duckWarning.color = _duckWarningColor;
					_duckWarningPos = Camera.main.WorldToViewportPoint(duckTransform.position);
					_duckWarningPos.y = _duckWarningPos.y - 0.25f + warnIncr;
					duckWarning.transform.position = _duckWarningPos;
					//Debug.Log("DuckW: " + duckWarning.color.ToString() + " Pos: " + duckWarning.transform.position);
				}
			}
			else if ( timeIsRightForEvent( _carriedTime) ) {
				_showDuckWarning = true;
				launchDuckEvent ( Random.value);
			}
		}
		//what if we are not carrying the duck?
	}

	void lastDuckEventFinished() {
		_showDuckWarning = false;
		_duckWarningPos.y = -0.5f;
		duckWarning.transform.position = _duckWarningPos;
		_duckWarningColor.a = 0.0f;
		duckWarning.color = _duckWarningColor;
	}

	void launchArmageddon() {
		_lastEventDuration = 3.0f; //esto dentro de cada evento
		Debug.Log ("Armageddon Launched!!!");
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