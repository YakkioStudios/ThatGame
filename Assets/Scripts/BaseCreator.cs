using UnityEngine;
using System.Collections;

public class BaseCreator : MonoBehaviour {

	public GameObject owned;
	public BoardManager manager;

	// Use this for initialization
	void Start () {
		manager.AddTile(owned, Vector3.zero);
	}

}
