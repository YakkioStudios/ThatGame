using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMain : MonoBehaviour {

	public Text die_text;

	// Use this for initialization
	void Start () {
		die_text.text = "";
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Killing Thing")) {
			KillPlayer();
		}
	}

	void KillPlayer() {
		die_text.text = "Lavacat ate you! Lavacat win!";
	}
}
