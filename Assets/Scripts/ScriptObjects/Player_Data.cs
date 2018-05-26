using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player data.
/// Saving Player progression
/// </summary>
[System.Serializable]
public class Player_Data {

	/// <summary>
	/// Default values of player data
	/// </summary>
	private int MAX_HEALTH = 20;

	public int PlayerLevel 	{ get; set; }
	public int Health 		{ get; set; }
	public int MaxHealth 	{ get; set; }

	public List<int> Deck { get; set; }

	public List<int> Skills { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Player_Data"/> class.
	/// </summary>
	public Player_Data() {
		PlayerLevel = 1;
		MaxHealth 	= MAX_HEALTH;
		Health 		= MAX_HEALTH;

		// Initital Deck
		Deck = new List<int> { 1,1,2,2,3 };

		Skills = new List<int> ( new int[4] ) ;
		Skills [0] = -1;
		Skills [1] = -1;
		Skills [2] = -1;
		Skills [3] = -1;
	}
}
