using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stun player.
/// </summary>
public class StunPlayer : _Effect {

	public override IEnumerator ResolveEffect() {

		PlayerCharacterController tPlayer	= mMatchController.Player;

		tPlayer.SkipTurn = true;

		yield return "Finished";
	}
}
