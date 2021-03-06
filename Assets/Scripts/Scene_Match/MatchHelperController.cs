using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;
using UnityEngine.Experimental.UIElements.StyleEnums;

public class MatchHelperController : MonoBehaviour {
	
	public GameObject CanvasHandSlot;
	public GameObject Canvas;

	public ObjectTransitionController Transitor;
	public CardFactory CardFactory;
	public EnemyFactory EnemyFactory;
	public SkillFactory SkillFactory;

	public void AlignCardsInHand(List<GameObject> pCardsInHand) {
		int Count = pCardsInHand.Count;
		if (Count == 0) //if list is null, stop function
			return;

		float LeftMargin;
		float SpaceBetweenCards;

		// Set how far the left most card is, within range
		if (Count < 4) {
			LeftMargin = (Count - 1) * -57.5f;
			// Align each of the card in hand
			for (int i = 0; i < Count; i++) {
				PutCardInHandSlot(pCardsInHand [i]);
				SpaceBetweenCards = LeftMargin + (Count - 1 - i) * 115f;
				pCardsInHand [i].transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = (2 * (Count-i)) -1;
				pCardsInHand [i].transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = (2 * (Count-i));

				Transitor.AddObjectToSmoothStep (pCardsInHand [i], pCardsInHand [i].transform.position, new Vector3(SpaceBetweenCards, 2, -350), 0.5f);
			}
		} else {
			// Make sure the card is in correct slot
			// Slowly increase left most card margin
			float incrementSpace = 0f;
			for (int i = 0; i < Count-3; i++)
				incrementSpace += (57.5f / (float)Math.Pow(2,i));

			LeftMargin = 115f + incrementSpace;
			// Align each of the card in hand
			for (int i = 0; i < Count; i++) {
				PutCardInHandSlot(pCardsInHand [i]);
				SpaceBetweenCards = LeftMargin + i * ((-2*LeftMargin)/(Count-1));
				pCardsInHand [i].transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = (2 * (Count-i)) -1;
				pCardsInHand [i].transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = (2 * (Count-i));

				Transitor.AddObjectToSmoothStep (pCardsInHand [i], pCardsInHand [i].transform.position, new Vector3(SpaceBetweenCards, 2, -350), 0.5f);
			}
		}
	}

	// TODO: write a dynamic enemy position alignment function
	public void AlignEnemySlots(List<GameObject> pEnemySlot, List<GameObject> pEnemies) {
		int EnemyCount = pEnemies.Count;
		if (EnemyCount == 0) //if list is empty, stop function
			return;

		List<int> EnemySlotFill = new List<int> { 0, 0, 0, 0, 0 };
		if (EnemyCount == 1) {
			pEnemies [0].transform.SetParent (pEnemySlot [2].transform);
			Transitor.AddObjectToSmoothStep (pEnemies [0], pEnemies [0].transform.position, pEnemySlot [2].transform.position, 0.5f);

		} else if (EnemyCount == 2) {
			pEnemies [0].transform.SetParent (pEnemySlot [1].transform);
			Transitor.AddObjectToSmoothStep (pEnemies [0], pEnemies [0].transform.position, pEnemySlot [1].transform.position, 0.5f);

			pEnemies [1].transform.SetParent (pEnemySlot [3].transform);
			Transitor.AddObjectToSmoothStep (pEnemies [1], pEnemies [1].transform.position, pEnemySlot [3].transform.position, 0.5f);
		}  else if (EnemyCount == 2) {
			pEnemies [0].transform.SetParent (pEnemySlot [1].transform);
			Transitor.AddObjectToSmoothStep (pEnemies [0], pEnemies [0].transform.position, pEnemySlot [1].transform.position, 0.5f);

			pEnemies [1].transform.SetParent (pEnemySlot [2].transform);
			Transitor.AddObjectToSmoothStep (pEnemies [1], pEnemies [1].transform.position, pEnemySlot [2].transform.position, 0.5f);

			pEnemies [2].transform.SetParent (pEnemySlot [3].transform);
			Transitor.AddObjectToSmoothStep (pEnemies [2], pEnemies [2].transform.position, pEnemySlot [3].transform.position, 0.5f);
		}

	}

	// Create a card
	public GameObject CreateCard(int pCardIndex) {
		return CardFactory.CreateCard (pCardIndex);
	}

	// Create a list of card_data
	public List<Card_Data> CreateCardList(List<int> pCardIndex) {
		return CardFactory.CreateCardDataList (pCardIndex);
	}

	// Create a skill
	public GameObject CreateSkill(int pSkillIndex) {
		return SkillFactory.CreateSkill (pSkillIndex);
	}

	// Create an enemy
	public GameObject CreateEnemy(GameObject pEnemyPrefab) {
		return EnemyFactory.CreateEnemy (pEnemyPrefab);
	}
		
	// Default is in player hand slot
	public bool PutCardOnCanvas(GameObject pCard) {
		// Change the card orientation
		pCard.transform.Rotate(new Vector3(90,0,0));
		pCard.transform.SetParent (Canvas.transform);

		return true;
	}

	/// <summary>
	/// Moves the card to grave.
	/// </summary>
	/// <param name="pCard">P card.</param>
	public IEnumerator MoveCardToGrave(GameObject pCard) {
		Transitor.AddObjectToSmoothStep (pCard, pCard.transform.position, new Vector3 (1000, 0, 0), 0.5f);

		yield return new WaitForSeconds(0.5f);

		yield return "Finished";
	}

	public void PutCardInHandSlot(GameObject pCard) {
		pCard.transform.SetParent (CanvasHandSlot.transform);
	}

	public GameObject SetCardData(Card_Data pCardData) {
		GameObject tCard = CardFactory.SetCard (pCardData);

		return tCard;
	}

	/// <summary>
	/// Recycles the card template.
	/// </summary>
	/// <param name="pCard">P card.</param>
	public IEnumerator RecycleCardTemplate(GameObject pCard) {
		string Result;

		CoroutineData MovingCoroutine = new CoroutineData(this, MoveCardToGrave (pCard) );

		// Wait for Card to be drew from the deck
		yield return MovingCoroutine.coroutine;

		Result = null;
		Result = (string)MovingCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		// Recycle the card
		CardFactory.RecycleCardTemplate (pCard);

		yield return "Finished";
	}

	public IEnumerator RenderCardFromDeck(GameObject pCard) {
		bool ready;

		// Put the card where the deck is 
		pCard.transform.position = new Vector3(-511, 1, -252);

		// Render the card
		ready = PutCardOnCanvas (pCard);
		yield return new WaitUntil(() => ready == true);

		// Show the card up close
		Transitor.AddObjectToSmoothStep (pCard, pCard.transform.position, new Vector3(-70, 340, -40), 0.75f);
		yield return new WaitForSeconds(1f);
		yield return "Finished";
	}

	public IEnumerator RenderCardToRightSide(GameObject pCard) {

		Transitor.AddObjectToSmoothStep (pCard, pCard.transform.position, new Vector3 (1000, 0, 0), 0.75f);

		yield return "Finished";
	}
}
