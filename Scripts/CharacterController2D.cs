// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Controller to enable player movement using 2D Game Physics, based on Unity Script included in Standard Assets
// Features : Smooth movement / Jumping / Crouching / Events for setting up animation / 2D Physics 
// Source: Brackeys (2018) https://github.com/Brackeys/2D-Character-Controller
// Source: Tutorial Used: https://www.youtube.com/watch?v=dwcT-Dch0bA&t=860s

using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float forceWhenJumping = 100f;							// Value of force added when the player jumps
	[Range(0, 1)] [SerializeField] private float speedWhenCrouching = .36f;			// Value of maximumSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float amountOfMovementSmoothing = .05f;// Level of movement smoothing
	[SerializeField] private bool ControlInAir = false;						// If player can control when jumping
	[SerializeField] private LayerMask groundElements;							    // Mask -  determining what elements are recognised as 'ground'
	[SerializeField] private Transform groundCheck;							        // Position -  marking where if player is on the 'ground'
	[SerializeField] private Transform ceilingCheck;							    // Position - marking where ceilings are
	[SerializeField] private Collider2D colliderDisabledWhenCrouching;				// Collider -  disabled if player is crouching

	const float groundedRadiumValue = .2f; // Radius value of the overlap circle when determining if player is grounded
	private bool groundedVal;              // If player is grounded
	const float ceilingRadiusValue = .2f;  // Radius value of the overlap circle when determining if the player can stand up
	private Rigidbody2D rigidbodyComp;     // Reference to players Rigidbody compoents
	private bool isRightFacing = true;     // Direction the player is currently facing
	private Vector3 velocityValue = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandingEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public BoolEvent OnCrouchingEvent;
	private bool wasCrouching = false;

	private void Awake()
	{
		rigidbodyComp = GetComponent<Rigidbody2D>();

		if (OnLandingEvent == null)
			OnLandingEvent = new UnityEvent();

		if (OnCrouchingEvent == null)
			OnCrouchingEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = groundedVal;
		groundedVal = false;

		// Determins that player = grounded if circlecast to the groundcheck position collides with anything marked as 'ground'
		// Done using layers in Unity Inspector
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadiumValue,groundElements);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				groundedVal = true;
                if (!wasGrounded && rigidbodyComp.velocity.y < 0)
                    OnLandingEvent.Invoke();
            }
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If player = crouching, check to determine if the character can stand up
		if (!crouch)
		{
			// If player = colliding with ceiling preventing them from standing up, player will continue crouching
			if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadiusValue,groundElements))
			{
				crouch = true;
			}
		}

        // Only control the player if grounded or amountOfControlInAir is turned on
        if (groundedVal ||ControlInAir)
		{

			// If player =  crouching
			if (crouch)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
					OnCrouchingEvent.Invoke(true);
				}

				// Reducution in the player speed by the crouchingSpeed multiplier
				move *= speedWhenCrouching;

				// Disable a collider when crouching
				if (colliderDisabledWhenCrouching != null)
					colliderDisabledWhenCrouching.enabled = false;
			} else
			{
				// Enable a collider when player = not crouching
				if (colliderDisabledWhenCrouching != null)
					colliderDisabledWhenCrouching.enabled = true;

				if (wasCrouching)
				{
					wasCrouching = false;
					OnCrouchingEvent.Invoke(false);
				}
			}

			// Move the player by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rigidbodyComp.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidbodyComp.velocity = Vector3.SmoothDamp(rigidbodyComp.velocity, targetVelocity, ref velocityValue, amountOfMovementSmoothing);

			// If the input is moving the player right and the player is facing left
			if (move > 0 && !isRightFacing)
			{
				// Flip direction of the player.
				Flip();
			}
			// If the input is moving the player left and the player is facing right
			else if (move < 0 && isRightFacing)
			{
				// Fliping the player to travel in correct direction
				Flip();
			}
		}
		// If player = jumping then this will happen
		if (groundedVal && jump)
		{
			// Adding verticle forc to the player obj
			groundedVal = false;
			rigidbodyComp.AddForce(new Vector2(0f, forceWhenJumping));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		isRightFacing = !isRightFacing;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
