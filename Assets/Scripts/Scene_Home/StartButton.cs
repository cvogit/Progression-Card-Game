using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	void OnMouseDown() {
		SceneController.ChangeScene ("Map");
	}
}
