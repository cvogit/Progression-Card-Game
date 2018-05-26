using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck_Data_0", menuName="Deck")]
public class Deck_Data : ScriptableObject {

	public string deckName;
	public string race;

	public int[] cards;
	public int[] skills;
}
