using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureController : MonoBehaviour {

	private GameController 	gGameController;
	private List<int> 		UnlockedMatches;

	public int 				AdventureProgressIndex;
	public 	List<GameObject> Matches;

	void Awake() {
		gGameController = GameObject.Find ("Game Controller").GetComponent<GameController> ();
	}

	void Start() {
		InitializeAdventureStatus ();
	}

	void InitializeAdventureStatus() {
		int Count = gGameController.GetAdventureProgress (AdventureProgressIndex);


		for (int i = 0; i < Count; i++) {
			if( i == (Count - 1) )
				Matches [i].GetComponent<RenderMatchNode> ().SetStatus(2);
			else 
				Matches [i].GetComponent<RenderMatchNode> ().SetStatus(1);

		}
	}
}
