using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	// A class that handles all of the player's movement


	public float jump_power;
	public float move_speed;

	private Rigidbody rb;
	private bool is_jumping;
	private Vector3 movement;

	// Use this for initialization
	void Start () {
		is_jumping = false;
		rb = GetComponent<Rigidbody>();
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
		float move_horiz = Input.GetAxisRaw("Horizontal");
		float move_vertical = Input.GetAxisRaw("Vertical");

		Move(move_horiz, move_vertical);
		Turning();
	}

	void Move(float h, float v) {
		movement.Set(h, 0f, v);
		movement = movement.normalized * move_speed * Time.deltaTime;
		rb.MovePosition(transform.position + movement);

	}

	void Turning() {
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
		// See if the user wants to jump
		bool jump_key_pressed = Input.GetKeyDown("space");
		bool should_jump = jump_key_pressed && !is_jumping;

		return should_jump;
	}

	void OnCollisionEnter(Collision other) {
		// If the y component of the collision velocity is negative, then you just fell so you are no longer jumping.
		if (other.relativeVelocity.y > 0) {
			is_jumping = false;
		}
	}

}
