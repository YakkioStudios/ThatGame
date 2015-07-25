using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	// A class that handles all of the player's movement

	public float jump_power;
	public float move_speed;

	public bool crazy_movement;
	public float time_to_max_speed = 1f;
	public float time_to_slow = 1f;

	private Rigidbody rb;
	private Animator anim;
	private bool is_jumping;
	private bool is_walking;
	private bool is_slowing;
	private Vector3 movement;

	private float time_moving;
	private float time_slowing;

	// Use this for initialization
	void Start () {
		time_moving = 0f;
		time_slowing = 0f;
		is_jumping = false;
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
	}

	// Fixed update for physics
	void FixedUpdate() {
		HandlePlayerJumping();
		HandlePlayerMovement();
	}

	void HandlePlayerMovement() {
		// Set the forces for horizontal (X) and the vertical (Y) axes.
		float move_horiz = Input.GetAxis("Horizontal");
		float move_vertical = Input.GetAxis("Vertical");

		Move(move_horiz, move_vertical);
		HandleTurning();
	}

	void Move(float h, float v) {
		DetermineWalking(h, v);
		Animate(1f);

		movement.Set(h, 0f, v);
		movement = movement.normalized * move_speed * Time.deltaTime;
		rb.MovePosition(transform.position + movement);

	}

	void MoveCrazy(float h, float v) {
		UpdateMovingTime(h, v);
		float speed_modifier = CalculateSpeedModifier();
		// Animate with the correct speed
		Animate(speed_modifier);

		if (is_walking) {
			movement.Set(h, 0f, v);
		}
		movement = movement.normalized * move_speed * speed_modifier * Time.deltaTime;
		rb.MovePosition(transform.position + movement);

	}

	void DetermineWalking(float h, float v) {
		// Create a boolean that is true if either of the input axes is non-zero.
		is_walking = h != 0f || v != 0f;
	}

	void UpdateMovingTime(float h, float v) {
		DetermineWalking(h, v);
		if (is_walking) {
			time_moving += Time.deltaTime;
			time_slowing = CalculateSpeedModifier() * time_to_slow;
		} else {
			time_moving = 0;
			time_slowing -= Time.deltaTime;
			time_slowing = Mathf.Max(time_slowing, 0);
		}
	}

	float CalculateSpeedModifier() {
		float speed_modifier = 0f;
		if (is_walking) {
			// The move speed changes based on how long the player has been moving
			speed_modifier = time_moving / time_to_max_speed;
			speed_modifier = Mathf.Min(speed_modifier, 1);
		} else if (time_slowing != 0f) {
			// The move speed changes based on how long the player has been slowing down
			is_slowing = true;
			speed_modifier = time_slowing / time_to_slow;
			speed_modifier = Mathf.Min(speed_modifier, 1);
		} else {
			is_slowing = false;
		}

		return speed_modifier;
	}

	void Animate (float speed_modifier) {
        // Tell the animator whether or not the player is walking.
		if (is_walking || is_slowing) {
			anim.speed = speed_modifier;
		} else {
			anim.speed = 1f;
		}
        anim.SetBool ("isWalking", is_walking || is_slowing);
    }

	void HandleTurning() {
		// Check to make sure the movement vector is nonzero so that we actually need a rotation
		if (movement != Vector3.zero){
			Quaternion new_rot = Quaternion.LookRotation(movement);
			rb.MoveRotation(new_rot);
		}
	}

	// Jumping force (only call from FixedUpdate)
	void HandlePlayerJumping() {

		// Check if the player should jump and set the move up force accordingly
		int move_up = 0;
		if (ShouldJump()) {
			move_up = 1;
			is_jumping = true;
		}

		// Build the jumpment vector
		Vector3 jumpment = new Vector3(0, move_up, 0);
		jumpment = jumpment * jump_power;

		rb.AddForce(jumpment);
	}

	bool ShouldJump() {
		bool should_jump = true;
		// See if the user wants to jump
		bool jump_key_pressed = Input.GetKeyDown("space");

		should_jump &= jump_key_pressed;
		should_jump &= !is_jumping;

		return should_jump;
	}

	void OnCollisionEnter(Collision other) {
		// If the y component of the collision velocity is negative, then you just fell so you are no longer jumping.
		if (other.relativeVelocity.y > 0) {
			is_jumping = false;
		}
	}

}
