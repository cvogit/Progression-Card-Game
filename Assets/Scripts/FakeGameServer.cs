using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGameServer : MonoBehaviour {

	private static bool Created = false;
	private int PlayerLevel = 1;

	private int PlayerCardsUnLocked = 1;
	private int PlayerItemsUnLocked = 1;
	private int PlayerSkillsUnLocked = 1;

	private int PlayerMapsUnLocked = 1;
	private List<int> PlayerDeck;

	void Awake() {
		if (!Created)
		{
			DontDestroyOnLoad(this.gameObject);
			Created = true;
		}

		// Fake data from local save

		PlayerDeck = new List<int>();

	}

	public int GetTopCardFromDeck() {
		return 0;
	}

	public void LoadGame()
	{ 
		
	}
}
