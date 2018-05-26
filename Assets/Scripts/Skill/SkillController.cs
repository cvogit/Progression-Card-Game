using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour {

	private int ManaCost;
	private int Cooldown;
	private int MaxCooldown;
	private double MouseOverTime;

	private bool SkillAttached;
	private bool Usable;

	private GameObject SkillObject;
	private Skill Skill;
	private Skill_Data SkillData;

	public Sprite SkillUnavailableSprite;
	public Image  SkillSprite;
	public Text ManaCostText;

	public GameObject SkillOverlay;
	public GameObject SkillInfo;
	public GameObject ManaCostObject;

	public Text			SkillInfoName;
	public Text 		SkillInfoCooldown;
	public Text 		SkillInfoDescription;

	void Awake() {
		SkillAttached = false;
		MouseOverTime = 0;
		InitializeSkill ();
	}

	public void InitializeSkill() {
		// Fill card basic details
		if (Skill) {
			ActivateSkill ();
		} else {
			// TODO reset skill
		}
			// Go through Skill.Effect list and activate any relevant effects on equip
			// TODO
	}

	void ActivateSkill() {

		// Set up skill variable
		SkillSprite.sprite	= SkillData.artwork;
		ManaCost 			= SkillData.manaCost;
		MaxCooldown 		= SkillData.cooldown;
		ManaCostText.text 	= SkillData.manaCost.ToString();
		Cooldown = 0;
		Usable = true;

		// Set up skill info
		SetSkillInfoBox();

		Vector3 tPosition;

		// Show Mana Cost Sprite
		tPosition = SkillOverlay.transform.position;
		tPosition.y = -2;
		SkillOverlay.transform.position = tPosition;

		// Show Mana Cost Text
		tPosition = SkillOverlay.transform.position;
		tPosition.y = -2;
		SkillOverlay.transform.position = tPosition;
	}

	public void DeactivateSkill() {
		Vector3 tPosition;

		// Show Mana Cost Sprite
		tPosition = SkillOverlay.transform.position;
		tPosition.y = 2;
		SkillOverlay.transform.position = tPosition;

		// Show Mana Cost Text
		tPosition = SkillOverlay.transform.position;
		tPosition.y = 2;
		SkillOverlay.transform.position = tPosition;
	}

	void CoverSkillWithOverlay() {
		Vector3 tPosition;

		// Show Overlay
		tPosition = SkillOverlay.transform.position;
		tPosition.y = -2;
		SkillOverlay.transform.position = tPosition;
	}

	void UncoverSkillWithOverlay() {
		Vector3 tPosition;

		// Hide overlay
		tPosition = SkillOverlay.transform.position;
		tPosition.y = 2;
		SkillOverlay.transform.position = tPosition;
	}

	/// <summary>
	/// Removes the skill gameobject and destroy it.
	/// </summary>
	public void RemoveSkill() {
		if (SkillAttached) {
			Skill = null;
			SkillData = null;
			Destroy (SkillObject);
			InitializeSkill ();
			SkillAttached = false;
		}
	}

	/// <summary>
	/// Attached the skill object as the child and set the skill data for rendering the skill
	/// </summary>
	/// <param name="pSkill">P skill.</param>
	public void SetSkill(GameObject pSkillObject) {
		if (!SkillAttached) {
			SkillObject = pSkillObject;
			pSkillObject.transform.SetParent (gameObject.transform);
			Skill = pSkillObject.GetComponent<Skill> ();
			SkillData = Skill.SkillInfo;
			InitializeSkill ();
			SkillAttached = true;
		}
	}

	void SetSkillInfoBox() {
		SkillInfoName.text			= SkillData.skillName;
		SkillInfoDescription.text	= SkillData.description;
		SkillInfoCooldown.text = "Cooldown: " + SkillData.cooldown.ToString ();
	}

	public void TurnStart() {

	}

	public void TurnEnd() {

	}

	void OnMouseOver() {
		if (MouseOverTime >= 0.75 && SkillAttached) {
			Vector3 tPosition = SkillInfo.transform.position;
			tPosition.y = 2;
			SkillInfo.transform.position = tPosition;
		} else if (MouseOverTime < 0.75 && SkillAttached) {
			MouseOverTime += Time.deltaTime;
		}
	}

	void OnMouseExit() {
		Vector3 tPosition = SkillInfo.transform.position;
		tPosition.y = -2;
		SkillInfo.transform.position = tPosition;
		MouseOverTime = 0;
	}

}
