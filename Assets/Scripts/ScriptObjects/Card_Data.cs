using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card_Data_0", menuName="Card")]
public class Card_Data : ScriptableObject {

	public string cardName;
	public string description;
	public string type;
	public string target;
	public int damage;
	public int block;

	public int manaCost;
	public int charge;

	public Sprite artwork;

	public List<string> effects;
	public List<string> playConditions;
	public List<string> requirements;
	public List<string> tags;
}
