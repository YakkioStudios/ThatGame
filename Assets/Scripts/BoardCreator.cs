using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardCreator : MonoBehaviour {
	// Creates a board, which is randomly generated with resources

	public GameObject[] templates;

	public int board_size;
	public float hex_width;
	public int height_steps;
	public float height_per_step;

	private BoardManager board_manager;

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
	void Awake () {
		board_manager = GetComponent<BoardManager>();
		SetupBoard();
	}

	void SetupBoard() {
		// Fill the tile locations array
		HexAxialGrid axial_grid = new HexAxialGrid(hex_width, board_size);
		List<Vector3> positions = axial_grid.hex_positions;

		CreateBoardWithPositions(positions);

	}

	void CreateBoardWithPositions(List<Vector3> positions) {
		// Instantiate Board and set boardHolder to its transform.

		for (int i = 0; i < positions.Count; ++i) {
			// Get a random template tile
			GameObject tile = templates[Random.Range(0, templates.Length)];
			// Set it to the position from earlier
			Vector3 pos = positions[i];

			pos.y = Random.Range(0, height_steps) * height_per_step;

			if (pos.x != 0 || pos.z != 0) {
				board_manager.AddTile(tile, pos);
			}
		}
	}
}
