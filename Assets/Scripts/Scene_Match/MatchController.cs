using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Match controller.
/// Control the match controllers, resources, and flow of data.
/// </summary>
public class MatchController : MonoBehaviour {

	/// <summary>
	/// Resources for the match.
	/// </summary>

	public List<GameObject> Enemies;
	public List<GameObject> EnemySlots;
	public List<SkillController> SkillSlots;
	public PlayerCharacterController Player;
	public DeckController Deck;
	public Card_Data CardBeingPlay;
	public GameObject TargetEnemy;
	public EnemyController EnemyActing;
	public Texture2D CursorTarget;

	private List<GameObject> CardsInHand;
	private List<GameObject> Skills;
	private List<Card_Data> DiscardPile;

	/// <summary>
	/// Helpers scripts and objects
	/// </summary>
	public MatchHelperController MatchHelper;
	public MatchInputController  InputController;
	private GameController gGameController;


	/// <summary>
	/// Reset all match data and fetch current session data from Game Controller
	/// </summary>
	void Awake() {
		gGameController = GameObject.Find ("Game Controller").GetComponent<GameController> ();

		CardsInHand = new List<GameObject>();
		DiscardPile = new List<Card_Data> ();
		Enemies = new List<GameObject> ();
	}

	void Start() {
		InitializeDeck ();
		InitializeSkills ();
		InitializeEnemies ();
		InitializePlayer ();
		StartCoroutine ("StartMatch");
	}

	public void AlignHand() {
		MatchHelper.AlignCardsInHand (CardsInHand);
	}

	public IEnumerator StartMatch() {
		for (int i = 0; i < 3; i++) {

			// Draw a card
			CoroutineData DrawingCard = new CoroutineData(this, DrawTopCardFromDeck() );

			// Wait for Card to be finished drawing
			yield return DrawingCard.coroutine;

			string DrawingCoroutine = null;
			DrawingCoroutine = (string)DrawingCard.result;

			yield return new WaitUntil(() => DrawingCoroutine == "Finished");
		}

		StartCoroutine ("StartPlayerTurn");
	}

	public IEnumerator StartPlayerTurn() {

		// TODO activate all OnPlayerTurnStart effects:
		// player, skills, and enemies

		// Draw a card
		CoroutineData DrawingCard = new CoroutineData(this, DrawTopCardFromDeck() );

		// Wait for Card to be finished drawing
		yield return DrawingCard.coroutine;

		string DrawingCoroutine = null;
		DrawingCoroutine = (string)DrawingCard.result;

		yield return new WaitUntil(() => DrawingCoroutine == "Finished");

		// Resolve player on turn start effects
		Player.StartPlayerTurn();

		// Allow user input
		InputController.PlayerInputPermission = true;


		yield return null;
	}

	public void EndPlayerTurn() {
		// TODO go through and resolve OnPlayerTurnEnd effects

		InputController.PlayerInputPermission = false;
		StartCoroutine( "StartEnemiesTurn" );
	}

	public IEnumerator StartEnemiesTurn() {

		// Run each enemies turn effects
		for (int i = 0; i < Enemies.Count; i++) {
			EnemyActing = Enemies [i].GetComponent<EnemyController> ();

			string Result;
			CoroutineData EnemyTurn = new CoroutineData(this, Enemies [i].GetComponent<EnemyController> ().StartEnemyTurn () );

			// Wait for Card to be finished drawing
			yield return EnemyTurn.coroutine;

			Result = (string)EnemyTurn.result;

			yield return new WaitUntil(() => Result == "Finished");
		}

		StartCoroutine( "StartPlayerTurn" );

		yield return "Finished";
	}

	public void EndEnemiesTurn() {

	}

	public void ChangeCursorToDefault() {
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}

	public void ChangeCursorToTarget() {
		Cursor.SetCursor(CursorTarget, Vector2.zero, CursorMode.Auto);
	}


	public IEnumerator DrawTopCardFromDeck() {

		// Set magnify on hover off
		InputController.CanMagnify = false;
		InputController.UnmagnifyCard ();

		string Result;

		// Get the card_data from the deck
		Card_Data tCardData = Deck.DrawTopCard();

		// Wait until the deck return a card_data
		yield return new WaitUntil(() => tCardData != null);

		// Set the card data into a template
		GameObject tCard = null;
		tCard = MatchHelper.SetCardData(tCardData);

		yield return new WaitUntil(() => tCard != null);

		// Show the card draw from the top of the deck
		CoroutineData DrawingCoroutine = new CoroutineData(this, MatchHelper.RenderCardFromDeck (tCard) );

		// Wait for Card to be drew from the deck
		yield return DrawingCoroutine.coroutine;

		Result = null;
		Result = (string)DrawingCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		// Put the card in the player hand
		CoroutineData PutCardInHandCoroutine = new CoroutineData(this, PutCardInHand (tCard) );
		yield return PutCardInHandCoroutine.coroutine;

		Result = null;
		Result = (string)PutCardInHandCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		// Set magnify on hover on
		InputController.CanMagnify = true;

		yield return "Finished";
	}

	/// <summary>
	/// Discards the card.
	/// </summary>
	/// <returns>The card.</returns>
	/// <param name="pCard">P card.</param>
	public IEnumerator DiscardCard(GameObject pCard) {
		string Result;

		// Put the card_data into discard list
		Card_Data tCardData = pCard.GetComponent<CardController> ().GetCardData();
		DiscardPile.Add (tCardData);

		CoroutineData DiscardingCoroutine = new CoroutineData(this, MatchHelper.RenderCardToRightSide(pCard) );

		yield return DiscardingCoroutine.coroutine;

		Result = null;
		Result = (string)DiscardingCoroutine.result;

		// Wait for Card to be render moving to the right side
		yield return new WaitUntil(() => Result == "Finished");

		yield return "Finished";
	}

	// Get list of enemies from game controller and align them
	public void InitializeEnemies() {

		// Get enemy prefab list
		List<GameObject> tEnemyPrefabs = gGameController.GetSceneEnemies ();
		int EnemyCount = tEnemyPrefabs.Count;

		for (int i = 0; i < EnemyCount; i++) {
			Enemies.Add (MatchHelper.CreateEnemy (tEnemyPrefabs [i]));
		}

		MatchHelper.AlignEnemySlots (EnemySlots, Enemies);
	}

	private void InitializeDeck() {
		List<int> tDeckIndex = gGameController.GetPlayer ().Deck;
		List<Card_Data> tDeck = MatchHelper.CreateCardList(tDeckIndex);
		Deck.SetCardsInDeck (tDeck);
	}

	private void InitializeSkills() {
		List<int> tSkills = gGameController.GetPlayer ().Skills;
		int SkillCount = tSkills.Count;

		for (int i = 0; i < SkillCount; i++) {
			if (tSkills [i] != -1) {
				GameObject tSkillObject = MatchHelper.CreateSkill (tSkills [i]);
				SkillSlots [i].SetSkill (tSkillObject);
			}
		}
	}

	private void InitializePlayer() {
		Player.SetPlayer(gGameController.GetPlayer ());
	}

	public int GetTopCardFromDeck() {
		return 0;
	}

	public int GetCurrentMana() {
		return Player.Mana;
	}

	public IEnumerator PutCardInHand(GameObject pCard) {
		// Discard the card if the hand is full
		if (CardsInHand.Count > 10) {
			DiscardCard (pCard);
			yield return "Finished";
		} else {
			yield return new WaitForSeconds(0.2f);
			pCard.GetComponent<CardController> ().Controllable = true;
			CardsInHand.Add (pCard);
			yield return "Finished";
		}
		AlignHand ();
		yield return "Finished";
	}

	public IEnumerator PlayCard(GameObject pCard) {

		// Get the card_data
		Card_Data tCardData = pCard.GetComponent<CardController> ().Card;

		// Remove the card from the hand
		CardsInHand.Remove(pCard);

		// Pay the cost of the card
		Player.PayManaCost(tCardData.manaCost);

		// TODO Check effects that activate on card activation

		// Toss the card template back to factory
		StartCoroutine(MatchHelper.RecycleCardTemplate(pCard));

		// Set card being play
		CardBeingPlay = tCardData;

		// Get all the effects of the card
		List<_Effect> tEffects = pCard.GetComponent<CardController>().GetEffects();

		// Resolve the card effects
		for (int i = 0; i < tEffects.Count; i++) {
			string Result;
			CoroutineData EffectCoroutine = new CoroutineData(this, tEffects [i].ResolveEffect () );

			yield return EffectCoroutine.coroutine;

			Result = null;
			Result = (string)EffectCoroutine.result;

			yield return new WaitUntil(() => Result == "Finished");
		}

		// Decrement card_data charge
		tCardData.charge--;
		Debug.Log ("Charge: " + tCardData.charge);

		// Put the card back into the deck or discard pile
		if (tCardData.charge > 0) {
			Deck.AddCardToDeck (tCardData);
		} else {
			DiscardPile.Add (tCardData);
		}

		AlignHand ();
		CardBeingPlay = null;
		yield return "Finished";
	}

	public IEnumerator AddBlockToPlayer(int pBlock) {
		string Result;

		CoroutineData tCoroutine = new CoroutineData(this, Player.AddBlock(pBlock) );

		yield return tCoroutine.coroutine;

		Result = null;
		Result = (string)tCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		yield return "Finished";
	}

	public IEnumerator DealDamageToPlayer(int pDamage) {
		string Result;

		CoroutineData tCoroutine = new CoroutineData(this, Player.AttackedForValue(pDamage) );

		yield return tCoroutine.coroutine;

		Result = null;
		Result = (string)tCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		yield return "Finished";
	}

	public IEnumerator PerformCoroutine(IEnumerator pCoroutine) {
		string Result;

		CoroutineData tCoroutine = new CoroutineData(this, pCoroutine );

		yield return tCoroutine.coroutine;

		Result = null;
		Result = (string)tCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		yield return "Finished";
	}

	public bool IsMatchOver() {

		if (Player.Health <= 0)
			return true;
		else if (Enemies.Count <= 0)
			return true;

		return false;
	}

	public void RemoveEnemy(GameObject pEnemy) {
		// TODO actually process enemy death animation and store data in a list, in case of revival events, etc...
		Enemies.Remove(pEnemy);
		Destroy (pEnemy);

		if (IsMatchOver ())
			EndMatch ();
	}

	public void EndMatch() {
		// TODO fade screen out, maybe show rewards
		gGameController.ChangeToSceneBeforeMatch ();
	}
}
