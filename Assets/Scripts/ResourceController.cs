using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	public float resource_reload_time = 10f;
	public Material neutral_material;

	private float time_since_consumed;
	private bool is_consumed;
	private Renderer renderer;
	private Material default_material;
	private string default_tag;

	// Use this for initialization
	void Start () {
		time_since_consumed = 0f;
		is_consumed = false;

		renderer = GetComponent<Renderer>();
		default_material = renderer.material;
		default_tag = gameObject.tag;
	}

	// Update is called once per frame
	void Update () {
		if (!is_consumed) {
			time_since_consumed = 0f;
		} else {
			time_since_consumed += Time.deltaTime;
			if (time_since_consumed >= resource_reload_time) {
				ReplinishResource();
			}
		}
	}

	void ConsumeResource() {
		is_consumed = true;
		renderer.material = neutral_material;
		gameObject.tag = "Neutral";
	}

	void ReplinishResource() {
		is_consumed = false;
		renderer.material = default_material;
		gameObject.tag = default_tag;
	}
}
