using UnityEngine;
using System.Collections;

public class PlayerMain : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Killing Thing")) {
			KillPlayer();
		} else if (other.CompareTag("Tile")) {
			Debug.Log("You are on a new tile: " + other.ToString());
		}
	}

	void KillPlayer() {
		Debug.Log("You die");
	}
}
