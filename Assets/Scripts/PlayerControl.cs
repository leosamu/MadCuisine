using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	//[HideInInspector]
	//public bool facingRight = true;			// For determining which way the player is currently facing.
	//[HideInInspector]
	//public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public Vector2 maxSpeed;				// The fastest the player can travel
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
//	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform sprite;
//	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	private Vector2	_speed;
	//private Transform	_prevTransform;
	public bool _ICanHasDuck = false;
	public bool _playerHasDuck = false;

	public float h,v;						//vertical and horizontal input controls
	public bool  f1;						//fire1 input control

	void Awake()
	{
		// Setting up references.
		if(maxSpeed.magnitude < 0.01f)
			maxSpeed.x = 2f; maxSpeed.y = 2f;
//		groundCheck = transform.Find("groundCheck");
		sprite = transform.Find ("chefSprite");
	//	_prevTransform = sprite;
		anim = GetComponent<Animator>();
		Random.seed = 69;
	}

	public void canHasDuck() {
		_ICanHasDuck = true;
	}
	
	public void noCanHasDuck() {
		_ICanHasDuck = false;
	}

	void Update()
	{
		if(Input.GetButtonUp("Fire1") && (_ICanHasDuck || _playerHasDuck)) {
			if(!_playerHasDuck) {
				GetComponent<CarryDuck>().hasDuck();
				_playerHasDuck = true;
			}
			else{
				GetComponent<CarryDuck>().noHasDuck();
				_playerHasDuck = false;
			}
		}
	}


	void FixedUpdate ()
	{
		//Debug.Log(h);
		// Cache the horizontal input
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
		#endif

		//Debug.Log(h);
		_speed.x = h;
		_speed.y = v;
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(_speed.magnitude));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(_speed.x * rigidbody2D.velocity.x < maxSpeed.x)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * _speed.x * moveForce);
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed.x)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity.Set(maxSpeed.x, rigidbody2D.velocity.y);
		if(_speed.y * rigidbody2D.velocity.y < maxSpeed.y)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.up * _speed.y * moveForce);
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed.y)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity.Set(rigidbody2D.velocity.x, maxSpeed.y);
		
		_speed = rigidbody2D.velocity;
		Vector2 sn = _speed.normalized;
		if(sn.magnitude > 0.1f) {
			float angle = 270.0f + Mathf.Atan2(sn.y, sn.x)*180.0f/Mathf.PI;
			//	Vector2.Angle(_speed.normalized, Vector2.up);
			//Debug.Log("Speed: " + _speed + " Angle: " + angle);
			//angle = Mathf.Lerp(sprite.rotation.z, angle, Time.deltaTime);
			sprite.rotation = Quaternion.Euler (0, 0, angle);
		}

		/*
		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
		*/
	}

	void LateUpdate(){
		//_ICanHasDuck = false;
	}

	/*
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	*/

	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!audio.isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				audio.clip = taunts[tauntIndex];
				audio.Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
