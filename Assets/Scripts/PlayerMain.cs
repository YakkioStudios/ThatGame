using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMain : MonoBehaviour {

	public BaseManager base_manager;
	public GameManager game_manager;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Owned")) {
			base_manager.EnterBase();
		} else if (other.CompareTag("Killing Thing")) {
			game_manager.KillPlayer();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Owned")) {
			base_manager.LeaveBase();
		}
	}

}
