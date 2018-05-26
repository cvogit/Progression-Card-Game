using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal damage to player.
/// </summary>
public class DealDamageToPlayer : _Effect {

	public override IEnumerator ResolveEffect() {

		int tDamage 		= mMatchController.EnemyActing.Damage;
		Debug.Log ("Enemy damage: " + tDamage);
		PlayerCharacterController tPlayer 	= mMatchController.Player;

		string Result;

		CoroutineData AttackCoroutine = new CoroutineData(mMatchController, tPlayer.AttackedForValue (tDamage) );

		yield return AttackCoroutine.coroutine;

		Result = null;
		Result = (string)AttackCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");


		yield return "Finished";
	}
}
