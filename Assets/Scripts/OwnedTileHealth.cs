using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OwnedTileHealth : MonoBehaviour {

	public GameObject enemy;
	public Text health_text;
	public float max_health;
	public float health_dif;

	private float health;
	private float health_dif_current;

	// Use this for initialization
	void Start () {
		health = max_health;

		StartHealing();

		UpdateHealthText();

	}

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Debug.Log("you die!");
		} else if (health <= max_health) {
			health += health_dif_current * Time.deltaTime;
		} else {
			health = max_health;
		}

		UpdateHealthText();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")){
			StartHealing();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")){
			StartTakingDamage();
		}
	}

	void StartHealing() {
		health_dif_current = health_dif;
		enemy.SetActive(false);
	}

	void StartTakingDamage() {
		health_dif_current = -health_dif;
		enemy.SetActive(true);
	}

	void UpdateHealthText() {
		health_text.text = "Health: " + health.ToString("#.0");
	}

}
