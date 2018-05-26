using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour {

	public MatchController mMatchController;

	void OnMouseDown() {
		mMatchController.EndPlayerTurn ();
	}
}
