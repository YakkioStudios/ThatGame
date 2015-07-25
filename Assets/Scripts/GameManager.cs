using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Text die_text;

	// Use this for initialization
	void Start () {
		die_text.text = "";
	}

	// Update is called once per frame
	void Update () {

	}

	public void KillPlayer() {
		Time.timeScale = 0f;
		die_text.text = "You Died";
	}

}
