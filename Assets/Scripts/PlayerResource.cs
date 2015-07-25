using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerResource : MonoBehaviour {

	public BoardManager board_manager;
	public BaseManager base_manager;

	public int castle_cost;
	public int repair_cost;

	public Text resource_text;

	private int resources;

	private GameObject current_tile;

	// Use this for initialization
	void Start () {
		resources = 0;
		UpdateResourceText();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")){
			if (current_tile.CompareTag("ResourceTile")){
				ConsumeResource();
			} else if (current_tile.CompareTag("Owned")) {
				RepairCastle();
			}
		} else if (Input.GetButtonDown("Fire2")) {
			if (current_tile.CompareTag("NeutralTile")) {
				BuildCastle();
			}
		}
	}

	void ConsumeResource() {
		board_manager.ReplaceWithNeutral(current_tile);
		resources++;
		UpdateResourceText();
	}

	void BuildCastle() {
		if (resources >= castle_cost) {
			board_manager.ReplaceWithOwned(current_tile);
			base_manager.UpgradeBase();
			resources -= castle_cost;
			UpdateResourceText();
		}
	}

	void RepairCastle() {
		if (resources >= repair_cost) {
			base_manager.RepairBase();
			resources -= repair_cost;
			UpdateResourceText();
		}
	}

	void OnTriggerEnter(Collider other) {
		current_tile = other.gameObject;
	}

	void UpdateResourceText() {
		resource_text.text = "Resources: " + resources;
	}
}
