using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float jump_time = 1f;
	public float jump_power = 100f;

	private float time_since_last_jump = 0f;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		HandleJumping();
	}

	void HandleJumping() {
		time_since_last_jump += Time.deltaTime;
		if (time_since_last_jump >= jump_time) {
			Jump();
		}
	}

	void Jump() {
		time_since_last_jump = 0f;
		rb.AddForce(new Vector3(0, jump_power, 0));
	}
}
