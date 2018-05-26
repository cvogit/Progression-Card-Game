using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory : MonoBehaviour {

	public List<GameObject> SkillList;

	public GameObject CreateSkill(int pSkillIndex) {
		return Instantiate (SkillList [pSkillIndex]);
	}
}
