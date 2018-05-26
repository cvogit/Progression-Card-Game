using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private static bool Created = false;

	// Set up when a scene is loaded
	void Awake() {
		if (!Created)
		{
			DontDestroyOnLoad(this.gameObject);
			Created = true;
		}

	}
}
