using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardCreator : MonoBehaviour {
	// Creates a board, which is randomly generated with resources

	public GameObject neutral_template;
	public GameObject water_template;
	public GameObject tree_template;
	public GameObject stone_template;

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
		CreateTileLocations();

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

	void CreateTileLocations() {
		// Fill the tile locations array with all of the positions for each tile. For now this is generated in a grid fashion. Maybe it will be better to do radial-ish setup later?
		tile_locations.Clear();

		for (int x = -10; x < 10; ++x) {
			for (int z = -10; z < 10; ++z) {
				float x_pos = x * ((3 / 2f) * hex_width);

				float z_pos = z * (hex_height / 2f) + (hex_height / 2f);
				if (z % 2 == 0) {
					x_pos = x_pos + ((3/4f) * hex_width);
				}

				tile_locations.Add(new Vector3(x_pos, 0.5f, z_pos));
			}
		}
	}

	// Eh?
	GameObject getRandomTile() {
		GameObject tile;

		int random_num = Random.Range(0, 100);
		if (random_num < 60) {
			tile = neutral_template;
		} else if (random_num < 75) {
			tile = water_template;
		} else if (random_num < 90) {
			tile = tree_template;
		} else {
			tile = stone_template;
		}

		return tile;
	}
}
