using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RenderMatchNode : MonoBehaviour {

	public int Status;

	public Sprite GrayNode;
	public Sprite GreenNode;
	public Sprite YellowNode;

	public string MatchScene;

	public SpriteRenderer NodeSprite;
	public List<GameObject> Enemies;

	public int AdventureIndex;
	public int MatchIndex;

	private GameController gGameController;
	private Vector3 OriginalPosition;

	void Awake() {
		gGameController = GameObject.Find ("Game Controller").GetComponent<GameController> ();
	}

	void Start() {
		RenderNode ();
	}

	/// <summary>
	/// Toggle the status of the node.
	/// </summary>
	/// <param name="pStatus">If set to <c>true</c> p status.</param>
	public void SetStatus(int pStatus) {
		//Status = pStatus;
		Status = 1;
		RenderNode ();
	}

	public void RenderNode() {
		if (Status == 0)
			NodeSprite.sprite = GrayNode;
		else if (Status == 1)
			NodeSprite.sprite = GreenNode;
		else if (Status == 2)
			NodeSprite.sprite = YellowNode;
	}

	void OnMouseDown() {
		if (Status != 0)
			gGameController.EnterFightScene (MatchScene, SceneManager.GetActiveScene().name, Enemies, AdventureIndex, MatchIndex);
	}
}
