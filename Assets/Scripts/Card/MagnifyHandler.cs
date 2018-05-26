using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MagnifyHandler : MonoBehaviour
{
	public bool Magnifiable { get; set; }
	public bool IsMagnify 	{ get; set; }

	private Vector3 NewPosition;
	private int SortingOrder = -1;

	public CardController mCardController;

	void Awake() {
		IsMagnify = false;
		Magnifiable = false;
	}

	public void OnMouseEnter() {
		if (Magnifiable) {
			IsMagnify = true;
			Transform Sprite = gameObject.transform.Find ("Sprites").transform;
			Sprite.localScale += gameObject.transform.localScale;
			Transform Text = gameObject.transform.Find ("Texts").transform;
			Text.localScale += gameObject.transform.localScale;

			// Change the sorting order
			if (SortingOrder == -1) {
				SortingOrder = gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder;

				gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = 20;
				gameObject.transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = 21;
			}
		}
	}

	public void OnMouseOver() {
		if (IsMagnify) {
			NewPosition = new Vector3 (0, 10, -200);
			NewPosition.x = gameObject.transform.position.x;
			gameObject.transform.Find ("Sprites").transform.position = NewPosition;
			gameObject.transform.Find ("Texts").transform.position = NewPosition;
		}
	}
		
	public void OnMouseExit()
	{
		if (IsMagnify) {
			IsMagnify = false;

			Transform Sprite = gameObject.transform.Find ("Sprites").transform;
			Sprite.localScale = gameObject.transform.localScale;
			Transform Text = gameObject.transform.Find ("Texts").transform;
			Text.localScale = gameObject.transform.localScale;

			gameObject.transform.Find ("Sprites").transform.position = gameObject.transform.position;
			gameObject.transform.Find ("Texts").transform.position = gameObject.transform.position;


			if (SortingOrder != -1) {
				gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = SortingOrder;
				gameObject.transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = SortingOrder + 1;

				SortingOrder = -1;
			}
		}
	}

	public void Unmagnify() {
		IsMagnify = false;

		Transform Sprite = gameObject.transform.Find ("Sprites").transform;
		Sprite.localScale = gameObject.transform.localScale;
		Transform Text = gameObject.transform.Find ("Texts").transform;
		Text.localScale = gameObject.transform.localScale;

		gameObject.transform.Find ("Sprites").transform.position = gameObject.transform.position;
		gameObject.transform.Find ("Texts").transform.position = gameObject.transform.position;


		if (SortingOrder != -1) {
			gameObject.transform.Find ("Sprites").GetComponent<Canvas> ().sortingOrder = SortingOrder;
			gameObject.transform.Find ("Texts").GetComponent<Canvas> ().sortingOrder = SortingOrder + 1;

			SortingOrder = -1;
		}
	}
}