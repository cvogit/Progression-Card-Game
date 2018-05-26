using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// Map controller.
/// Selection map for adventures the player go to
/// </summary>
public class MapController : MonoBehaviour {

	private GameController 	gGameController;
	private List<int> 		UnlockedAdventures;

	public List<GameObject> Adventures;

	void Awake() {
		gGameController = GameObject.Find ("Game Controller").GetComponent<GameController> ();
	}

	void Start() {
		InitializeMapStatus ();
	}

	void InitializeMapStatus() {
		UnlockedAdventures = gGameController.GetMapProgress ();

		int Count = UnlockedAdventures.Count;

		for (int i = 0; i < Count; i++) {
			Adventures [i].GetComponent<RenderAdventureNode> ().SetActive(true);
		}
	}
}
