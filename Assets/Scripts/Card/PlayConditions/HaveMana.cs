using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveMana : _PlayCondition {

	/// <summary>
	/// Check if the player have enough mana.
	/// </summary>
	/// <returns><c>true</c> if this instance is enough mana the specified pManaCost; otherwise, <c>false</c>.</returns>
	/// <param name="pManaCost">P mana cost.</param>
	public override bool IsFulfilled() {
		int tCurrentMana = mMatchController.GetCurrentMana ();
		if (tCurrentMana >= mMatchController.CardBeingPlay.manaCost)
			return true;
		else
			return false;
	}
}
