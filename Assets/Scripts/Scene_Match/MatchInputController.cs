using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MatchInputController : MonoBehaviour {

	public MatchController MatchController;
	public bool PlayerInputPermission { get; set; }
	public bool IsMagnifyingCard 	{ get; set; }
	public bool CanMagnify;
	public GameObject 	MagnifyingCard;

	private Vector3 	MagnifyCardNewPosition;
	private int SortingOrder = -1;

	void Awake() {
		PlayerInputPermission = false;
		MagnifyingCard = null;
		CanMagnify = true;
	}
	
	// Update is called once per frame
	void Update () {
		Ray tRay;
		RaycastHit tHit;

		// Check for mouse click
		if (Input.GetMouseButtonDown (0)) { 
			// Cast a ray from mouse to screen
			tRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			// Check gameobject the ray hit
			if (Physics.Raycast (tRay, out tHit, 10000)) {

				// If a card is hit
				if (tHit.collider.gameObject.tag == "Card") {

				}
			}
		} else {
			// Check hovering status
			tRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			// Check gameobject the ray hit
			if (Physics.Raycast(tRay, out tHit, 10000)) 
			{
				// If a card is being hovered over is a card and have have permission to magnify a card
				if (tHit.collider.gameObject.tag == "Card" && CanMagnify) {

					// If already was magnifying card, check if hovering over another card
					if ( IsMagnifyingCard &&(tHit.collider.gameObject != MagnifyingCard) ){
						// Unmagnify the old card and set new card to be magnify
						UnmagnifyCard ();
						MagnifyingCard = tHit.collider.gameObject;
						IsMagnifyingCard = false;
					}
					
					// If is not magnifying any card
					if (!IsMagnifyingCard) {
						// Set the new card to be the card being magnify
						MagnifyingCard = tHit.collider.gameObject;

						// Set magnifying status to true
						IsMagnifyingCard = true;

						// Bring the card up close
						Transform Sprite = tHit.collider.gameObject.transform.Find ("Sprites").transform;
						Sprite.localScale += tHit.collider.gameObject.transform.localScale;
						Transform Text = tHit.collider.gameObject.transform.Find ("Texts").transform;
						Text.localScale += tHit.collider.gameObject.transform.localScale;

						// Change the sorting order
						if (SortingOrder == -1) {
							SortingOrder = tHit.collider.gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder;

							tHit.collider.gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = 20;
							tHit.collider.gameObject.transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = 21;
						}
					}	
				} else if (tHit.collider.gameObject.tag != "Card" && IsMagnifyingCard) {
					// If an object is being hit is not a card while a card is being magnify
					// This mean the mouse leave the body of the card being magnify

					// Unmagnify the old card and set magnifying card to null
					UnmagnifyCard();
					MagnifyingCard = null;
				}
			}

			// If is magnifying a card and still hovering over it, change the card images and text as the body moves
			if (IsMagnifyingCard) {
				MagnifyCardNewPosition = new Vector3 (0, 10, -200);
				MagnifyCardNewPosition.x = MagnifyingCard.transform.position.x;
				MagnifyingCard.transform.Find ("Sprites").transform.position = MagnifyCardNewPosition;
				MagnifyingCard.transform.Find ("Texts").transform.position = MagnifyCardNewPosition;
			}
		}
	}

	public void UnmagnifyCard() {
		if (MagnifyingCard != null) {
			IsMagnifyingCard = false;

			Transform Sprite = MagnifyingCard.transform.Find ("Sprites").transform;
			Sprite.localScale = MagnifyingCard.transform.localScale;
			Transform Text = MagnifyingCard.transform.Find ("Texts").transform;
			Text.localScale = MagnifyingCard.transform.localScale;

			MagnifyingCard.transform.Find ("Sprites").transform.position = MagnifyingCard.transform.position;
			MagnifyingCard.transform.Find ("Texts").transform.position = MagnifyingCard.transform.position;


			if (SortingOrder != -1) {
				MagnifyingCard.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = SortingOrder;
				MagnifyingCard.transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = SortingOrder + 1;

				SortingOrder = -1;
			}
		}
	}
}
