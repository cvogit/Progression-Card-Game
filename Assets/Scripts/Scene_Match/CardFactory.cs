using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CardFactory : MonoBehaviour {

	protected static CardFactory instance;

	public GameObject 		CardPrefab;
	public List<Card_Data> 	CardsData;
	public List<GameObject> CardTemplates;

	// Use this for initialization
	void Start () {
	}

	/// <summary>
	/// Creates a new card_data, set it into a card template and return.
	/// </summary>
	/// <returns>The card.</returns>
	/// <param name="pCardDataNum">P card data number.</param>
	public GameObject CreateCard(int pCardDataNum) {
		// Create the card
		if (CardTemplates.Count == 0) {
			CardTemplates.Add (Instantiate (instance.CardPrefab));
		}

		GameObject tCard = CardTemplates[0];
		CardTemplates.RemoveAt (0);
		Debug.Log (CardTemplates.Count);
		// Set the card data
		tCard.GetComponent<CardController> ().SetCardData (Object.Instantiate(CardsData[pCardDataNum]));

		return tCard;
	}

	/// <summary>
	/// Set Card_data into a card template and return the card template
	/// </summary>
	/// <returns>The card.</returns>
	/// <param name="tCardData">T card data.</param>
	public GameObject SetCard(Card_Data tCardData) {
		// Create the card
		if (CardTemplates.Count == 0) {
			CardTemplates.Add (Instantiate (instance.CardPrefab));
		}
		
		GameObject tCard = CardTemplates[0];
		CardTemplates.RemoveAt (0);

		// Set the card data
		tCard.GetComponent<CardController> ().SetCardData (tCardData);
		tCard.transform.Rotate (new Vector3 (-90, 0, 0));

		return tCard;
	}

	/// <summary>
	/// Creates a card data list and return it.
	/// </summary>
	/// <returns>The card data list.</returns>
	/// <param name="pCardIndex">P card index.</param>
	public List<Card_Data> CreateCardDataList(List<int> pCardIndex) {
		int tCount = pCardIndex.Count;
		List<Card_Data> CardDatas = new List<Card_Data> ();

		for (int i = 0; i < tCount; i++) {
			CardDatas.Add (Object.Instantiate(CardsData[pCardIndex [i]]));
		}

		return CardDatas;
	}

	/// <summary>
	/// Recycles the card template.
	/// </summary>
	/// <param name="pCard">P card.</param>
	public void RecycleCardTemplate(GameObject pCard) {
		CardTemplates.Add (pCard);
		pCard.transform.position = new Vector3 (-1000,0,0);
	}

}
