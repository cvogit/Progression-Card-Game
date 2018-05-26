using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RenderAdventureNode : MonoBehaviour {

	public bool active;
	public SpriteRenderer NodeSprite;
	public string 	AdventureScene;

	private Vector3 OriginalPosition;
	private GameController 	gGameController;

	void Awake() {
		gGameController = GameObject.Find ("Game Controller").GetComponent<GameController> ();
	}

	void Start() {
		ChangePosition ();
	}

	/// <summary>
	/// Toggle the status of the node.
	/// </summary>
	/// <param name="pStatus">If set to <c>true</c> p status.</param>
	public void SetActive(bool pStatus) {
		active = pStatus;
		ChangePosition ();
	}

	void ChangePosition() {
		if (!active) {
			OriginalPosition = gameObject.transform.position;
			gameObject.transform.position = new Vector3 (1000, 1000);
		} else
			gameObject.transform.position = OriginalPosition;
	}

	void OnMouseDown() {
		SceneController.ChangeScene (AdventureScene);
	}
}
