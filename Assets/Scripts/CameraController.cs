using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset  = transform.position - player.transform.position;
	}

	// // Update it on fixed update for any positional changes from forces
	// void FixedUpdate () {
	// 	updateCameraPos();
	// }

	// LateUpdate is called once per frame, after everything else has been finished. This ensures that the player has moved for the frame before we update position
	void LateUpdate () {
		updateCameraPos();
	}

	void updateCameraPos() {
		transform.position = player.transform.position + offset;
	}
}
