﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardCreator : MonoBehaviour {
	// Creates a board, which is randomly generated with resources

	public GameObject neutral_template;
	public GameObject water_template;
	public GameObject tree_template;
	public GameObject stone_template;

	public int board_width;
	public int board_height;

	public float hex_width;

	private Transform board_holder;
	private List<Vector3> tile_locations = new List<Vector3>();
	private float hex_height;

	// Use this for initialization
	void Start () {
		// Hex dimensions hardcoded
		// using
		// http://www.had2know.com/academics/hexagon-measurement-calculator.html
		hex_height = Mathf.Sqrt(3f) * hex_width / 2f;

		SetupBoard();
	}

	// Update is called once per frame
	void Update () {

	}

	void SetupBoard() {
		// Fill the tile locations array
		CreateTileLocationsGrid();

		// Instantiate Board and set boardHolder to its transform.
		board_holder = new GameObject("Board").transform;

		for (int i = 0; i < tile_locations.Count; ++i) {
			// Get a random template tile
			GameObject tile = getRandomTile();
			// Set it to the position from earlier
			Vector3 pos = tile_locations[i];

			// Create a copy of the template and put it in the correct position
			GameObject instance = Instantiate (tile, pos, tile.transform.rotation) as GameObject;

			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
			instance.transform.SetParent (board_holder);
		}

	}

	void CreateTileLocationsHex() {
		// Fill the tile locations array with all of the positions for each tile. For now this is generated in a grid fashion. Maybe it will be better to do radial-ish setup later?
		tile_locations.Clear();

	}

	void CreateTileLocationsGrid() {
		// Fill the tile locations array with all of the positions for each tile. For now this is generated in a grid fashion. Maybe it will be better to do radial-ish setup later?
		tile_locations.Clear();

		int board_x_min = -board_width / 2;
		int board_x_max = board_width / 2;

		if (board_width % 2 != 0) {
			board_x_min -= 1;
		}

		int board_z_min = -board_height / 2;
		int board_z_max = board_height / 2;

		if (board_height % 2 != 0) {
			board_z_min -= 1;
		}

		for (int x = board_x_min; x < board_x_max; ++x) {
			for (int z = board_z_min; z < board_z_max; ++z) {
				float x_pos = x * ((3 / 2f) * hex_width);

				float z_pos = z * (hex_height / 2f) - (hex_height / 2f);
				if (z % 2 == 0) {
					x_pos = x_pos + ((3/4f) * hex_width);
				}

				int y_pos_step = Random.Range(0, 3);
				float y_pos = 0.5f * y_pos_step;

				if (x_pos != 0 || z_pos != 0) {
					tile_locations.Add(new Vector3(x_pos, y_pos, z_pos));
				}

			}
		}
	}

	// Eh?
	GameObject getRandomTile() {
		GameObject tile;

		int random_num = Random.Range(0, 100);
		if (random_num < 80) {
			tile = neutral_template;
		} else if (random_num < 90) {
			tile = tree_template;
		} else if (random_num < 95) {
			tile = water_template;
		} else {
			tile = stone_template;
		}

		return tile;
	}
}
