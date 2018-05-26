using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour {

	private double MouseOverTime;

	public Card_Data EmptyDeck;
	public List<Card_Data> CardsInDeck;

	/// <summary>
	/// Adds the card to deck and shuffle.
	/// </summary>
	/// <param name="pCardData">P card data.</param>
	public void AddCardToDeck(Card_Data pCardData) {
		CardsInDeck.Add (pCardData);
		Shuffle ();
	}

	/// <summary>
	/// Sets the cards in deck and shuffle them.
	/// </summary>
	/// <param name="pCards">P cards.</param>
	public void SetCardsInDeck(List<Card_Data> pCards) {
		CardsInDeck = pCards;
		Shuffle ();
	}

	/// <summary>
	/// Fisher-Yates shuffle implementation
	/// Shuffle the current deck
	/// </summary>
	public void Shuffle()
	{
		var randomSeed = new System.Random();
		int cardNum = CardsInDeck.Count;

		while (cardNum > 1)
		{
			cardNum--;
			int randomCard = randomSeed.Next(cardNum + 1);
			Card_Data card = CardsInDeck[randomCard];
			CardsInDeck[randomCard] = CardsInDeck[cardNum];
			CardsInDeck[cardNum] = card;
		}
	}

	/// <summary>
	/// Draws the top card.
	/// </summary>
	/// <returns>The top card.</returns>
	public Card_Data DrawTopCard() {
		Card_Data tCardData;
		if (CardsInDeck.Count > 0) {
			tCardData = CardsInDeck [0];
			CardsInDeck.RemoveAt (0);
		} else
			tCardData = EmptyDeck;
		
		return tCardData;
	}

	void OnMouseOver() {
		if (MouseOverTime >= 0.25) {
			//TODO show card numbers in deck
		} else if (MouseOverTime < 0.25) {
			MouseOverTime += Time.deltaTime;
		}
	}

	void OnMouseExit() {
		MouseOverTime = 0;
	}
}
