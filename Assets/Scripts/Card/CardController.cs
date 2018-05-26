using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework.Constraints;

public class CardController : MonoBehaviour {

	private int Index;
	private bool ChargeHidden;

	public Card_Data Card;
	public bool Controllable 	{ get; set; }
	public bool Magnifiable 	{ get; set; }
	public string Target 		{ get; set; }

	public Text CardName;
	public Text Description;
	public Text Type;
	public Text Charge;

	public Image ArtSprite;
	public Image ManaSprite;
	public Image ChargeSprite;

	public Text Cost;

	private MatchController mMatchController;

	void Awake() {
		ChargeHidden = false;
		Card = null;
		mMatchController = GameObject.Find ("Match Controller").GetComponent<MatchController> ();
	}

	public void SetCardData(Card_Data pCardData) {
		Card = pCardData;
		InitializeCard ();
	}

	public void InitializeCard() {
		// Fill card basic details
		CardName.text 		= Card.cardName.ToString();
		Type.text 			= Card.type.ToString();
		Description.text 	= Card.description.ToString();
		Cost.text     		= Card.manaCost.ToString();
		Charge.text 		= Card.charge.ToString ();
		ArtSprite.sprite    = Card.artwork;
		Target = Card.target;

		if (Card.charge == -1 && ChargeSprite.enabled) {
			HideCharge ();
		} else if (Card.charge != -1 && !ChargeSprite.enabled) {
			ShowCharge ();
		}
	}

	public Card_Data GetCardData() {
		return Card;
	}

	void HideCharge() {
		ChargeSprite.enabled = false;
		Charge.text = "";
	}

	void ShowCharge() {
		ChargeSprite.enabled = true;
		Charge.text = Card.charge.ToString ();
	}

	public void ResetCardTemplate() {
		CardName.text 		= "";
		Type.text 			= "";
		Description.text 	= "";
		Cost.text     		= "";
		ArtSprite.sprite = null;
		Target = "";
	}

	/// <summary>
	/// Determines whether the conditions to play this card is fulfilled.
	/// </summary>
	/// <returns><c>true</c> if this instance is playable; otherwise, <c>false</c>.</returns>
	public bool IsPlayable() {
		// Set current card_data as card being check for conditions at match controller
		mMatchController.CardBeingPlay = Card;

		_PlayCondition tCondition;

		// Create the instances of the play conditions stored in card_data
		// Check if the condition is fulfilled, if any are not; return false
		for (int i = 0; i < Card.playConditions.Count; i++) {

			tCondition = (_PlayCondition) System.Activator.CreateInstance(null, Card.playConditions[i]).Unwrap();
			if (!tCondition.IsFulfilled ()) {
				return false;
			}
		}

		// If all conditions passed, return true
		return true;
	}

	public List<_Effect> GetEffects() {
		_Effect tEffect;
		List<_Effect> Result = new List<_Effect> ();

		for (int i = 0; i < Card.effects.Count; i++) {
			tEffect = (_Effect) System.Activator.CreateInstance(null, Card.effects[i]).Unwrap();
			Result.Add (tEffect);
		}

		return Result;
	}
}
