using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerResources : MonoBehaviour {

	public Text tree_text;
	public Text stone_text;
	public Text water_text;

	private int trees = 0;
	private int stones = 0;
	private int water = 0;

	// Use this for initialization
	void Start () {
		UpdateResourcesText();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Tree")) {
			trees++;
	   	} else if (collider.CompareTag("Stone")) {
			stones++;
	   	} else if (collider.CompareTag("Water")) {
			water++;
	   	}

		UpdateResourcesText();
	}

	void UpdateResourcesText() {
		tree_text.text = "Trees: " + trees.ToString();
		stone_text.text = "Stones: " + stones.ToString();
		water_text.text = "Water: " + water.ToString();
	}
}
