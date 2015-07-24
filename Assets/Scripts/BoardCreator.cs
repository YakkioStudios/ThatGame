using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardCreator : MonoBehaviour {
	// Creates a board, which is randomly generated with resources

	public GameObject neutral_template;
	public GameObject water_template;
	public GameObject tree_template;
	public GameObject stone_template;

	public int board_size;
	public float hex_width;

	private Transform board_holder;
	private List<Vector3> tile_locations = new List<Vector3>();

	[Serializable]
	public class HexAxialGrid {
		public List<Vector3> hex_positions = new List<Vector3>();

		private int min_r;
		private int max_r;

		private int min_q;
		private int max_q;

		private Vector3 r_vec;
		private Vector3 q_vec;

		public HexAxialGrid(float width, int radius) {
			setupHexDimensions(width);
			setupBounds(radius);

			fillMap();
		}

		void setupHexDimensions(float width){
			float hex_width = width;
			float hex_height = Mathf.Sqrt(3f) * width / 2f;

			r_vec = new Vector3((3f/4f) * hex_width, 0, hex_height / 2f);
			q_vec = new Vector3(0, 0, hex_height);
		}

		void setupBounds(int radius){
			min_r = -radius;
			max_r = radius;

			min_q = -radius;
			max_q = radius;
		}

		void fillMap(){
			// With help from:
			// http://www.redblobgames.com/grids/hexagons/
			// and
			// http://www.redblobgames.com/grids/hexagons/#map-storage
			hex_positions.Clear();

			for (int q = min_q; q <= max_q; ++q) {
				// Adjust the row coordinate bounds to make a bigger hex
				int adj_min_r = min_r;
				int adj_max_r = max_r;
				if (q < 0) {
					adj_min_r -= q;
				} else if (q > 0) {
					adj_max_r -= q;
				}

				for (int r = adj_min_r; r <= adj_max_r; ++r) {
					Vector3 pos = (q * q_vec) + (r * r_vec);

					hex_positions.Add(pos);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		SetupBoard();
	}

	void SetupBoard() {
		// Fill the tile locations array
		CreateTileLocationsHex();

		// Instantiate Board and set boardHolder to its transform.
		board_holder = new GameObject("Board").transform;

		for (int i = 0; i < tile_locations.Count; ++i) {
			// Get a random template tile
			GameObject tile = getRandomTile();
			// Set it to the position from earlier
			Vector3 pos = tile_locations[i];

			if (pos.x != 0 || pos.z != 0) {
				// Create a copy of the template and put it in the correct position
				GameObject instance = Instantiate (tile, pos, tile.transform.rotation) as GameObject;

				//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
				instance.transform.SetParent (board_holder);
			}
		}

	}

	void CreateTileLocationsHex() {
		// Fill the tile locations array with all of the positions for each tile. For now this is generated in a grid fashion. Maybe it will be better to do radial-ish setup later?
		tile_locations.Clear();

		HexAxialGrid axial_grid = new HexAxialGrid(hex_width, board_size);
		tile_locations = axial_grid.hex_positions;

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
