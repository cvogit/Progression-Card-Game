using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Save.
/// Responsible for holding the game state of past sessions.
/// Provide data for current session.
/// </summary>
[System.Serializable]
public class Save
{
	/// <summary>
	/// Default values and properties of save data
	/// </summary>
	private int TOTAL_CARDS = 10;
	private int TOTAL_GEARS = 3;


	// Player object, holding Deck and Gear currently equip
	public Player_Data Player { get; set; }

	// The map the player currently on
	public int CurrentMap 			{ get; set; }
	public int CurrentAdventure 	{ get; set; }


	// The progress the player have made
	public List<int> MapProgress 		{ get; set; }
	public List<int> AdventureProgress 	{ get; set; }

	// The cards and gears the player have
	public List<int> CardsUnlocked { get; set; }
	public List<int> GearsUnlocked { get; set; }


	/// <summary>
	/// Initializes a new instance of the <see cref="Save"/> class.
	/// </summary>
	public Save() {
		Player = new Player_Data();

		// Initial Adventure and Map unlocked list
		List<int> DEFAULT_UNLOCKED = new List<int>();
		DEFAULT_UNLOCKED.Add (1);

		MapProgress 		= DEFAULT_UNLOCKED;
		AdventureProgress 	= DEFAULT_UNLOCKED;

		// Initial cards and decks
		CardsUnlocked = new List<int>( new int[TOTAL_CARDS] );
		CardsUnlocked [0] = 1;
		CardsUnlocked [1] = 1;

		GearsUnlocked = new List<int>( new int[TOTAL_GEARS] );

		// Initialize new map and adventure location
		CurrentMap = 0;
		CurrentAdventure = -1;
	}
}