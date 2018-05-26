using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public MatchController mMatchController;
	public CardController mCardController;
	public MatchInputController mMatchInput;
	public ObjectTransitionController Transitor;

	private Vector3 MouseToScreenOffset;
	private Vector3 MouseBeginPosition;

	private bool IsBeingDrag;
	private bool IsCursorTarget;
	private int SortingOrder;

	void Awake() {
		IsBeingDrag = false;
		IsCursorTarget = false;
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		// If the card being try to get drag is playable, allow the player to drag it
		if (mCardController.IsPlayable()) {
			IsBeingDrag = true;

			// If the any object is being magnify (likely the current object being drag) unmagnify it and set magnifiable off
			if (mMatchInput.IsMagnifyingCard) {
				mMatchInput.CanMagnify = false;
				mMatchInput.UnmagnifyCard ();
				mMatchInput.MagnifyingCard = null;
			} else {
				// Shut off magnify ability regardless
				mMatchInput.CanMagnify = false;
			}

			MouseBeginPosition = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			MouseToScreenOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, MouseBeginPosition.z));
		
			// Change Sorting Order and store original sorting order
			SortingOrder = gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder;
			gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = 20;
			gameObject.transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = 21;
		}
	}

	#endregion


	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		// If the card target is the play panel, allow the user to drag it around
		if (mCardController.Target == "Play Panel" && IsBeingDrag) {
			Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, MouseBeginPosition.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + MouseToScreenOffset;
			transform.position = cursorPosition;
		} else if (mCardController.Target == "Enemy" && IsBeingDrag && !IsCursorTarget) {
			// If the card target a specific object, lift the card up slightly and draw an arrow from the card to mouse
			Vector3 tCardPosition = transform.position;
			tCardPosition.z = -300;
			transform.position = tCardPosition;
			mMatchController.ChangeCursorToTarget ();
			IsCursorTarget = true;
		}
	}
	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if(IsBeingDrag) {

			// Set input magnify ability on again
			if (!mMatchInput.CanMagnify) {
				mMatchInput.CanMagnify = true;
			}

			// Return the card old sorting order
			gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = SortingOrder;
			gameObject.transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = SortingOrder + 1;

			SortingOrder = -10;

			Ray tRay;
			RaycastHit tHit;
			tRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			// Check gameobject the ray hit
			if (Physics.Raycast(tRay, out tHit, 10000)) 
			{
				// If the card is dropped in the correct target
				if (tHit.collider.transform.tag == mCardController.Target) {
					if (IsCursorTarget) {
						mMatchController.ChangeCursorToDefault ();
						mMatchController.TargetEnemy = tHit.collider.gameObject;
					}
					// Play the card through MatchController;
					StartCoroutine(mMatchController.PlayCard(gameObject));
				} else {
					// Reset card position back to the hand and tell match controller no card is being consider
					mMatchController.AlignHand();
					mMatchController.CardBeingPlay = null;
				}
			}
		}
	}

	#endregion
}
