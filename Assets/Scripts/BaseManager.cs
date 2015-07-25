using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseManager : MonoBehaviour {

	public GameManager game_manager;

	public float health_per_tile;
	public Text health_text;

	private int num_tiles;
	private float health;
	private float max_health;
	private bool player_in_base;

	// Use this for initialization
	void Start () {
		player_in_base = true;

		num_tiles = 0;
		UpgradeBase();

	}

	// Update is called once per frame
	void Update () {
		if (!player_in_base) {
			DamageBase();
		}
		UpdateHealthText();
	}

	void DamageBase() {
		if (health > 0) {
			float damage = 1f;
			health = health - (damage * Time.deltaTime);
		} else {
			game_manager.KillPlayer();
		}
	}

	public void UpgradeBase() {
		num_tiles += 1;
		max_health = num_tiles * health_per_tile;
		health = max_health;
		UpdateHealthText();
	}

	void UpdateHealthText() {
		health_text.text = "Health: " + health.ToString("#.0");
	}

	public void LeaveBase() {
		player_in_base = false;
	}

	public void EnterBase() {
		player_in_base = true;
	}
}
