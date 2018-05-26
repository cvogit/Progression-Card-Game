using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Data_0", menuName="Skill")]
public class Skill_Data : ScriptableObject {

	public string skillName;
	public string description;
	public string type;
	public string requirement;

	public int manaCost;
	public int cooldown;

	public Sprite artwork;
	public List<string> effects;
	public List<string> tags;
}
