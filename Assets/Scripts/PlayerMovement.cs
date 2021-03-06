﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	// A class that handles all of the player's movement

	public float jump_power;
	public float move_speed;

	private Rigidbody rb;
	private Animator anim;
	private bool is_jumping;
	private Vector3 movement;

	// Use this for initialization
	void Start () {
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
		Animate(move_horiz, move_vertical);
		HandleTurning();
	}

	void Move(float h, float v) {
		movement.Set(h, 0f, v);
		movement = movement.normalized * move_speed * Time.deltaTime;
		rb.MovePosition(transform.position + movement);

	}

	void Animate (float h, float v) {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool ("isWalking", walking);
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
