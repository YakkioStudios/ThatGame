using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject neutral;
	public GameObject owned;

	private Transform holder;

	// Use this for initialization
	void Awake () {
		holder = new GameObject("Board").transform;
	}

	// Update is called once per frame
	void Update () {

	}

	public void ReplaceWithNeutral(GameObject obj) {
		ReplaceWithSomething(obj, neutral);
	}

	public void ReplaceWithOwned(GameObject obj) {
		ReplaceWithSomething(obj, owned);
	}

	private void ReplaceWithSomething(GameObject obj, GameObject template) {
		AddTile(template, obj.transform.position);
		Destroy(obj);
	}

	public void AddTile(GameObject tile, Vector3 pos) {
		// Store the prefabs default rotation
		Quaternion prefab_rotation = tile.transform.rotation;

		// Randomly rotate the tile into one of 6 rotations for variance
		float y_rot = Random.Range(0, 6) * 60f;
		Quaternion rotation_modifier = Quaternion.AngleAxis(y_rot, Vector3.up);

		// Rotate the global rotation modifier then do whatever the prefab rotation is
		Quaternion final_rotation = rotation_modifier * prefab_rotation;

		// Create a copy of the template and put it in the correct position
		GameObject instance = Instantiate (tile, pos, final_rotation) as GameObject;

		//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
		instance.transform.SetParent(holder);
	}
}
